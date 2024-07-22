#if DLAB_ASYNC
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
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
        #region GetAllEntitiesAsync
        
        /// <summary>
        /// Gets all Entities where the columnNameAndValue Pairs match
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logicalName">LogicalName of the Entity.</param>
        /// <param name="columnNameAndValuePairs">List of pairs that look like this:
        /// (string name of the column, value of the column) ie. "name", "John Doe" </param>
        /// <param name="token">Cancellation Token</param>
        /// <returns></returns>
        public static IAsyncEnumerable<Entity> GetAllEntitiesAsync(this IOrganizationServiceAsync2 service, string logicalName, CancellationToken token,
                params object[] columnNameAndValuePairs)
        {
            return service.GetAllEntitiesAsync<Entity>(QueryExpressionFactory.Create(logicalName, columnNameAndValuePairs), token: token);
        }

        /// <summary>
        /// Gets all Entities where the columnNameAndValue Pairs match
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logicalName">LogicalName of the Entity.</param>
        /// <param name="columnNameAndValuePairs">List of pairs that look like this:
        /// (string name of the column, value of the column) ie. "name", "John Doe" </param>
        /// <returns></returns>
        public static IAsyncEnumerable<Entity> GetAllEntitiesAsync(this IOrganizationServiceAsync2 service, string logicalName, 
            params object[] columnNameAndValuePairs)
        {
            return service.GetAllEntitiesAsync<Entity>(QueryExpressionFactory.Create(logicalName, columnNameAndValuePairs));
        }

        /// <summary>
        /// Gets all Entities where the columnNameAndValue Pairs match
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logicalName">LogicalName of the Entity.</param>
        /// <param name="columnSet">Columns to retrieve</param>
        /// <param name="columnNameAndValuePairs">List of pairs that look like this:
        /// (string name of the column, value of the column) ie. "name", "John Doe" </param>
        /// <param name="token">Cancellation Token</param>
        /// <returns></returns>
        public static IAsyncEnumerable<Entity> GetAllEntitiesAsync(this IOrganizationServiceAsync2 service, string logicalName, ColumnSet columnSet,
                CancellationToken token, params object[] columnNameAndValuePairs)
        {
            return service.GetAllEntitiesAsync<Entity>(QueryExpressionFactory.Create(logicalName, columnSet, columnNameAndValuePairs), token: token);
        }

        /// <summary>
        /// Gets all Entities where the columnNameAndValue Pairs match
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logicalName">LogicalName of the Entity.</param>
        /// <param name="columnSet">Columns to retrieve</param>
        /// <param name="columnNameAndValuePairs">List of pairs that look like this:
        /// (string name of the column, value of the column) ie. "name", "John Doe" </param>
        /// <returns></returns>
        public static IAsyncEnumerable<Entity> GetAllEntitiesAsync(this IOrganizationServiceAsync2 service, string logicalName, ColumnSet columnSet,
            params object[] columnNameAndValuePairs)
        {
            return service.GetAllEntitiesAsync<Entity>(QueryExpressionFactory.Create(logicalName, columnSet, columnNameAndValuePairs));
        }

        #endregion GetAllEntitiesAsync

        #region GetAllEntitiesAsync<T>

        /// <summary>
        /// Gets all entities using the Query Expression
        /// </summary>
        /// <typeparam name="T">Type of Entity List to return</typeparam>
        /// <param name="service">The service.</param>
        /// <param name="qe">Query Expression to Execute.</param>
        /// <param name="maxCount">The maximum number of entities to retrieve.  Use null for default.</param>
        /// <param name="pageSize">Number of records to return in each fetch.  Use null for default.</param>
        /// <param name="token">Cancellation Token</param>
        /// <returns></returns>
        public static IAsyncEnumerable<T> GetAllEntitiesAsync<T>(this IOrganizationServiceAsync2 service, QueryExpression qe, int? maxCount = null, int? pageSize = null, CancellationToken token = default)
            where T : Entity
        {
            return RetrieveAllEntitiesAsync<T>.GetAllEntities(service, qe, maxCount, pageSize, token);
        }

        /// <summary>
        /// Gets all entities using the Query Expression
        /// </summary>
        /// <typeparam name="T">Type of Entity List to return</typeparam>
        /// <param name="service">The service.</param>
        /// <param name="qe">Query Expression to Execute.</param>
        /// <param name="maxCount">The maximum number of entities to retrieve.  Use null for default.</param>
        /// <param name="pageSize">Number of records to return in each fetch.  Use null for default.</param>
        /// /// <param name="token">Cancellation Token</param>
        /// <returns></returns>
        public static IAsyncEnumerable<T> GetAllEntitiesAsync<T>(this IOrganizationServiceAsync2 service, TypedQueryExpression<T> qe, int? maxCount = null, int? pageSize = null, CancellationToken token = default)
            where T : Entity
        {
            return RetrieveAllEntitiesAsync<T>.GetAllEntities(service, qe, maxCount, pageSize, token);
        }


        /// <summary>
        /// Gets all Entities where the columnNameAndValue Pairs match
        /// </summary>
        /// <typeparam name="T">Type of Entity List to return</typeparam>
        /// <param name="service"></param>
        /// <param name="token">Cancellation Token</param>
        /// <param name="columnNameAndValuePairs">List of pairs that look like this:
        /// (string name of the column, value of the column) ie. "name", "John Doe" </param>
        /// <returns></returns>
        public static IAsyncEnumerable<T> GetAllEntitiesAsync<T>(this IOrganizationServiceAsync2 service,
            CancellationToken token, params object[] columnNameAndValuePairs) where T : Entity
        {
            return service.GetAllEntitiesAsync(QueryExpressionFactory.Create<T>(columnNameAndValuePairs), token: token);
        }

        /// <summary>
        /// Gets all Entities where the columnNameAndValue Pairs match
        /// </summary>
        /// <typeparam name="T">Type of Entity List to return</typeparam>
        /// <param name="service"></param>
        /// <param name="columnNameAndValuePairs">List of pairs that look like this:
        /// (string name of the column, value of the column) ie. "name", "John Doe" </param>
        /// <returns></returns>
        public static IAsyncEnumerable<T> GetAllEntitiesAsync<T>(this IOrganizationServiceAsync2 service,
            params object[] columnNameAndValuePairs) where T : Entity
        {
            return service.GetAllEntitiesAsync(QueryExpressionFactory.Create<T>(columnNameAndValuePairs));
        }

        /// <summary>
        /// Gets all Entities where the columnNameAndValue Pairs match
        /// </summary>
        /// <typeparam name="T">Type of Entity List to return</typeparam>
        /// <param name="service"></param>
        /// <param name="anonymousTypeInitializer">An Anonymous Type Initializer where the properties of the anonymous
        /// type are the column names to add</param>
        /// <param name="token">Cancellation Token</param>
        /// <param name="columnNameAndValuePairs">List of pairs that look like this:
        /// (string name of the column, value of the column) ie. "name", "John Doe" </param>
        /// <returns></returns>
        public static IAsyncEnumerable<T> GetAllEntitiesAsync<T>(this IOrganizationServiceAsync2 service,
                Expression<Func<T, object>> anonymousTypeInitializer, CancellationToken token, params object[] columnNameAndValuePairs)
            where T : Entity
        {
            var columnSet = new ColumnSet(anonymousTypeInitializer.GetAttributeNamesArray());
            return service.GetAllEntitiesAsync<T>(columnSet, token, columnNameAndValuePairs);
        }

        /// <summary>
        /// Gets all Entities where the columnNameAndValue Pairs match
        /// </summary>
        /// <typeparam name="T">Type of Entity List to return</typeparam>
        /// <param name="service"></param>
        /// <param name="anonymousTypeInitializer">An Anonymous Type Initializer where the properties of the anonymous
        /// type are the column names to add</param>
        /// <param name="columnNameAndValuePairs">List of pairs that look like this:
        /// (string name of the column, value of the column) ie. "name", "John Doe" </param>
        /// <returns></returns>
        public static IAsyncEnumerable<T> GetAllEntitiesAsync<T>(this IOrganizationServiceAsync2 service,
            Expression<Func<T, object>> anonymousTypeInitializer, params object[] columnNameAndValuePairs)
            where T : Entity
        {
            var columnSet = new ColumnSet(anonymousTypeInitializer.GetAttributeNamesArray());
            return service.GetAllEntitiesAsync<T>(columnSet, columnNameAndValuePairs);
        }

        /// <summary>
        /// Gets all Entities where the columnNameAndValue Pairs match
        /// </summary>
        /// <typeparam name="T">Type of Entity List to return</typeparam>
        /// <param name="service"></param>
        /// <param name="columnSet">Columns to retrieve</param>
        /// <param name="token">Cancellation Token</param>
        /// <param name="columnNameAndValuePairs">List of pairs that look like this:
        /// (string name of the column, value of the column) ie. "name", "John Doe" </param>
        /// <returns></returns>
        public static IAsyncEnumerable<T> GetAllEntitiesAsync<T>(this IOrganizationServiceAsync2 service, ColumnSet columnSet,
            CancellationToken token, params object[] columnNameAndValuePairs) where T : Entity
        {
            return service.GetAllEntitiesAsync(QueryExpressionFactory.Create<T>(columnSet, columnNameAndValuePairs), token:token);
        }

        /// <summary>
        /// Gets all Entities where the columnNameAndValue Pairs match
        /// </summary>
        /// <typeparam name="T">Type of Entity List to return</typeparam>
        /// <param name="service"></param>
        /// <param name="columnSet">Columns to retrieve</param>
        /// <param name="columnNameAndValuePairs">List of pairs that look like this:
        /// (string name of the column, value of the column) ie. "name", "John Doe" </param>
        /// <returns></returns>
        public static IAsyncEnumerable<T> GetAllEntitiesAsync<T>(this IOrganizationServiceAsync2 service, ColumnSet columnSet,
            params object[] columnNameAndValuePairs) where T : Entity
        {
            return service.GetAllEntitiesAsync(QueryExpressionFactory.Create<T>(columnSet, columnNameAndValuePairs));
        }

        #endregion GetAllEntitiesAsync<T>
    }
}
#endif