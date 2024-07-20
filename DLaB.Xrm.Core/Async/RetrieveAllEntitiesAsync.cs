#if DLAB_ASYNC
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

#if DLAB_UNROOT_NAMESPACE || DLAB_XRM
namespace DLaB.Xrm
#else
namespace Source.DLaB.Xrm
#endif
{
    internal class RetrieveAllEntitiesAsync<T> where T : Entity
    {
        private const int DefaultPageSize = 5000;
        private int _totalRetrievedCount;

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="qe">The qe.</param>
        /// <param name="maxCount">The maximum count.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="token">Cancellation Token</param>
        /// <returns></returns>
        public static IAsyncEnumerable<T> GetAllEntities(IOrganizationServiceAsync2 service, QueryExpression qe, int? maxCount = null, int? pageSize = null, CancellationToken token = default)
        {
            return new RetrieveAllEntitiesAsync<T>().GetAllEntitiesInstance(service, qe, maxCount, pageSize, token);
        }

        private async IAsyncEnumerable<T> GetAllEntitiesInstance(IOrganizationServiceAsync2 service, QueryExpression qe, int? maxCount, int? pageSize, [EnumeratorCancellation] CancellationToken token)
        {
            var page = qe.PageInfo;
            ConditionallySetPageCount(maxCount, pageSize, page);
            page.PageNumber = 1;
            page.PagingCookie = null;
            EntitiesWithCookie<T> response;
            do
            {
                response = await GetEntitiesWithCookie(service, qe, token);
                UpdatePageCount(page, maxCount);
                page.PageNumber++;
                page.PagingCookie = response.Cookie;
                foreach (var entity in response.Entities)
                {
                    yield return entity;
                }

            } while (response.MoreRecords
                     && response.Entities != null
                     && (maxCount == null || maxCount.Value <= _totalRetrievedCount));

            // No more records on server, return whatever has been received
            if (response.Entities == null)
            {
                yield break;
            }

            foreach (var entity in response.Entities)
            {
                yield return entity;
            }
        }

        private static void ConditionallySetPageCount(int? maxCount, int? pageSize, PagingInfo page)
        {
            if (maxCount != null && pageSize == null && maxCount < DefaultPageSize)
            {
                // Update page Size to Max Count to limit the number of records retrieved
                pageSize = maxCount;
            }

            // Check for page Size / Max Count Settings
            if (maxCount < pageSize)
            {
                pageSize = maxCount;
            }

            if (pageSize != null && pageSize > 0)
            {
                page.Count = pageSize.Value;
            }
        }


        private void UpdatePageCount(PagingInfo page, int? maxCount)
        {
            if (maxCount <= 0)
            {
                return;
            }

            if (page.Count + _totalRetrievedCount > maxCount)
            {
                page.Count = maxCount.Value - _totalRetrievedCount;
            }
            else
            {
                _totalRetrievedCount += page.Count;
            }
        }

        private async Task<EntitiesWithCookie<T>> GetEntitiesWithCookie(IOrganizationServiceAsync2 service, QueryExpression qe, CancellationToken token)
        {
            var response = await service.RetrieveMultipleAsync(qe, token);
            return new EntitiesWithCookie<T>(response);
        }

        private class EntitiesWithCookie<TEntity> where TEntity : Entity
        {
            public IEnumerable<TEntity> Entities { get; }
            public string Cookie { get; }
            public bool MoreRecords { get; }

            public EntitiesWithCookie(EntityCollection collection)
            {
                Entities = collection.Entities.Select(e => e.AsEntity<TEntity>());
                Cookie = collection.PagingCookie;
                MoreRecords = collection.MoreRecords;
            }
        }
    }
}
#endif