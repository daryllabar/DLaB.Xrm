#if DLAB_ASYNC
#nullable enable
using Microsoft.Crm.Sdk.Messages;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;


#if DLAB_UNROOT_COMMON_NAMESPACE
using DLaB.Common;
#else
#endif

#if DLAB_UNROOT_NAMESPACE || DLAB_XRM
namespace DLaB.Xrm
#else
namespace Source.DLaB.Xrm
#endif
{
    public static partial class Extensions
    {
        /// <summary>
        /// Downloads a file or image
        /// </summary>
        /// <param name="service">The service</param>
        /// <param name="entityReference">A reference to the record with the file or image column</param>
        /// <param name="attributeName">The name of the file or image column</param>
        /// <returns></returns>
        public static async Task<byte[]> DownloadFileAsync(this IOrganizationServiceAsync2 service, EntityReference entityReference, string attributeName)
        {
            var initializeFileBlocksDownloadResponse = await service.ExecuteAsync<InitializeFileBlocksDownloadResponse>(new InitializeFileBlocksDownloadRequest
            {
                Target = entityReference,
                FileAttributeName = attributeName
            });
            var fileContinuationToken = initializeFileBlocksDownloadResponse.FileContinuationToken;
            var fileSizeInBytes = initializeFileBlocksDownloadResponse.FileSizeInBytes;
            var fileBytes = new List<byte>((int)fileSizeInBytes);
            long offset = 0;
            // If chunking is not supported, chunk size will be full size of the file.
            var blockSizeDownload = !initializeFileBlocksDownloadResponse.IsChunkingSupported ? fileSizeInBytes : _4MB;

            // File size may be smaller than defined block size
            if (fileSizeInBytes < blockSizeDownload)
            {
                blockSizeDownload = fileSizeInBytes;
            }

            while (fileSizeInBytes > 0)
            {
                var downloadBlockResponse = await service.ExecuteAsync<DownloadBlockResponse>(new DownloadBlockRequest
                {
                    BlockLength = blockSizeDownload,
                    FileContinuationToken = fileContinuationToken,
                    Offset = offset
                });

                // Add the block returned to the list
                fileBytes.AddRange(downloadBlockResponse.Data);

                // Subtract the amount downloaded,
                // which may make fileSizeInBytes < 0 and indicate
                // no further blocks to download
                fileSizeInBytes -= (int)blockSizeDownload;
                // Increment the offset to start at the beginning of the next block.
                offset += blockSizeDownload;
            }

            return fileBytes.ToArray();
        }

        /// <summary>
        /// Uploads a file or image column value
        /// </summary>
        /// <param name="service">The service</param>
        /// <param name="entityReference">A reference to the record with the file or image column</param>
        /// <param name="fileAttributeName">The name of the file or image column</param>
        /// <param name="fileInfo">Information about the file or image to upload.</param>
        /// <param name="fileMimeType">The mime type of the file or image, if known.</param>
        /// <returns></returns>
        public static async Task<CommitFileBlocksUploadResponse> UploadFileAsync(this IOrganizationServiceAsync2 service, EntityReference entityReference, string fileAttributeName, FileInfo fileInfo, string? fileMimeType = null)
        {
            var initResponse = await service.ExecuteAsync<InitializeFileBlocksUploadResponse>(new InitializeFileBlocksUploadRequest
            {
                Target = entityReference,
                FileAttributeName = fileAttributeName,
                FileName = fileInfo.Name
            });

            await using var uploadFileStream = fileInfo.OpenRead();
            return await UploadStreamAsync(service, fileInfo.Name, uploadFileStream, initResponse.FileContinuationToken, fileMimeType);
        }

        private static async Task<CommitFileBlocksUploadResponse> UploadStreamAsync(IOrganizationServiceAsync2 service, string fileName, Stream uploadFileStream, string fileContinuationToken, string? fileMimeType)
        {
            var blockIds = new List<string>();
            var buffer = new byte[_4MB];
            int bytesRead;

            // While there is unread data from the file
            while ((bytesRead = await uploadFileStream.ReadAsync(buffer)) > 0)
            {
                // The file or final block may be smaller than the buffer
                if (bytesRead < buffer.Length)
                {
                    Array.Resize(ref buffer, bytesRead);
                }

                var blockId = Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()));
                blockIds.Add(blockId);

                await service.ExecuteAsync(new UploadBlockRequest
                {
                    BlockData = buffer,
                    BlockId = blockId,
                    FileContinuationToken = fileContinuationToken,
                });
            }

            // Try to get the mimetype if not provided.
            if (string.IsNullOrEmpty(fileMimeType))
            {
                var provider = new InternalFileExtensionContentTypeProvider();

                if (!provider.TryGetContentType(fileName, out fileMimeType))
                {
                    fileMimeType = "application/octet-stream";
                }
            }

            return await service.ExecuteAsync<CommitFileBlocksUploadResponse>(new CommitFileBlocksUploadRequest
            {
                BlockList = blockIds.ToArray(),
                FileContinuationToken = fileContinuationToken,
                FileName = fileName,
                MimeType = fileMimeType
            });
        }

        /// <summary>
        /// Uploads a file or image column value
        /// </summary>
        /// <param name="service">The service</param>
        /// <param name="entityReference">A reference to the record with the file or image column</param>
        /// <param name="fileAttributeName">The name of the file or image column</param>
        /// <param name="fileName">Name of the file</param>
        /// <param name="fileData">The byte array containing the file or image data to upload.</param>
        /// <param name="fileMimeType">The mime type of the file or image, if known.</param>
        /// <returns></returns>
        public static async Task<CommitFileBlocksUploadResponse> UploadFileAsync(this IOrganizationServiceAsync2 service, EntityReference entityReference, string fileAttributeName, string fileName, byte[] fileData, string? fileMimeType = null)
        {
            var initResponse = await service.ExecuteAsync<InitializeFileBlocksUploadResponse>(new InitializeFileBlocksUploadRequest
            {
                Target = entityReference,
                FileAttributeName = fileAttributeName,
                FileName = "file"
            });

            using var uploadMemoryStream = new MemoryStream(fileData);
            return await UploadStreamAsync(service, fileName, uploadMemoryStream, initResponse.FileContinuationToken, fileMimeType);
        }
    }
}
#endif