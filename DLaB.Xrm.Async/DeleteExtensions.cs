using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

#if DLAB_UNROOT_NAMESPACE || DLAB_XRM
namespace DLaB.Xrm
#else
namespace Source.DLaB.Xrm
#endif
{
    public static partial class Extensions
    {

        #region DeleteAsync

        /// <summary>
        /// Deletes the specified entity
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="entity">The entity to be deleted.</param>
        /// <param name="token"></param>
        public static Task DeleteAsync(this IOrganizationServiceAsync2 service, Entity entity, CancellationToken token = default)
        {
            return service.DeleteAsync(entity.LogicalName, entity.Id, token);
        }

        /// <summary>
        /// Deletes the specified entity  
        /// </summary>
        /// <param name="service"></param>
        /// <param name="entity"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static Task DeleteAsync(this IOrganizationServiceAsync2 service, EntityReference entity, CancellationToken token = default)
        {
            return service.DeleteAsync(entity.LogicalName, entity.Id, token);
        }

        #endregion DeleteAsync

        #region DeleteIfExistsAsync

        /// <summary>
        /// Attempts to delete the entity with the given id. If it doesn't exist, false is returned
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="entity">The entity to delete if it exists.</param>
        /// <param name="token"></param>
        public static Task DeleteIfExistsAsync(this IOrganizationServiceAsync2 service, Entity entity, CancellationToken token = default)
        {
            return service.DeleteIfExistsAsync(entity.LogicalName, entity.Id, token);
        }

        /// <summary>
        /// Delete all active entities in the entity specified by the LogicalName and the Filter Expression
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="logicalName">The logical name of the entity that will be deleted.</param>
        /// <param name="fe">The filter expression to use to determine what records to delete.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static Task<bool> DeleteIfExistsAsync(this IOrganizationServiceAsync2 service, string logicalName, FilterExpression fe, CancellationToken token = default)
        {
            var qe = new QueryExpression(logicalName) { Criteria = fe };
            return service.DeleteIfExistsAsync(qe, token);
        }

        /// <summary>
        /// Attempts to delete the entity with the given id. If it doesn't exist, false is returned
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="entityName">Name of the entity.</param>
        /// <param name="id">The id of the entity to search and potentially delete.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static Task<bool> DeleteIfExistsAsync(this IOrganizationServiceAsync2 service, string entityName, Guid id, CancellationToken token = default)
        {
            return DeleteIfExistsWithRetryAsync(service, entityName, id, 0, token);
        }

        /// <summary>
        /// Delete all entities that are returned by the QueryExpression.
        /// </summary>
        /// <param name="service">The Service</param>
        /// <param name="qe">The query expression used to define the set of entities to delete</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<bool> DeleteIfExistsAsync(this IOrganizationServiceAsync2 service, QueryExpression qe, CancellationToken token = default)
        {
            var exists = false;
            var idName = EntityHelper.GetIdAttributeName(qe.EntityName);
            qe.ColumnSet = new ColumnSet(idName);
            qe.NoLock = true;
            var entities = await service.RetrieveMultipleAsync(qe, token);
            if (entities.Entities.Count > 0)
            {
                exists = true;
                await Task.WhenAll(entities.Entities.Select(e => service.DeleteAsync(qe.EntityName, e.Id)));
            }
            return exists;
        }

        private static async Task<bool> DeleteIfExistsInternalAsync(IOrganizationServiceAsync2 service, string logicalName, Guid id, CancellationToken token)
        {
            var exists = false;
            var idName = EntityHelper.GetIdAttributeName(logicalName);
            var qe = new QueryExpression(logicalName) { ColumnSet = new ColumnSet(idName) };

            qe.WhereEqual(idName, id);
            qe.First();
            qe.NoLock = true;
            if ((await service.RetrieveMultipleAsync(qe, token)).Entities.Count > 0)
            {
                await service.DeleteAsync(logicalName, id, token);
                exists = true;
            }
            return exists;
        }

        private static async Task<bool> DeleteIfExistsWithRetryAsync(IOrganizationServiceAsync2 service, string entityName, Guid id,
                                                    int retryCount, CancellationToken token)
        {
            bool exists;
            try
            {
                exists = await DeleteIfExistsInternalAsync(service, entityName, id, token);
            }
            catch (System.ServiceModel.FaultException<OrganizationServiceFault> ex)
            {
                if (retryCount < 10 && ex.Message.Equals("Generic SQL error.", StringComparison.CurrentCultureIgnoreCase))
                { // This is normally caused by database deadlock issue.  
                    // Attempt to reprocess once after sleeping a random number of milliseconds
                    await Task.Delay(new Random(Thread.CurrentThread.ManagedThreadId).
                        Next(1000, 5000), token);
                    exists = await DeleteIfExistsWithRetryAsync(service, entityName, id, retryCount + 1, token);
                }
                else if (ex.Message.EndsWith(id + " Does Not Exist"))
                {
                    exists = false;
                }
                else if (ex.Message == "The object you tried to delete is associated with another object and cannot be deleted.")
                {
                    throw new Exception("Entity " + entityName + " (" + id + ") is associated with another object and cannot be deleted.");
                }
                else
                {
                    throw;
                }
            }

            return exists;
        }

        #endregion DeleteIfExistsAsync

        /// <summary>
        /// Attempts to delete the Entity, eating the error if it doesn't exist
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="logicalName">Logical name of the entity.</param>
        /// <param name="id">The id.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<bool> TryDeleteAsync(this IOrganizationServiceAsync2 service, string logicalName, Guid id, CancellationToken token = default)
        {
            var exists = false;
            try
            {
                await service.DeleteAsync(logicalName, id, token);
                exists = true;
            }
            catch (System.ServiceModel.FaultException<OrganizationServiceFault> ex)
            {
                if (!ex.Message.EndsWith(id + " Does Not Exist"))
                {
                    throw;
                }
            }

            return exists;
        }

    }
}
