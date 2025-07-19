#nullable enable
using System;
using Microsoft.Xrm.Sdk;

#if DLAB_UNROOT_NAMESPACE || DLAB_XRM

namespace DLaB.Xrm
#else

namespace Source.DLaB.Xrm
#endif
{
    public static partial class Extensions
    {
        public static string GetAccessTokenManagedIdentity(this IManagedIdentityService service, string[] scopes) {
            string token = string.Empty;
            try
            {                
                token = service.AcquireToken(scopes);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the access token. See inner exception for details.", ex);
            }
            return token;
        }
    }
}