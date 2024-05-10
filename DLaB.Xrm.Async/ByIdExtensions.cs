using System.Collections;
using System.Linq.Expressions;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
#if DLAB_UNROOT_COMMON_NAMESPACE
using DLaB.Common;
#else
using Source.DLaB.Common;
#endif

#if DLAB_UNROOT_NAMESPACE || DLAB_XRM
namespace DLaB.Xrm
#else
namespace Source.DLaB.Xrm
#endif
{
    public static partial class Extensions
    {
        #region GetEntityOrDefaultAsync

        /// <summary>
        /// Gets the entity by id. Null is returned if it isn't found.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="logicalName">Logical name of the entity.</param>
        /// <param name="id">Id of the entity to search for.</param>
        /// <param name="columnSet">Columns to retrieve.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static Task<Entity?> GetEntityOrDefaultAsync(this IOrganizationServiceAsync2 service, string logicalName, Guid id, ColumnSet columnSet = null, CancellationToken token = default)
        {
            var idName = EntityHelper.GetIdAttributeName(logicalName);
            return columnSet == null
                ? service.GetFirstOrDefaultAsync(logicalName, token, idName, id)
                : service.GetFirstOrDefaultAsync(logicalName, columnSet, token, idName, id);
        }

        /// <summary>
        /// Gets the first entity that matches the query expression.  Null is returned if none are found.
        /// </summary>
        /// <typeparam name="T">The Entity Type.</typeparam>
        /// <param name="service">The service.</param>
        /// <param name="id">Id of the entity to search for.</param>
        /// <param name="anonymousTypeInitializer">An Anonymous Type Initializer where the properties of the anonymous
        /// type are the column names to add.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static Task<T?> GetEntityOrDefaultAsync<T>(this IOrganizationServiceAsync2 service, Guid id, Expression<Func<T, object>> anonymousTypeInitializer = null, CancellationToken token = default) where T : Entity
        {
            var idName = EntityHelper.GetIdAttributeName<T>();
            return anonymousTypeInitializer == null 
                ? service.GetFirstOrDefaultAsync<T>(token, idName, id)
                : service.GetFirstOrDefaultAsync(anonymousTypeInitializer, token, idName, id);
        }

        #endregion GetEntityOrDefaultAsync

        #region GetEntitiesByIdAsync

        /// <summary>
        /// Gets the first 5000 active entities with the given ids.
        /// </summary>
        /// <param name="service">The IOrganizationService.</param>
        /// <param name="logicalName">Logical name of the entity.</param>
        /// <param name="ids">Ids of the entity to search for.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static Task<List<Entity>> GetEntitiesByIdAsync(this IOrganizationServiceAsync2 service,
                string logicalName, IEnumerable<Guid> ids, CancellationToken token = default)
        {
            return service.GetEntitiesInAsync(logicalName, token, EntityHelper.GetIdAttributeName(logicalName), ids);
        }

        /// <summary>
        /// Gets the first 5000 active entities with the given ids.
        /// </summary>
        /// <param name="service">The IOrganizationService.</param>
        /// <param name="logicalName">Logical name of the entity.</param>
        /// <param name="token"></param>
        /// <param name="ids">Ids of the entity to search for.</param>
        /// <returns></returns>
        public static Task<List<Entity>> GetEntitiesByIdAsync(this IOrganizationServiceAsync2 service,
                string logicalName, CancellationToken token, params Guid[] ids)
        {
            return service.GetEntitiesInAsync(logicalName, token, EntityHelper.GetIdAttributeName(logicalName), ids);
        }

        /// <summary>
        /// Gets the first 5000 active entities with the given ids.
        /// </summary>
        /// <param name="service">The IOrganizationService.</param>
        /// <param name="logicalName">Logical name of the entity.</param>
        /// <param name="ids">Ids of the entity to search for.</param>
        /// <returns></returns>
        public static Task<List<Entity>> GetEntitiesByIdAsync(this IOrganizationServiceAsync2 service,
                string logicalName, params Guid[] ids)
        {
            return service.GetEntitiesByIdAsync(logicalName, default(CancellationToken), ids);
        }

        /// <summary>
        /// Gets the first 5000 active entities (with the given subset of columns only) with the given ids.
        /// </summary>
        /// <param name="service">The IOrganizationService.</param>
        /// <param name="logicalName">Logical name of the entity.</param>
        /// <param name="columnSet">Columns to retrieve.</param>
        /// <param name="ids">Ids of the entity to search for.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static Task<List<Entity>> GetEntitiesByIdAsync(this IOrganizationServiceAsync2 service, string logicalName,
                ColumnSet columnSet, IEnumerable<Guid> ids, CancellationToken token = default)
        {
            return service.GetEntitiesInAsync(logicalName, columnSet, EntityHelper.GetIdAttributeName(logicalName), ids, token);
        }

        /// <summary>
        /// Gets the first 5000 active entities (with the given subset of columns only) with the given ids.
        /// </summary>
        /// <param name="service">The IOrganizationService.</param>
        /// <param name="logicalName">Logical name of the entity.</param>
        /// <param name="columnSet">Columns to retrieve.</param>
        /// <param name="token"></param>
        /// <param name="ids">Ids of the entity to search for.</param>
        /// <returns></returns>
        public static Task<List<Entity>> GetEntitiesByIdAsync(this IOrganizationServiceAsync2 service, string logicalName,
                ColumnSet columnSet, CancellationToken token = default, params Guid[] ids)
        {
            return service.GetEntitiesInAsync(logicalName, columnSet, token, EntityHelper.GetIdAttributeName(logicalName), ids);
        }

        /// <summary>
        /// Gets the first 5000 active entities (with the given subset of columns only) with the given ids.
        /// </summary>
        /// <param name="service">The IOrganizationService.</param>
        /// <param name="logicalName">Logical name of the entity.</param>
        /// <param name="columnSet">Columns to retrieve.</param>
        /// <param name="ids">Ids of the entity to search for.</param>
        /// <returns></returns>
        public static Task<List<Entity>> GetEntitiesByIdAsync(this IOrganizationServiceAsync2 service, string logicalName,
                 ColumnSet columnSet, params Guid[] ids)
        {
            return service.GetEntitiesInAsync(logicalName, columnSet, logicalName, default, ids);
        }

        #endregion GetEntitiesByIdAsync

        #region GetEntitiesByIdAsync<T>

        /// <summary>
        /// Gets the first 5000 active entities with the given ids.
        /// </summary>
        /// <typeparam name="T">Type of Entity List to return</typeparam>
        /// <param name="service">The IOrganizationService.</param>
        /// <param name="ids">Ids of the entity to search for.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static Task<List<T>> GetEntitiesByIdAsync<T>(this IOrganizationServiceAsync2 service, IEnumerable<Guid> ids, CancellationToken token = default) where T : Entity
        {
            return service.GetEntitiesInAsync<T>(token, EntityHelper.GetIdAttributeName<T>(), ids);
        }

        /// <summary>
        /// Gets the first 5000 active entities with the given ids.
        /// </summary>
        /// <typeparam name="T">Type of Entity List to return.</typeparam>
        /// <param name="service">The IOrganizationService.</param>
        /// <param name="token"></param>
        /// <param name="ids">Ids of the entity to search for.</param>
        /// <returns></returns>
        public static Task<List<T>> GetEntitiesByIdAsync<T>(this IOrganizationServiceAsync2 service, CancellationToken token, params Guid[] ids) where T : Entity
        {
            return service.GetEntitiesInAsync<T>(token, EntityHelper.GetIdAttributeName<T>(), ids);
        }

        /// <summary>
        /// Gets the first 5000 active entities with the given ids.
        /// </summary>
        /// <typeparam name="T">Type of Entity List to return.</typeparam>
        /// <param name="service">The IOrganizationService.</param>
        /// <param name="ids">Ids of the entity to search for.</param>
        /// <returns></returns>
        public static Task<List<T>> GetEntitiesByIdAsync<T>(this IOrganizationServiceAsync2 service, params Guid[] ids) where T : Entity
        {
            return service.GetEntitiesByIdAsync<T>(default(CancellationToken), ids);
        }

        /// <summary>
        /// Gets the first 5000 active entities (with the given subset of columns only) with the given ids.
        /// </summary>
        /// <typeparam name="T">Type of Entity List to return.</typeparam>
        /// <param name="service">The IOrganizationService.</param>
        /// <param name="anonymousTypeInitializer">An Anonymous Type Initializer where the properties of the anonymous
        /// type are the column names to add.</param>
        /// <param name="ids">Ids of the entity to search for.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static Task<List<T>> GetEntitiesByIdAsync<T>(this IOrganizationServiceAsync2 service, Expression<Func<T, object>> anonymousTypeInitializer,
                IEnumerable<Guid> ids, CancellationToken token = default) where T : Entity
        {
            return service.GetEntitiesInAsync(anonymousTypeInitializer, EntityHelper.GetIdAttributeName<T>(), ids, token);
        }

        /// <summary>
        /// Gets the first 5000 active entities (with the given subset of columns only) with the given ids.
        /// </summary>
        /// <typeparam name="T">Type of Entity List to return.</typeparam>
        /// <param name="service">The IOrganizationService.</param>
        /// <param name="anonymousTypeInitializer">An Anonymous Type Initializer where the properties of the anonymous
        /// type are the column names to add.</param>
        /// <param name="token"></param>
        /// <param name="ids">Ids of the entity to search for.</param>
        /// <returns></returns>
        public static Task<List<T>> GetEntitiesByIdAsync<T>(this IOrganizationServiceAsync2 service, Expression<Func<T, object>> anonymousTypeInitializer,
                CancellationToken token = default, params Guid[] ids) where T : Entity
        {
            return service.GetEntitiesInAsync(anonymousTypeInitializer, token, EntityHelper.GetIdAttributeName<T>(), ids);
        }

        /// <summary>
        /// Gets the first 5000 active entities (with the given subset of columns only) with the given ids.
        /// </summary>
        /// <typeparam name="T">Type of Entity List to return.</typeparam>
        /// <param name="service">The IOrganizationService.</param>
        /// <param name="columnSet">Columns to retrieve</param>
        /// <param name="ids">Ids of the entity to search for.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static Task<List<T>> GetEntitiesByIdAsync<T>(this IOrganizationServiceAsync2 service, ColumnSet columnSet, IEnumerable<Guid> ids, CancellationToken token = default) where T : Entity
        {
            return service.GetEntitiesInAsync<T>(columnSet, token, EntityHelper.GetIdAttributeName<T>(), ids);
        }

        /// <summary>
        /// Gets first 5000 Active Entities (with the given subset of columns only) 
        /// where the columnNameAndValue Pairs match.
        /// </summary>
        /// <typeparam name="T">Type of Entity List to return.</typeparam>
        /// <param name="service">The IOrganizationService.</param>
        /// <param name="columnSet">Columns to retrieve.</param>
        /// <param name="token"></param>
        /// <param name="ids">Ids of the entity to search for.</param>
        /// <returns></returns>
        public static Task<List<T>> GetEntitiesByIdAsync<T>(this IOrganizationServiceAsync2 service, ColumnSet columnSet, CancellationToken token = default,
                params Guid[] ids) where T : Entity
        {
            return service.GetEntitiesInAsync<T>(columnSet, token, EntityHelper.GetIdAttributeName<T>(), ids);
        }

        /// <summary>
        /// Gets first 5000 Active Entities (with the given subset of columns only) 
        /// where the columnNameAndValue Pairs match.
        /// </summary>
        /// <typeparam name="T">Type of Entity List to return.</typeparam>
        /// <param name="service">The IOrganizationService.</param>
        /// <param name="columnSet">Columns to retrieve.</param>
        /// <param name="ids">Ids of the entity to search for.</param>
        /// <returns></returns>
        public static Task<List<T>> GetEntitiesByIdAsync<T>(this IOrganizationServiceAsync2 service, ColumnSet columnSet, params Guid[] ids) where T : Entity
        {
            return service.GetEntitiesByIdAsync<T>(columnSet, default, ids);
        }

        #endregion GetEntitiesByIdAsync<T>
    }
}
