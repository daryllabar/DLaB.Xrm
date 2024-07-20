#if DLAB_ASYNC
#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    public static partial class Extensions
    {
        #region GetEntitiesAsync

        /// <summary>
        /// Gets first 5000 Active Entities where the columnNameAndValue Pairs match
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="logicalName">LogicalName of the Entity.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <param name="columnNameAndValuePairs">List of pairs that look like this:
        /// (string name of the column, value of the column) ie. "name", "John Doe"</param>
        /// <returns></returns>
        public static Task<List<Entity>> GetEntitiesAsync(this IOrganizationServiceAsync2 service, string logicalName, CancellationToken token,
                params object[] columnNameAndValuePairs)
        {
            return service.GetEntitiesAsync<Entity>(QueryExpressionFactory.Create(logicalName, columnNameAndValuePairs), token);
        }

        /// <summary>
        /// Gets first 5000 Active Entities where the columnNameAndValue Pairs match
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="logicalName">LogicalName of the Entity.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <param name="columnNameAndValuePairs">List of pairs that look like this:
        /// (string name of the column, value of the column) ie. "name", "John Doe"</param>
        /// <returns></returns>
        public static Task<List<Entity>> GetEntitiesAsync(this IOrganizationServiceAsync2 service, string logicalName,
                params object[] columnNameAndValuePairs)
        {
            return service.GetEntitiesAsync(logicalName, default(CancellationToken), columnNameAndValuePairs);
        }
        /// <summary>
        /// Gets first 5000 Active Entities (with the given subset of columns only) 
        /// where the columnNameAndValue Pairs match
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logicalName">LogicalName of the Entity.</param>
        /// <param name="columnSet">Columns to retrieve</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <param name="columnNameAndValuePairs">List of pairs that look like this:
        /// (string name of the column, value of the column) ie. "name", "John Doe" </param>
        /// <returns></returns>
        public static Task<List<Entity>> GetEntitiesAsync(this IOrganizationServiceAsync2 service, string logicalName, ColumnSet columnSet,
                CancellationToken token, params object[] columnNameAndValuePairs)
        {
            return service.GetEntitiesAsync<Entity>(QueryExpressionFactory.Create(logicalName, columnSet, columnNameAndValuePairs), token);
        }

        /// <summary>
        /// Gets first 5000 Active Entities (with the given subset of columns only) 
        /// where the columnNameAndValue Pairs match
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logicalName">LogicalName of the Entity.</param>
        /// <param name="columnSet">Columns to retrieve</param>
        /// <param name="columnNameAndValuePairs">List of pairs that look like this:
        /// (string name of the column, value of the column) ie. "name", "John Doe" </param>
        /// <returns></returns>
        public static Task<List<Entity>> GetEntitiesAsync(this IOrganizationServiceAsync2 service, string logicalName, ColumnSet columnSet,
                params object[] columnNameAndValuePairs)
        {
            return service.GetEntitiesAsync(logicalName, columnSet, default, columnNameAndValuePairs);
        }


        #endregion GetEntitiesAsync

        #region GetEntitiesAsync<T>

        /// <summary>
        /// Gets first 5000 Active Entities where the columnNameAndValue Pairs match
        /// </summary>
        /// <typeparam name="T">Type of Entity List to return</typeparam>
        /// <param name="service"></param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <param name="columnNameAndValuePairs">List of pairs that look like this:
        /// (string name of the column, value of the column) ie. "name", "John Doe" </param>
        /// <returns></returns>
        public static Task<List<T>> GetEntitiesAsync<T>(this IOrganizationServiceAsync2 service, CancellationToken token,
                params object[] columnNameAndValuePairs) where T : Entity
        {
            return service.GetEntitiesAsync(QueryExpressionFactory.Create<T>(columnNameAndValuePairs), token);
        }

        /// <summary>
        /// Gets first 5000 Active Entities where the columnNameAndValue Pairs match
        /// </summary>
        /// <typeparam name="T">Type of Entity List to return</typeparam>
        /// <param name="service"></param>
        /// <param name="columnNameAndValuePairs">List of pairs that look like this:
        /// (string name of the column, value of the column) ie. "name", "John Doe" </param>
        /// <returns></returns>
        public static Task<List<T>> GetEntitiesAsync<T>(this IOrganizationServiceAsync2 service, 
                params object[] columnNameAndValuePairs) where T : Entity
        {
            return service.GetEntitiesAsync<T>(default(CancellationToken), columnNameAndValuePairs);
        }

        /// <summary>
        /// Gets first 5000 Active Entities (with the given subset of columns only) 
        /// where the columnNameAndValue Pairs match
        /// </summary>
        /// <typeparam name="T">Type of Entity List to return</typeparam>
        /// <param name="service"></param>
        /// <param name="anonymousTypeInitializer">An Anonymous Type Initializer where the properties of the anonymous
        /// type are the column names to add</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <param name="columnNameAndValuePairs">List of pairs that look like this:
        /// (string name of the column, value of the column) ie. "name", "John Doe" </param>
        /// <returns></returns>
        public static Task<List<T>> GetEntitiesAsync<T>(this IOrganizationServiceAsync2 service, Expression<Func<T, object>> anonymousTypeInitializer,
                CancellationToken token = default, params object[] columnNameAndValuePairs) where T : Entity
        {
            var columnSet = new ColumnSet(anonymousTypeInitializer.GetAttributeNamesArray());
            return service.GetEntitiesAsync<T>(columnSet, token, columnNameAndValuePairs);
        }

        /// <summary>
        /// Gets first 5000 Active Entities (with the given subset of columns only) 
        /// where the columnNameAndValue Pairs match
        /// </summary>
        /// <typeparam name="T">Type of Entity List to return</typeparam>
        /// <param name="service"></param>
        /// <param name="anonymousTypeInitializer">An Anonymous Type Initializer where the properties of the anonymous
        /// type are the column names to add</param>
        /// <param name="columnNameAndValuePairs">List of pairs that look like this:
        /// (string name of the column, value of the column) ie. "name", "John Doe" </param>
        /// <returns></returns>
        public static Task<List<T>> GetEntitiesAsync<T>(this IOrganizationServiceAsync2 service, Expression<Func<T, object>> anonymousTypeInitializer,
                params object[] columnNameAndValuePairs) where T : Entity
        {
            return service.GetEntitiesAsync(anonymousTypeInitializer, default, columnNameAndValuePairs);
        }

        /// <summary>
        /// Gets first 5000 Active Entities (with the given subset of columns only) 
        /// where the columnNameAndValue Pairs match
        /// </summary>
        /// <typeparam name="T">Type of Entity List to return</typeparam>
        /// <param name="service"></param>
        /// <param name="columnSet">Columns to retrieve</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <param name="columnNameAndValuePairs">List of pairs that look like this:
        /// (string name of the column, value of the column) ie. "name", "John Doe" </param>
        /// <returns></returns>
        public static Task<List<T>> GetEntitiesAsync<T>(this IOrganizationServiceAsync2 service, ColumnSet columnSet,
                CancellationToken token, params object[] columnNameAndValuePairs) where T : Entity
        {
            return service.GetEntitiesAsync(QueryExpressionFactory.Create<T>(columnSet, columnNameAndValuePairs), token);
        }

        /// <summary>
        /// Gets first 5000 Active Entities (with the given subset of columns only) 
        /// where the columnNameAndValue Pairs match
        /// </summary>
        /// <typeparam name="T">Type of Entity List to return</typeparam>
        /// <param name="service"></param>
        /// <param name="columnSet">Columns to retrieve</param>
        /// <param name="columnNameAndValuePairs">List of pairs that look like this:
        /// (string name of the column, value of the column) ie. "name", "John Doe" </param>
        /// <returns></returns>
        public static Task<List<T>> GetEntitiesAsync<T>(this IOrganizationServiceAsync2 service, ColumnSet columnSet,
                params object[] columnNameAndValuePairs) where T : Entity
        {
            return service.GetEntitiesAsync<T>(columnSet, default, columnNameAndValuePairs);
        }

        #endregion GetEntitiesAsync<T>

        #region GetFirstAsync

        /// <summary>
        /// Retrieves the first entity where the columnNameAndValue Pairs match
        /// </summary>
        /// <param name="service">open IOrganizationServiceAsync2</param>
        /// <param name="logicalName">LogicalName of the Entity.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <param name="columnNameAndValuePairs">List of pairs that look like this:
        /// (string name of the column, value of the column) ie. "name","John Doe" goes to entity.name = "John Doe"</param>
        /// <returns></returns>
        public static async Task<Entity> GetFirstAsync(this IOrganizationServiceAsync2 service, string logicalName,
                CancellationToken token = default, params object[] columnNameAndValuePairs)
        {
            var entity = await service.GetFirstOrDefaultAsync(logicalName, columnNameAndValuePairs);
            AssertExistsWhere(entity, logicalName, columnNameAndValuePairs);
            return entity!;
        }

        /// <summary>
        /// Retrieves the first entity where the columnNameAndValue Pairs match
        /// </summary>
        /// <param name="service">open IOrganizationServiceAsync2</param>
        /// <param name="logicalName">LogicalName of the Entity.</param>
        /// <param name="columnNameAndValuePairs">List of pairs that look like this:
        /// (string name of the column, value of the column) ie. "name","John Doe" goes to entity.name = "John Doe"</param>
        /// <returns></returns>
        public static Task<Entity> GetFirstAsync(this IOrganizationServiceAsync2 service, string logicalName,
                params object[] columnNameAndValuePairs)
        {
            return service.GetFirstAsync(logicalName, default(CancellationToken), columnNameAndValuePairs);
        }

        /// <summary>
        /// Retrieves the first entity (with the given subset of columns only)
        /// where the columnNameAndValue Pairs match
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="logicalName">LogicalName of the Entity.</param>
        /// <param name="columnSet">Columns to retrieve</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <param name="columnNameAndValuePairs">List of pairs that look like this:
        /// (string name of the column, value of the column) ie. "name","John Doe" goes to entity.name = "John Doe"</param>
        /// <returns></returns>
        public static async Task<Entity> GetFirstAsync(this IOrganizationServiceAsync2 service, string logicalName, ColumnSet columnSet,
                CancellationToken token, params object[] columnNameAndValuePairs)
        {
            var entity = await service.GetFirstOrDefaultAsync(logicalName, columnSet, token, columnNameAndValuePairs);
            AssertExistsWhere(entity, logicalName, columnNameAndValuePairs);
            return entity!;
        }

        /// <summary>
        /// Retrieves the first entity (with the given subset of columns only)
        /// where the columnNameAndValue Pairs match
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="logicalName">LogicalName of the Entity.</param>
        /// <param name="columnSet">Columns to retrieve</param>
        /// <param name="columnNameAndValuePairs">List of pairs that look like this:
        /// (string name of the column, value of the column) ie. "name","John Doe" goes to entity.name = "John Doe"</param>
        /// <returns></returns>
        public static Task<Entity> GetFirstAsync(this IOrganizationServiceAsync2 service, string logicalName, ColumnSet columnSet,
                params object[] columnNameAndValuePairs)
        {
            return service.GetFirstAsync(logicalName, columnSet, default, columnNameAndValuePairs);
        }

        #endregion GetFirstAsync

        #region GetFirstAsync<T>

        /// <summary>
        /// Retrieves the first Active entity where the columnNameAndValue Pairs match
        /// </summary>
        /// <typeparam name="T">An early bound Entity Type</typeparam>
        /// <param name="service">open IOrganizationServiceAsync2</param>
        /// <param name="columnNameAndValuePairs">List of pairs that look like this:
        /// (string name of the column, value of the column) ie. "name","John Doe" goes to entity.name = "John Doe"
        /// </param>
        /// <returns></returns>
        public static async Task<T> GetFirstAsync<T>(this IOrganizationServiceAsync2 service, CancellationToken token = default,
                params object[] columnNameAndValuePairs) where T : Entity
        {
            var entity = await service.GetFirstOrDefaultAsync<T>(token, columnNameAndValuePairs);
            AssertExistsWhere(entity, columnNameAndValuePairs);
            return entity!;
        }

        /// <summary>
        /// Retrieves the first Active entity where the columnNameAndValue Pairs match
        /// </summary>
        /// <typeparam name="T">An early bound Entity Type</typeparam>
        /// <param name="service">open IOrganizationServiceAsync2</param>
        /// <param name="columnNameAndValuePairs">List of pairs that look like this:
        /// (string name of the column, value of the column) ie. "name","John Doe" goes to entity.name = "John Doe"
        /// </param>
        /// <returns></returns>
        public static Task<T> GetFirstAsync<T>(this IOrganizationServiceAsync2 service,
                params object[] columnNameAndValuePairs) where T : Entity
        {
            return service.GetFirstAsync<T>(default(CancellationToken), columnNameAndValuePairs);
        }

        /// <summary>
        /// Retrieves the first Active entity (with the given subset of columns only) 
        /// where the columnNameAndValue Pairs match
        /// </summary>
        /// <typeparam name="T">An early bound Entity Type</typeparam>
        /// <param name="service"></param>
        /// <param name="anonymousTypeInitializer">An Anonymous Type Initializer where the properties of the anonymous
        /// type are the column names to add</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <param name="columnNameAndValuePairs">List of pairs that look like this:
        /// (string name of the column, value of the column) ie. "name","John Doe" goes to entity.name = "John Doe"
        /// </param>
        /// <returns></returns>
        public static Task<T> GetFirstAsync<T>(this IOrganizationServiceAsync2 service, Expression<Func<T, object>> anonymousTypeInitializer,
                CancellationToken token, params object[] columnNameAndValuePairs) where T : Entity
        {
            var columnSet = new ColumnSet(anonymousTypeInitializer.GetAttributeNamesArray());
            return service.GetFirstAsync<T>(columnSet, token, columnNameAndValuePairs);
        }

        /// <summary>
        /// Retrieves the first Active entity (with the given subset of columns only) 
        /// where the columnNameAndValue Pairs match
        /// </summary>
        /// <typeparam name="T">An early bound Entity Type</typeparam>
        /// <param name="service"></param>
        /// <param name="anonymousTypeInitializer">An Anonymous Type Initializer where the properties of the anonymous
        /// type are the column names to add</param>
        /// <param name="columnNameAndValuePairs">List of pairs that look like this:
        /// (string name of the column, value of the column) ie. "name","John Doe" goes to entity.name = "John Doe"
        /// </param>
        /// <returns></returns>
        public static Task<T> GetFirstAsync<T>(this IOrganizationServiceAsync2 service, Expression<Func<T, object>> anonymousTypeInitializer,
                params object[] columnNameAndValuePairs) where T : Entity
        {
            return service.GetFirstAsync(anonymousTypeInitializer, default, columnNameAndValuePairs);
        }

        /// <summary>
        /// Retrieves the first Active entity (with the given subset of columns only) 
        /// where the columnNameAndValue Pairs match
        /// </summary>
        /// <typeparam name="T">An early bound Entity Type</typeparam>
        /// <param name="service"></param>
        /// <param name="columnSet">Columns to retrieve</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <param name="columnNameAndValuePairs">List of pairs that look like this:
        /// (string name of the column, value of the column) ie. "name","John Doe" goes to entity.name = "John Doe"
        /// </param>
        /// <returns></returns>
        public static async Task<T> GetFirstAsync<T>(this IOrganizationServiceAsync2 service, ColumnSet columnSet, CancellationToken token,
                params object[] columnNameAndValuePairs) where T : Entity
        {
            var entity = await service.GetFirstOrDefaultAsync<T>(columnSet, token, columnNameAndValuePairs);
            AssertExistsWhere(entity, columnNameAndValuePairs);
            return entity!;
        }

        /// <summary>
        /// Retrieves the first Active entity (with the given subset of columns only) 
        /// where the columnNameAndValue Pairs match
        /// </summary>
        /// <typeparam name="T">An early bound Entity Type</typeparam>
        /// <param name="service"></param>
        /// <param name="columnSet">Columns to retrieve</param>
        /// <param name="columnNameAndValuePairs">List of pairs that look like this:
        /// (string name of the column, value of the column) ie. "name","John Doe" goes to entity.name = "John Doe"
        /// </param>
        /// <returns></returns>
        public static Task<T> GetFirstAsync<T>(this IOrganizationServiceAsync2 service, ColumnSet columnSet,
                params object[] columnNameAndValuePairs) where T : Entity
        {
            return service.GetFirstAsync<T>(columnSet, default, columnNameAndValuePairs);
        }

        #endregion GetFirstAsync<T>

        #region GetFirstOrDefaultAsync

        /// <summary>
        /// Retrieves the first Active entity where the columnNameAndValue Pairs match
        /// </summary>
        /// <param name="service">open IOrganizationServiceAsync2</param>
        /// <param name="logicalName">Logical Name of the Entity:</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <param name="columnNameAndValuePairs"></param>
        /// <returns></returns>
        public static async Task<Entity?> GetFirstOrDefaultAsync(this IOrganizationServiceAsync2 service, string logicalName,
                CancellationToken token, params object[] columnNameAndValuePairs)
        {
            var settings = new LateBoundQuerySettings(logicalName);
            return (await service.RetrieveMultipleAsync(settings.CreateExpression(columnNameAndValuePairs), token)).Entities.FirstOrDefault();
        }

        /// <summary>
        /// Retrieves the first Active entity where the columnNameAndValue Pairs match
        /// </summary>
        /// <param name="service">open IOrganizationServiceAsync2</param>
        /// <param name="logicalName">Logical Name of the Entity:</param>
        /// <param name="columnNameAndValuePairs"></param>
        /// <returns></returns>
        public static async Task<Entity?> GetFirstOrDefaultAsync(this IOrganizationServiceAsync2 service, string logicalName, params object[] columnNameAndValuePairs)
        {
            var settings = new LateBoundQuerySettings(logicalName);
            return (await service.RetrieveMultipleAsync(settings.CreateExpression(columnNameAndValuePairs))).Entities.FirstOrDefault();
        }

        /// <summary>
        /// Retrieves the first Active entity (with the given subset of columns only) 
        /// where the columnNameAndValue Pairs match
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logicalName"></param>
        /// <param name="columnSet">Columns to retrieve</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <param name="columnNameAndValuePairs">List of pairs that look like this:
        /// (string name of the column, value of the column) ie. "name","John Doe" goes to entity.name = "John Doe"
        /// </param>
        /// <returns></returns>
        public static async Task<Entity?> GetFirstOrDefaultAsync(this IOrganizationServiceAsync2 service, string logicalName, ColumnSet columnSet, CancellationToken token,
                params object[] columnNameAndValuePairs)
        {
            var settings = new LateBoundQuerySettings(logicalName)
            {
                Columns = columnSet,
                First = true
            };
            return (await service.RetrieveMultipleAsync(settings.CreateExpression(columnNameAndValuePairs), token)).Entities.FirstOrDefault();
        }

        /// <summary>
        /// Retrieves the first Active entity (with the given subset of columns only) 
        /// where the columnNameAndValue Pairs match
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logicalName"></param>
        /// <param name="columnSet">Columns to retrieve</param>
        /// <param name="columnNameAndValuePairs">List of pairs that look like this:
        /// (string name of the column, value of the column) ie. "name","John Doe" goes to entity.name = "John Doe"
        /// </param>
        /// <returns></returns>
        public static Task<Entity?> GetFirstOrDefaultAsync(this IOrganizationServiceAsync2 service, string logicalName, ColumnSet columnSet,
                params object[] columnNameAndValuePairs)
        {
            return service.GetFirstOrDefaultAsync(logicalName, columnSet, default, columnNameAndValuePairs);
        }

        #endregion GetFirstOrDefaultAsync

        #region GetFirstOrDefaultAsync<T>

        /// <summary>
        /// Retrieves the first Active entity where the columnNameAndValue Pairs match
        /// </summary>
        /// <typeparam name="T">An early bound Entity Type</typeparam>
        /// <param name="service">open IOrganizationServiceAsync2</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <param name="columnNameAndValuePairs">List of pairs that look like this:
        /// (string name of the column, value of the column) ie. "name","John Doe" goes to entity.name = "John Doe"
        /// </param>
        /// <returns></returns>
        public static async Task<T?> GetFirstOrDefaultAsync<T>(this IOrganizationServiceAsync2 service, CancellationToken token,
              params object[] columnNameAndValuePairs) where T : Entity
        {
            var settings = new QuerySettings<T> { First = true };
            return (await service.GetEntitiesAsync<T>(settings.CreateExpression(columnNameAndValuePairs), token)).FirstOrDefault();
        }

        /// <summary>
        /// Retrieves the first Active entity where the columnNameAndValue Pairs match
        /// </summary>
        /// <typeparam name="T">An early bound Entity Type</typeparam>
        /// <param name="service">open IOrganizationServiceAsync2</param>
        /// <param name="columnNameAndValuePairs">List of pairs that look like this:
        /// (string name of the column, value of the column) ie. "name","John Doe" goes to entity.name = "John Doe"
        /// </param>
        /// <returns></returns>
        public static Task<T?> GetFirstOrDefaultAsync<T>(this IOrganizationServiceAsync2 service,
              params object[] columnNameAndValuePairs) where T : Entity
        {
            return service.GetFirstOrDefaultAsync<T>(default(CancellationToken), columnNameAndValuePairs);
        }

        /// <summary>
        /// Retrieves the first Active entity (with the given subset of columns only) 
        /// where the columnNameAndValue Pairs match
        /// </summary>
        /// <typeparam name="T">An early bound Entity Type</typeparam>
        /// <param name="service"></param>
        /// <param name="anonymousTypeInitializer">An Anonymous Type Initializer where the properties of the anonymous
        /// type are the column names to add</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <param name="columnNameAndValuePairs">List of pairs that look like this:
        /// (string name of the column, value of the column) ie. "name","John Doe" goes to entity.name = "John Doe"
        /// </param>
        /// <returns></returns>
        public static Task<T?> GetFirstOrDefaultAsync<T>(this IOrganizationServiceAsync2 service,
                Expression<Func<T, object>> anonymousTypeInitializer, CancellationToken token, params object[] columnNameAndValuePairs)
            where T : Entity
        {
            var columnSet = new ColumnSet(anonymousTypeInitializer.GetAttributeNamesArray());
            return service.GetFirstOrDefaultAsync<T>(columnSet, token, columnNameAndValuePairs);
        }

        /// <summary>
        /// Retrieves the first Active entity (with the given subset of columns only) 
        /// where the columnNameAndValue Pairs match
        /// </summary>
        /// <typeparam name="T">An early bound Entity Type</typeparam>
        /// <param name="service"></param>
        /// <param name="anonymousTypeInitializer">An Anonymous Type Initializer where the properties of the anonymous
        /// type are the column names to add</param>
        /// <param name="columnNameAndValuePairs">List of pairs that look like this:
        /// (string name of the column, value of the column) ie. "name","John Doe" goes to entity.name = "John Doe"
        /// </param>
        /// <returns></returns>
        public static Task<T?> GetFirstOrDefaultAsync<T>(this IOrganizationServiceAsync2 service,
                Expression<Func<T, object>> anonymousTypeInitializer, params object[] columnNameAndValuePairs)
            where T : Entity
        {
            return service.GetFirstOrDefaultAsync(anonymousTypeInitializer, default, columnNameAndValuePairs);
        }

        /// <summary>
        /// Retrieves the first Active entity (with the given subset of columns only) 
        /// where the columnNameAndValue Pairs match
        /// </summary>
        /// <typeparam name="T">An early bound Entity Type</typeparam>
        /// <param name="service"></param>
        /// <param name="columnSet">Columns to retrieve</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <param name="columnNameAndValuePairs">List of pairs that look like this:
        /// (string name of the column, value of the column) ie. "name","John Doe" goes to entity.name = "John Doe"
        /// </param>
        /// <returns></returns>
        public static async Task<T?> GetFirstOrDefaultAsync<T>(this IOrganizationServiceAsync2 service, ColumnSet columnSet,
                CancellationToken token, params object[] columnNameAndValuePairs) where T : Entity
        {
            var settings = new QuerySettings<T>
            {
                Columns = columnSet,
                First = true
            };
            return (await service.GetEntitiesAsync<T>(settings.CreateExpression(columnNameAndValuePairs), token)).FirstOrDefault();
        }

        /// <summary>
        /// Retrieves the first Active entity (with the given subset of columns only) 
        /// where the columnNameAndValue Pairs match
        /// </summary>
        /// <typeparam name="T">An early bound Entity Type</typeparam>
        /// <param name="service"></param>
        /// <param name="columnSet">Columns to retrieve</param>
        /// <param name="columnNameAndValuePairs">List of pairs that look like this:
        /// (string name of the column, value of the column) ie. "name","John Doe" goes to entity.name = "John Doe"
        /// </param>
        /// <returns></returns>
        public static Task<T?> GetFirstOrDefaultAsync<T>(this IOrganizationServiceAsync2 service, ColumnSet columnSet,
                params object[] columnNameAndValuePairs) where T : Entity
        {
            return service.GetFirstOrDefaultAsync<T>(columnSet, default, columnNameAndValuePairs);
        }

        #endregion GetFirstOrDefaultAsync<T>

        #region Equal Only Extensions

        // These Extension Methods only makes sense for equality, since they set the values if not found...
        /// <summary>
        /// Retrieves the entity from CRM where the columnNameAndValuePairs match.  If it doesn't exist, it creates it
        /// populating the entity with the columnNameAndValuePairs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="service"></param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <param name="columnNameAndValuePairs">List of pairs that look like this:
        /// (string name of the column, value of the column) ie. "name","John Doe" goes to entity.name = "John Doe"
        /// </param>
        /// <returns></returns>
        public static Task<T> GetOrCreateEntityAsync<T>(this IOrganizationServiceAsync2 service, CancellationToken token, params object[] columnNameAndValuePairs)
                where T : Entity, new()
        {
            return service.GetOrCreateEntityAsync<T>(SolutionCheckerAvoider.CreateColumnSetWithAllColumns(), token, columnNameAndValuePairs);
        }

        // These Extension Methods only makes sense for equality, since they set the values if not found...
        /// <summary>
        /// Retrieves the entity from CRM where the columnNameAndValuePairs match.  If it doesn't exist, it creates it
        /// populating the entity with the columnNameAndValuePairs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="service"></param>
        /// <param name="columnNameAndValuePairs">List of pairs that look like this:
        /// (string name of the column, value of the column) ie. "name","John Doe" goes to entity.name = "John Doe"
        /// </param>
        /// <returns></returns>
        public static Task<T> GetOrCreateEntityAsync<T>(this IOrganizationServiceAsync2 service, params object[] columnNameAndValuePairs)
                where T : Entity, new()
        {
            return service.GetOrCreateEntityAsync<T>(default(CancellationToken), columnNameAndValuePairs);
        }

        /// <summary>
        /// Retrieves the entity from CRM where the columnNameAndValuePairs match.  If it doesn't exist, it creates it
        /// populating the entity with the columnNameAndValuePairs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="service"></param>
        /// <param name="anonymousTypeInitializer">An Anonymous Type Initializer where the properties of the anonymous
        /// type are the column names to add</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <param name="columnNameAndValuePairs">List of pairs that look like this:
        /// (string name of the column, value of the column) ie. "name","John Doe" goes to entity.name = "John Doe"
        /// </param>
        /// <returns></returns>
        public static Task<T> GetOrCreateEntityAsync<T>(this IOrganizationServiceAsync2 service,
                Expression<Func<T, object>> anonymousTypeInitializer, CancellationToken token, params object[] columnNameAndValuePairs)
            where T : Entity, new()
        {
            var columnSet = new ColumnSet(anonymousTypeInitializer.GetAttributeNamesArray());
            return service.GetOrCreateEntityAsync<T>(columnSet, token, columnNameAndValuePairs);
        }

        /// <summary>
        /// Retrieves the entity from CRM where the columnNameAndValuePairs match.  If it doesn't exist, it creates it
        /// populating the entity with the columnNameAndValuePairs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="service"></param>
        /// <param name="anonymousTypeInitializer">An Anonymous Type Initializer where the properties of the anonymous
        /// type are the column names to add</param>
        /// <param name="columnNameAndValuePairs">List of pairs that look like this:
        /// (string name of the column, value of the column) ie. "name","John Doe" goes to entity.name = "John Doe"
        /// </param>
        /// <returns></returns>
        public static Task<T> GetOrCreateEntityAsync<T>(this IOrganizationServiceAsync2 service,
                Expression<Func<T, object>> anonymousTypeInitializer, params object[] columnNameAndValuePairs)
            where T : Entity, new()
        {
            return service.GetOrCreateEntityAsync(anonymousTypeInitializer, default, columnNameAndValuePairs);
        }

        /// <summary>
        /// Retrieves the entity from CRM where the columnNameAndValuePairs match.  If it doesn't exist, it creates it
        /// populating the entity with the columnNameAndValuePairs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="service"></param>
        /// <param name="columnSet"></param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <param name="columnNameAndValuePairs">List of pairs that look like this:
        /// (string name of the column, value of the column) ie. "name","John Doe" goes to entity.name = "John Doe"
        /// </param>
        /// <returns></returns>
        public static async Task<T> GetOrCreateEntityAsync<T>(this IOrganizationServiceAsync2 service, ColumnSet columnSet, CancellationToken token,
            params object[] columnNameAndValuePairs) where T : Entity, new()
        {
            int length = columnNameAndValuePairs.Length;
            if (length % 2 != 0)
            {
                throw new ArgumentException("Each Column Name must have a value after it.  Invalid " +
                    "columnNameAndValuePairs length");
            }

            var entity = new T();
            for (int i = 0; i < length; i += 2)
            {
                Type valueType = columnNameAndValuePairs[i + 1].GetType();
                entity.Attributes[columnNameAndValuePairs[i] as string] = columnNameAndValuePairs[i + 1];

                if (valueType == typeof(OptionSetValue))
                {
                    // The Get Methods need OptionSetValues to be ints, but the create method needs option set 
                    columnNameAndValuePairs[i + 1] = ((OptionSetValue)columnNameAndValuePairs[i + 1]).Value;
                }

                if (valueType == typeof(EntityReference))
                {
                    // The Get Methods need EntityReference to be Guids, but the create method needs EntityReference 
                    columnNameAndValuePairs[i + 1] = ((EntityReference)columnNameAndValuePairs[i + 1]).Id;
                }
            }

            var result = await service.GetFirstOrDefaultAsync<T>(columnSet, token, columnNameAndValuePairs);
            if (result == null)
            {
                entity.Id = await service.CreateAsync(entity, token);
                result = entity;
            }
            return result;
        }

        /// <summary>
        /// Retrieves the entity from CRM where the columnNameAndValuePairs match.  If it doesn't exist, it creates it
        /// populating the entity with the columnNameAndValuePairs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="service"></param>
        /// <param name="columnSet"></param>
        /// <param name="columnNameAndValuePairs">List of pairs that look like this:
        /// (string name of the column, value of the column) ie. "name","John Doe" goes to entity.name = "John Doe"
        /// </param>
        /// <returns></returns>
        public static Task<T> GetOrCreateEntityAsync<T>(this IOrganizationServiceAsync2 service, ColumnSet columnSet,
            params object[] columnNameAndValuePairs) where T : Entity, new()
        {
            return service.GetOrCreateEntityAsync<T>(columnSet, default, columnNameAndValuePairs);
        }

        #endregion Equal Only Extensions
    }
}
#endif