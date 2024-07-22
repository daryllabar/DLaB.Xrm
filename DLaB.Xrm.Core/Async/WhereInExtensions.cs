#if DLAB_ASYNC
#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
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
        #region GetAllEntitiesInAsync

        /// <summary>
        /// Gets all Active Entities where the values are in the columnName
        /// </summary>
        /// <typeparam name="T">Type of Entity List to return.</typeparam>
        /// <param name="service"></param>
        /// <param name="columnName">The name of the column to perform the in against.</param>
        /// <param name="values">The list of values to search for being in the column name.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns></returns>
        public static IAsyncEnumerable<T> GetAllEntitiesInAsync<T>(this IOrganizationServiceAsync2 service,
                string columnName, IEnumerable values, CancellationToken token = default) where T : Entity
        {
            return RetrieveAllEntitiesAsync<T>.GetAllEntities(service, QueryExpressionFactory.CreateIn<T>(columnName, values), token: token);
        }

        /// <summary>
        /// Gets all Active Entities where the values are in the columnName
        /// </summary>
        /// <typeparam name="T">Type of Entity List to return.</typeparam>
        /// <param name="service"></param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <param name="columnName">The name of the column to perform the in against.</param>
        /// <param name="values">The list of values to search for being in the column name.</param>
        /// <returns></returns>
        public static IAsyncEnumerable<T> GetAllEntitiesInAsync<T>(this IOrganizationServiceAsync2 service, CancellationToken token,
                string columnName, params object[] values) where T : Entity
        {
            return RetrieveAllEntitiesAsync<T>.GetAllEntities(service, QueryExpressionFactory.CreateIn<T>(columnName, values), token: token);
        }

        /// <summary>
        /// Gets all Active Entities where the values are in the columnName
        /// </summary>
        /// <typeparam name="T">Type of Entity List to return.</typeparam>
        /// <param name="service"></param>
        /// <param name="columnName">The name of the column to perform the in against.</param>
        /// <param name="values">The list of values to search for being in the column name.</param>
        /// <returns></returns>
        public static IAsyncEnumerable<T> GetAllEntitiesInAsync<T>(this IOrganizationServiceAsync2 service,
                string columnName, params object[] values) where T : Entity
        {
            return service.GetAllEntitiesInAsync<T>(default(CancellationToken), columnName, values);
        }

        /// <summary>
        /// Gets all Active Entities (with the given subset of columns only) 
        /// where the values are in the columnName
        /// </summary>
        /// <typeparam name="T">Type of Entity List to return.</typeparam>
        /// <param name="service"></param>
        /// <param name="anonymousTypeInitializer">An Anonymous Type Initializer where the properties of the anonymous
        /// type are the column names to add.</param>
        /// <param name="columnName">The name of the column to perform the in against.</param>
        /// <param name="values">The list of values to search for being in the column name.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns></returns>
        public static IAsyncEnumerable<T> GetAllEntitiesInAsync<T>(this IOrganizationServiceAsync2 service, 
                Expression<Func<T, object>> anonymousTypeInitializer, string columnName, IEnumerable values, CancellationToken token)
            where T : Entity
        {
            var columnSet = new ColumnSet(anonymousTypeInitializer.GetAttributeNamesArray());
            return service.GetAllEntitiesInAsync<T>(columnSet, columnName, values, token);
        }

        /// <summary>
        /// Gets all Active Entities (with the given subset of columns only) 
        /// where the values are in the columnName
        /// </summary>
        /// <typeparam name="T">Type of Entity List to return.</typeparam>
        /// <param name="service"></param>
        /// <param name="columnSet">Columns to Return.</param>
        /// <param name="columnName">The name of the column to perform the in against.</param>
        /// <param name="values">The list of values to search for being in the column name.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns></returns>
        public static IAsyncEnumerable<T> GetAllEntitiesInAsync<T>(this IOrganizationServiceAsync2 service, ColumnSet columnSet,
                string columnName, IEnumerable values, CancellationToken token = default) where T : Entity
        {
            return RetrieveAllEntitiesAsync<T>.GetAllEntities(service, QueryExpressionFactory.CreateIn<T>(columnSet, columnName, values), token: token);
        }

        /// <summary>
        /// Gets all Active Entities (with the given subset of columns only) 
        /// where the values are in the columnName
        /// </summary>
        /// <typeparam name="T">Type of Entity List to return.</typeparam>
        /// <param name="service"></param>
        /// <param name="anonymousTypeInitializer">An Anonymous Type Initializer where the properties of the anonymous
        /// type are the column names to add.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <param name="columnName">The name of the column to perform the in against.</param>
        /// <param name="values">The list of values to search for being in the column name.</param>
        /// <returns></returns>
        public static IAsyncEnumerable<T> GetAllEntitiesInAsync<T>(this IOrganizationServiceAsync2 service, 
                Expression<Func<T, object>> anonymousTypeInitializer, CancellationToken token, string columnName, params object[] values)
            where T : Entity
        {
            var columnSet = new ColumnSet(anonymousTypeInitializer.GetAttributeNamesArray());
            return service.GetAllEntitiesInAsync<T>(columnSet, token, columnName, values);
        }

        /// <summary>
        /// Gets all Active Entities (with the given subset of columns only) 
        /// where the values are in the columnName
        /// </summary>
        /// <typeparam name="T">Type of Entity List to return.</typeparam>
        /// <param name="service"></param>
        /// <param name="anonymousTypeInitializer">An Anonymous Type Initializer where the properties of the anonymous
        /// type are the column names to add.</param>
        /// <param name="columnName">The name of the column to perform the in against.</param>
        /// <param name="values">The list of values to search for being in the column name.</param>
        /// <returns></returns>
        public static IAsyncEnumerable<T> GetAllEntitiesInAsync<T>(this IOrganizationServiceAsync2 service, 
                Expression<Func<T, object>> anonymousTypeInitializer, string columnName, params object[] values)
            where T : Entity
        {
            return service.GetAllEntitiesInAsync<T>(anonymousTypeInitializer, default, columnName, values);
        }

        /// <summary>
        /// Gets all Active Entities (with the given subset of columns only) 
        /// where the values are in the columnName
        /// </summary>
        /// <typeparam name="T">Type of Entity List to return.</typeparam>
        /// <param name="service"></param>
        /// <param name="columnSet">Columns to Return.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <param name="columnName">The name of the column to perform the in against.</param>
        /// <param name="values">The list of values to search for being in the column name.</param>
        /// <returns></returns>
        public static IAsyncEnumerable<T> GetAllEntitiesInAsync<T>(this IOrganizationServiceAsync2 service, ColumnSet columnSet, CancellationToken token,
                string columnName, params object[] values) where T : Entity
        {
            return RetrieveAllEntitiesAsync<T>.GetAllEntities(service, QueryExpressionFactory.CreateIn<T>(columnSet, columnName, values), token: token);
        }

        /// <summary>
        /// Gets all Active Entities (with the given subset of columns only) 
        /// where the values are in the columnName
        /// </summary>
        /// <typeparam name="T">Type of Entity List to return.</typeparam>
        /// <param name="service"></param>
        /// <param name="columnSet">Columns to Return.</param>
        /// <param name="columnName">The name of the column to perform the in against.</param>
        /// <param name="values">The list of values to search for being in the column name.</param>
        /// <returns></returns>
        public static IAsyncEnumerable<T> GetAllEntitiesInAsync<T>(this IOrganizationServiceAsync2 service, ColumnSet columnSet,
                string columnName, params object[] values) where T : Entity
        {
            return service.GetAllEntitiesInAsync<T>(columnSet, default, columnName, values);
        }

        #endregion GetAllEntitiesInAsync

        #region GetEntitiesInAsync

        /// <summary>
        /// Gets first 5000 Active Entities where the values are in the columnName
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logicalName">The LogicalName of the Entity to query.</param>
        /// <param name="columnName">The name of the column to perform the in against.</param>
        /// <param name="values">The list of values to search for being in the column name.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns></returns>
        public static async Task<List<Entity>> GetEntitiesInAsync(this IOrganizationServiceAsync2 service, string logicalName,
                string columnName, IEnumerable values, CancellationToken token = default) 
        {
            var settings = new LateBoundQuerySettings(logicalName);
            return ( await service.RetrieveMultipleAsync(settings.CreateInExpression(columnName, values), token)).Entities.ToList();
        }

        /// <summary>
        /// Gets first 5000 Active Entities where the values are in the columnName
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logicalName">The LogicalName of the Entity to query.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <param name="columnName">The name of the column to perform the in against.</param>
        /// <param name="values">The list of values to search for being in the column name.</param>
        /// <returns></returns>
        public static async Task<List<Entity>> GetEntitiesInAsync(this IOrganizationServiceAsync2 service, string logicalName,
                CancellationToken token, string columnName, params object[] values) 
        {
            var settings = new LateBoundQuerySettings(logicalName);
            return (await service.RetrieveMultipleAsync(settings.CreateInExpression(columnName, values), token)).Entities.ToList();
        }

        /// <summary>
        /// Gets first 5000 Active Entities where the values are in the columnName
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logicalName">The LogicalName of the Entity to query.</param>
        /// <param name="columnName">The name of the column to perform the in against.</param>
        /// <param name="values">The list of values to search for being in the column name.</param>
        /// <returns></returns>
        public static Task<List<Entity>> GetEntitiesInAsync(this IOrganizationServiceAsync2 service, string logicalName,
                string columnName, params object[] values)
        {
            return service.GetEntitiesInAsync(logicalName, default(CancellationToken), columnName, values);
        }

        /// <summary>
        /// Gets first 5000 Active Entities (with the given subset of columns only) 
        /// where the values are in the columnName
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logicalName">The LogicalName of the Entity to query.</param>
        /// <param name="columnSet">Columns to Return.</param>
        /// <param name="columnName">The name of the column to perform the in against.</param>
        /// <param name="values">The list of values to search for being in the column name.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns></returns>
        public static async Task<List<Entity>> GetEntitiesInAsync(this IOrganizationServiceAsync2 service, string logicalName,
            ColumnSet columnSet, string columnName, IEnumerable values, CancellationToken token = default) 
        {
            var settings = new LateBoundQuerySettings(logicalName)
            {
                Columns = columnSet,
            };
            return (await service.RetrieveMultipleAsync(settings.CreateInExpression(columnName, values), token)).Entities.ToList();
        }

        /// <summary>
        /// Gets first 5000 Active Entities (with the given subset of columns only) 
        /// where the values are in the columnName
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logicalName">The LogicalName of the Entity to query.</param>
        /// <param name="columnSet">Columns to Return.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <param name="columnName">The name of the column to perform the in against.</param>
        /// <param name="values">The list of values to search for being in the column name.</param>
        /// <returns></returns>
        public static async Task<List<Entity>> GetEntitiesInAsync(this IOrganizationServiceAsync2 service, string logicalName,
                ColumnSet? columnSet, CancellationToken token, string columnName, params object[] values) 
        {
            var settings = new LateBoundQuerySettings(logicalName)
            {
                Columns = columnSet,
            };
            return (await service.RetrieveMultipleAsync(settings.CreateInExpression(columnName, values), token)).Entities.ToList();
        }

        /// <summary>
        /// Gets first 5000 Active Entities (with the given subset of columns only) 
        /// where the values are in the columnName
        /// </summary>
        /// <param name="service"></param>
        /// <param name="logicalName">The LogicalName of the Entity to query.</param>
        /// <param name="columnSet">Columns to Return.</param>
        /// <param name="columnName">The name of the column to perform the in against.</param>
        /// <param name="values">The list of values to search for being in the column name.</param>
        /// <returns></returns>
        public static Task<List<Entity>> GetEntitiesInAsync(this IOrganizationServiceAsync2 service, string logicalName,
                ColumnSet? columnSet, string columnName, params object[] values) 
        {
            return service.GetEntitiesInAsync(logicalName, columnSet, default, columnName, values);
        }

        #endregion GetEntitiesInAsync

        #region GetEntitiesInAsync<T>

        /// <summary>
        /// Gets first 5000 Active Entities where the values are in the columnName
        /// </summary>
        /// <typeparam name="T">Type of Entity List to return.</typeparam>
        /// <param name="service"></param>
        /// <param name="columnName">The name of the column to perform the in against.</param>
        /// <param name="values">The list of values to search for being in the column name.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns></returns>
        public static Task<List<T>> GetEntitiesInAsync<T>(this IOrganizationServiceAsync2 service,
                string columnName, IEnumerable values, CancellationToken token = default) where T : Entity
        {
            return service.GetEntitiesAsync(QueryExpressionFactory.CreateIn<T>(columnName, values), token);
        }

        /// <summary>
        /// Gets first 5000 Active Entities where the values are in the columnName
        /// </summary>
        /// <typeparam name="T">Type of Entity List to return.</typeparam>
        /// <param name="service"></param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <param name="columnName">The name of the column to perform the in against.</param>
        /// <param name="values">The list of values to search for being in the column name.</param>
        /// <returns></returns>
        public static Task<List<T>> GetEntitiesInAsync<T>(this IOrganizationServiceAsync2 service, CancellationToken token,
                string columnName, params object[] values) where T : Entity
        {
            return service.GetEntitiesAsync(QueryExpressionFactory.CreateIn<T>(columnName, values), token);
        }

        /// <summary>
        /// Gets first 5000 Active Entities where the values are in the columnName
        /// </summary>
        /// <typeparam name="T">Type of Entity List to return.</typeparam>
        /// <param name="service"></param>
        /// <param name="columnName">The name of the column to perform the in against.</param>
        /// <param name="values">The list of values to search for being in the column name.</param>
        /// <returns></returns>
        public static Task<List<T>> GetEntitiesInAsync<T>(this IOrganizationServiceAsync2 service, 
                string columnName, params object[] values) where T : Entity
        {
            return service.GetEntitiesInAsync<T>(default(CancellationToken), columnName, values);
        }

        /// <summary>
        /// Gets first 5000 Active Entities (with the given subset of columns only) 
        /// where the values are in the columnName
        /// </summary>
        /// <typeparam name="T">Type of Entity List to return.</typeparam>
        /// <param name="service"></param>
        /// <param name="anonymousTypeInitializer">An Anonymous Type Initializer where the properties of the anonymous
        /// type are the column names to add.</param>
        /// <param name="columnName">The name of the column to perform the in against.</param>
        /// <param name="values">The list of values to search for being in the column name.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns></returns>
        public static Task<List<T>> GetEntitiesInAsync<T>(this IOrganizationServiceAsync2 service, Expression<Func<T, object>> anonymousTypeInitializer,
                string columnName, IEnumerable values, CancellationToken token = default)
            where T : Entity
        {
            var columnSet = new ColumnSet(anonymousTypeInitializer.GetAttributeNamesArray());
            return service.GetEntitiesInAsync<T>(columnSet, token, columnName, values);
        }

        /// <summary>
        /// Gets first 5000 Active Entities (with the given subset of columns only) 
        /// where the values are in the columnName
        /// </summary>
        /// <typeparam name="T">Type of Entity List to return.</typeparam>
        /// <param name="service"></param>
        /// <param name="columnSet">Columns to Return.</param>
        /// <param name="columnName">The name of the column to perform the in against.</param>
        /// <param name="values">The list of values to search for being in the column name.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns></returns>
        public static Task<List<T>> GetEntitiesInAsync<T>(this IOrganizationServiceAsync2 service, ColumnSet columnSet,
                string columnName, IEnumerable values, CancellationToken token = default) where T : Entity
        {
            return service.GetEntitiesAsync(QueryExpressionFactory.CreateIn<T>(columnSet, columnName, values), token);
        }

        /// <summary>
        /// Gets first 5000 Active Entities (with the given subset of columns only) 
        /// where the values are in the columnName
        /// </summary>
        /// <typeparam name="T">Type of Entity List to return.</typeparam>
        /// <param name="service"></param>
        /// <param name="anonymousTypeInitializer">An Anonymous Type Initializer where the properties of the anonymous
        /// type are the column names to add.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <param name="columnName">The name of the column to perform the in against.</param>
        /// <param name="values">The list of values to search for being in the column name.</param>
        /// <returns></returns>
        public static Task<List<T>> GetEntitiesInAsync<T>(this IOrganizationServiceAsync2 service, 
            Expression<Func<T, object>> anonymousTypeInitializer, CancellationToken token, string columnName, params object[] values)
            where T : Entity
        {
            var columnSet = new ColumnSet(anonymousTypeInitializer.GetAttributeNamesArray());
            return service.GetEntitiesInAsync<T>(columnSet, token, columnName, values);
        }

        /// <summary>
        /// Gets first 5000 Active Entities (with the given subset of columns only) 
        /// where the values are in the columnName
        /// </summary>
        /// <typeparam name="T">Type of Entity List to return.</typeparam>
        /// <param name="service"></param>
        /// <param name="anonymousTypeInitializer">An Anonymous Type Initializer where the properties of the anonymous
        /// type are the column names to add.</param>
        /// <param name="columnName">The name of the column to perform the in against.</param>
        /// <param name="values">The list of values to search for being in the column name.</param>
        /// <returns></returns>
        public static Task<List<T>> GetEntitiesInAsync<T>(this IOrganizationServiceAsync2 service, 
                Expression<Func<T, object>> anonymousTypeInitializer, string columnName, params object[] values)
            where T : Entity
        {
            return service.GetEntitiesInAsync(anonymousTypeInitializer, default, columnName, values);
        }

        /// <summary>
        /// Gets first 5000 Active Entities (with the given subset of columns only) 
        /// where the values are in the columnName
        /// </summary>
        /// <typeparam name="T">Type of Entity List to return.</typeparam>
        /// <param name="service"></param>
        /// <param name="columnSet">Columns to Return.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <param name="columnName">The name of the column to perform the in against.</param>
        /// <param name="values">The list of values to search for being in the column name.</param>
        /// <returns></returns>
        public static Task<List<T>> GetEntitiesInAsync<T>(this IOrganizationServiceAsync2 service, ColumnSet columnSet, CancellationToken token,
            string columnName, params object[] values) where T : Entity
        {
            return service.GetEntitiesAsync(QueryExpressionFactory.CreateIn<T>(columnSet, columnName, values), token);
        }

        /// <summary>
        /// Gets first 5000 Active Entities (with the given subset of columns only) 
        /// where the values are in the columnName
        /// </summary>
        /// <typeparam name="T">Type of Entity List to return.</typeparam>
        /// <param name="service"></param>
        /// <param name="columnSet">Columns to Return.</param>
        /// <param name="columnName">The name of the column to perform the in against.</param>
        /// <param name="values">The list of values to search for being in the column name.</param>
        /// <returns></returns>
        public static Task<List<T>> GetEntitiesInAsync<T>(this IOrganizationServiceAsync2 service, ColumnSet columnSet,
                string columnName, params object[] values) where T : Entity
        {
            return service.GetEntitiesInAsync<T>(columnSet, default, columnName, values);
        }

        #endregion GetEntitiesInAsync<T>

        #region GetFirstOrDefaultInAsync

        /// <summary>
        /// Retrieves the first active entity (with the given subset of columns only) 
        /// where the values are in the columnName
        /// </summary>
        /// <typeparam name="T">An early bound Entity Type.</typeparam>
        /// <param name="service"></param>
        /// <param name="columnName">The name of the column to perform the in against.</param>
        /// <param name="values">The list of values to search for being in the column name.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns></returns>
        public static async Task<T?> GetFirstOrDefaultInAsync<T>(this IOrganizationServiceAsync2 service,
                string columnName, IEnumerable values, CancellationToken token = default) where T : Entity
        {
            var settings = new QuerySettings<T> { First = true };
            return (await service.GetEntitiesAsync<T>(settings.CreateInExpression(columnName, values), token)).FirstOrDefault();
        }

        /// <summary>
        /// Retrieves the first active entity (with the given subset of columns only) 
        /// where the values are in the columnName
        /// </summary>
        /// <typeparam name="T">An early bound Entity Type.</typeparam>
        /// <param name="service"></param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <param name="columnName">The name of the column to perform the in against.</param>
        /// <param name="values">The list of values to search for being in the column name.</param>
        /// <returns></returns>
        public static async Task<T?> GetFirstOrDefaultInAsync<T>(this IOrganizationServiceAsync2 service, CancellationToken token,
                string columnName, params object[] values) where T : Entity
        {
            var settings = new QuerySettings<T> { First = true };
            return (await service.GetEntitiesAsync<T>(settings.CreateInExpression(columnName, values), token)).FirstOrDefault();
        }

        /// <summary>
        /// Retrieves the first active entity (with the given subset of columns only) 
        /// where the values are in the columnName
        /// </summary>
        /// <typeparam name="T">An early bound Entity Type.</typeparam>
        /// <param name="service"></param>
        /// <param name="columnName">The name of the column to perform the in against.</param>
        /// <param name="values">The list of values to search for being in the column name.</param>
        /// <returns></returns>
        public static Task<T?> GetFirstOrDefaultInAsync<T>(this IOrganizationServiceAsync2 service,
                string columnName, params object[] values) where T : Entity
        {
            var settings = new QuerySettings<T> { First = true };
            return service.GetFirstOrDefaultInAsync<T>(default(CancellationToken), columnName, values);
        }

        /// <summary>
        /// Retrieves the first active entity (with the given subset of columns only) 
        /// where the values are in the columnName
        /// </summary>
        /// <typeparam name="T">An early bound Entity Type.</typeparam>
        /// <param name="service"></param>
        /// <param name="anonymousTypeInitializer">An Anonymous Type Initializer where the properties of the anonymous
        /// type are the column names to add.</param>
        /// <param name="columnName">The name of the column to perform the in against.</param>
        /// <param name="values">The list of values to search for being in the column name.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns></returns>
        public static Task<T?> GetFirstOrDefaultInAsync<T>(this IOrganizationServiceAsync2 service, 
                Expression<Func<T, object>> anonymousTypeInitializer, string columnName, IEnumerable values, CancellationToken token = default)
            where T : Entity
        {
            var columnSet = new ColumnSet(anonymousTypeInitializer.GetAttributeNamesArray());
            return service.GetFirstOrDefaultInAsync<T>(columnSet, columnName, values, token);
        }

        /// <summary>
        /// Retrieves the first active entity (with the given subset of columns only) 
        /// where the values are in the columnName
        /// </summary>
        /// <typeparam name="T">An early bound Entity Type.</typeparam>
        /// <param name="service"></param>
        /// <param name="columnSet">Columns to Return.</param>
        /// <param name="columnName">The name of the column to perform the in against.</param>
        /// <param name="values">The list of values to search for being in the column name.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns></returns>
        public static async Task<T?> GetFirstOrDefaultInAsync<T>(this IOrganizationServiceAsync2 service, ColumnSet columnSet,
                string columnName, IEnumerable values, CancellationToken token = default) where T : Entity
        {
            var settings = new QuerySettings<T> { Columns = columnSet, First = true };
            return (await service.GetEntitiesAsync<T>(settings.CreateInExpression(columnName, values), token)).FirstOrDefault();
        }

        /// <summary>
        /// Retrieves the first active entity (with the given subset of columns only) 
        /// where the values are in the columnName
        /// </summary>
        /// <typeparam name="T">An early bound Entity Type</typeparam>
        /// <param name="service"></param>
        /// <param name="anonymousTypeInitializer">An Anonymous Type Initializer where the properties of the anonymous
        /// type are the column names to add.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <param name="columnName">The name of the column to perform the in against.</param>
        /// <param name="values">The list of values to search for being in the column name.</param>
        /// <returns></returns>
        public static Task<T?> GetFirstOrDefaultInAsync<T>(this IOrganizationServiceAsync2 service, 
                Expression<Func<T, object>> anonymousTypeInitializer, CancellationToken token, string columnName, params object[] values)
            where T : Entity
        {
            var columnSet = new ColumnSet(anonymousTypeInitializer.GetAttributeNamesArray());
            return service.GetFirstOrDefaultInAsync<T>(columnSet, token, columnName, values);
        }

        /// <summary>
        /// Retrieves the first active entity (with the given subset of columns only) 
        /// where the values are in the columnName
        /// </summary>
        /// <typeparam name="T">An early bound Entity Type</typeparam>
        /// <param name="service"></param>
        /// <param name="anonymousTypeInitializer">An Anonymous Type Initializer where the properties of the anonymous
        /// type are the column names to add.</param>
        /// <param name="columnName">The name of the column to perform the in against.</param>
        /// <param name="values">The list of values to search for being in the column name.</param>
        /// <returns></returns>
        public static Task<T?> GetFirstOrDefaultInAsync<T>(this IOrganizationServiceAsync2 service, 
                Expression<Func<T, object>> anonymousTypeInitializer, string columnName, params object[] values)
            where T : Entity
        {
            return service.GetFirstOrDefaultInAsync(anonymousTypeInitializer, default, columnName, values);
        }

        /// <summary>
        /// Retrieves the first active entity (with the given subset of columns only) 
        /// where the values are in the columnName
        /// </summary>
        /// <typeparam name="T">An early bound Entity Type</typeparam>
        /// <param name="service"></param>
        /// <param name="columnSet">Columns to Return.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <param name="columnName">The name of the column to perform the in against.</param>
        /// <param name="values">The list of values to search for being in the column name.</param>
        /// <returns></returns>
        public static async Task<T?> GetFirstOrDefaultInAsync<T>(this IOrganizationServiceAsync2 service, ColumnSet columnSet,
                CancellationToken token, string columnName, params object[] values) where T : Entity
        {
            var settings = new QuerySettings<T> { Columns = columnSet, First = true };
            return (await service.GetEntitiesAsync<T>(settings.CreateInExpression(columnName, values), token)).FirstOrDefault();
        }

        /// <summary>
        /// Retrieves the first active entity (with the given subset of columns only) 
        /// where the values are in the columnName
        /// </summary>
        /// <typeparam name="T">An early bound Entity Type</typeparam>
        /// <param name="service"></param>
        /// <param name="columnSet">Columns to Return.</param>
        /// <param name="columnName">The name of the column to perform the in against.</param>
        /// <param name="values">The list of values to search for being in the column name.</param>
        /// <returns></returns>
        public static Task<T?> GetFirstOrDefaultInAsync<T>(this IOrganizationServiceAsync2 service, ColumnSet columnSet,
                string columnName, params object[] values) where T : Entity
        {
            return service.GetFirstOrDefaultInAsync<T>(columnSet, default, columnName, values);
        }

        #endregion GetFirstOrDefaultInAsync
    }
}
#endif