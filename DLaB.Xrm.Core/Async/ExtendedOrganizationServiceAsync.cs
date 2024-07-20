#if DLAB_ASYNC
#nullable enable
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Source.DLaB.Xrm;
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
    /// <summary>
    /// An IOrganizationService Wrapper that utilizes ExtendedOrganizationServiceSettings and an ITracingService to potentially log every request, timing it as well as parsing the queries into Sql
    /// </summary>
#if !DLAB_XRM_DEBUG
    [DebuggerNonUserCode]
#endif
    public class ExtendedOrganizationServiceAsync :  IOrganizationServiceAsync2
    {
        /// <summary>
        /// The Settings.
        /// </summary>
        protected ExtendedOrganizationServiceAsyncSettings Settings { get; }
        /// <summary>
        /// The Service.
        /// </summary>
        protected IOrganizationServiceAsync2 Service { get; }
        private readonly ITracingService? _tracingService;
        private readonly ILogger? _logger;

        /// <summary>
        /// Constructor for determining if statements are timed and or logged.
        /// </summary>
        /// <param name="service">IOrganizationServiceAsync2 to wrap.</param>
        /// <param name="tracingService">Tracing Service Required</param>
        /// <param name="settings">Settings</param>
        public ExtendedOrganizationServiceAsync(IOrganizationServiceAsync2 service, ITracingService tracingService, ExtendedOrganizationServiceAsyncSettings? settings = null)
        {
            Service = service;
            Settings = settings ?? new ExtendedOrganizationServiceAsyncSettings();
            _tracingService = tracingService ?? throw new ArgumentNullException(nameof(tracingService));
        }

        /// <summary>
        /// Constructor for determining if statements are timed and or logged.
        /// </summary>
        /// <param name="service">IOrganizationService to wrap.</param>
        /// <param name="logger">Logger Required</param>
        /// <param name="settings">Settings</param>
        public ExtendedOrganizationServiceAsync(IOrganizationServiceAsync2 service, ILogger logger, ExtendedOrganizationServiceAsyncSettings? settings = null)
        {
            Service = service;
            Settings = settings ?? new ExtendedOrganizationServiceAsyncSettings();
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region Implementation of IOrganizationService

        /// <inheritdoc />
        public Guid Create(Entity entity)
        {
            var message = Settings.LogDetailedRequests
                ? $"Create Request for {entity.LogicalName} with Id {entity.Id} and Attributes {entity.ToStringAttributes()}"
                : "Create Request";
            if (Settings.TimeRequests)
            {
                var timer = new Stopwatch();
                try
                {
                    TraceStart(message);
                    timer.Start();
                    return Service.Create(entity);
                }
                finally
                {
                    TraceEnd(timer);
                }
            }

            TraceExecute(message);
            return Service.Create(entity);
        }

        /// <inheritdoc />
        public Entity Retrieve(string entityName, Guid id, ColumnSet columnSet)
        {
            var message = Settings.LogDetailedRequests
                ? $"Retrieve Request for {entityName} with id {id} and Columns {string.Join(", ", columnSet.Columns)}"
                : "Retrieve Request";

            if (Settings.TimeRequests)
            {
                var timer = new Stopwatch();
                try
                {
                    TraceStart(message);
                    timer.Start();
                    return Service.Retrieve(entityName, id, columnSet);
                }
                finally
                {
                    TraceEnd(timer);
                }
            }

            TraceExecute(message);
            return Service.Retrieve(entityName, id, columnSet);
        }

        /// <inheritdoc />
        public void Update(Entity entity)
        {
            var message = Settings.LogDetailedRequests
                ? $"Update Request for {entity.LogicalName} with Id {entity.Id} and Attributes {entity.ToStringAttributes()}"
                : "Update Request";
            if (Settings.TimeRequests)
            {
                var timer = new Stopwatch();
                try
                {
                    TraceStart(message);
                    timer.Start();
                    Service.Update(entity);
                    return;
                }
                finally
                {
                    TraceEnd(timer);
                }
            }

            TraceExecute(message);
            Service.Update(entity);
        }

        /// <inheritdoc />
        public void Delete(string entityName, Guid id)
        {
            var message = Settings.LogDetailedRequests
                ? $"Delete Request for {entityName} with Id {id}"
                : "Delete Request";
            if (Settings.TimeRequests)
            {
                var timer = new Stopwatch();
                try
                {
                    TraceStart(message);
                    timer.Start();
                    Service.Delete(entityName, id);
                    return;
                }
                finally
                {
                    TraceEnd(timer);
                }
            }

            TraceExecute(message);
            Service.Delete(entityName, id);
        }

        /// <inheritdoc />
        public OrganizationResponse Execute(OrganizationRequest request)
        {
            var message = Settings.LogDetailedRequests
                ? GetDetailedMessage(request)
                : $"Execute Request {request.RequestName}";

            if (Settings.TimeRequests)
            {
                var timer = new Stopwatch();
                try
                {
                    TraceStart(message);
                    timer.Start();
                    return Service.Execute(request);
                }
                finally
                {
                    TraceEnd(timer);
                }
            }

            TraceExecute(message);
            return Service.Execute(request);
        }

        /// <inheritdoc />
        public void Associate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities)
        {
            var message = Settings.LogDetailedRequests
                ? $"Associate Request for {entityName}, with Id {entityId}, Relationship {relationship.SchemaName}, and Related Entities {relatedEntities.Select(e => e.ToStringDebug()).ToCsv()}."
                : "Associate Request";
            if (Settings.TimeRequests)
            {
                var timer = new Stopwatch();
                try
                {
                    TraceStart(message);
                    timer.Start();
                    Service.Associate(entityName, entityId, relationship, relatedEntities);
                    return;
                }
                finally
                {
                    TraceEnd(timer);
                }
            }

            TraceExecute(message);
            Service.Associate(entityName, entityId, relationship, relatedEntities);
        }

        /// <inheritdoc />
        public void Disassociate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities)
        {
            var message = Settings.LogDetailedRequests
                ? $"Disassociate Request for {entityName}, with Id {entityId}, Relationship {relationship.SchemaName}, and Related Entities {relatedEntities.ToStringDebug(new StringDebugInfo(singleLine: true))}."
                : "Disassociate Request";
            if (Settings.TimeRequests)
            {
                var timer = new Stopwatch();
                try
                {
                    TraceStart(message);
                    timer.Start();
                    Service.Disassociate(entityName, entityId, relationship, relatedEntities);
                    return;
                }
                finally
                {
                    TraceEnd(timer);
                }
            }

            TraceExecute(message);
            Service.Disassociate(entityName, entityId, relationship, relatedEntities);
        }

        /// <inheritdoc />
        public EntityCollection RetrieveMultiple(QueryBase query)
        {
            var message = Settings.LogDetailedRequests
                ? "Retrieve Multiple Request " + GetDetailedMessage(query)
                : "Retrieve Multiple Request";

            if (Settings.TimeRequests)
            {
                var timer = new Stopwatch();
                try
                {
                    TraceStart(message);
                    timer.Start();
                    var timedResults = Service.RetrieveMultiple(query);
                    if (Settings.LogDetailedRequests)
                    {
                        Trace("Returned: " + timedResults.Entities.Count);
                    }
                    return timedResults;
                }
                finally
                {
                    TraceEnd(timer);
                }
            }

            TraceExecute(message);
            var results = Service.RetrieveMultiple(query);
            if (Settings.LogDetailedRequests)
            {
                Trace("Returned: " + results.Entities.Count);
            }
            return results;
        }

        #endregion

        #region Implementation of IOrganizationServiceAsync

        /// <inheritdoc />
        public Task<Guid> CreateAsync(Entity entity)
        {
            return CreateAsync(entity, default);
        }

        /// <inheritdoc />
        public Task<Entity> RetrieveAsync(string entityName, Guid id, ColumnSet columnSet)
        {
            return Service.RetrieveAsync(entityName, id, columnSet, default);
        }

        /// <inheritdoc />
        public Task UpdateAsync(Entity entity)
        {
            return UpdateAsync(entity, default);
        }

        /// <inheritdoc />
        public Task DeleteAsync(string entityName, Guid id)
        {
            return DeleteAsync(entityName, id, default);
        }

        /// <inheritdoc />
        public Task<OrganizationResponse> ExecuteAsync(OrganizationRequest request)
        {
            return ExecuteAsync(request, default);
        }

        /// <inheritdoc />
        public Task AssociateAsync(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities)
        {
            return AssociateAsync(entityName, entityId, relationship, relatedEntities, default);
        }

        /// <inheritdoc />
        public Task DisassociateAsync(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities)
        {
            return DisassociateAsync(entityName, entityId, relationship, relatedEntities, default);
        }

        /// <inheritdoc />
        public Task<EntityCollection> RetrieveMultipleAsync(QueryBase query)
        {
            return RetrieveMultipleAsync(query, default);
        }

        #endregion Implementation of IOrganizationServiceAsync

        #region Implementation of IOrganizationServiceAsync2

        /// <inheritdoc />
        public async Task<Guid> CreateAsync(Entity entity, CancellationToken cancellationToken)
        {
            var message = Settings.LogDetailedRequests
                ? $"Create Request for {entity.LogicalName} with Id {entity.Id} and Attributes {entity.ToStringAttributes()}"
                : "Create Request";
            if (Settings.TimeRequests)
            {
                var timer = new Stopwatch();
                try
                {
                    TraceStart(message);
                    timer.Start();
                    return await Service.CreateAsync(entity, cancellationToken);
                }
                finally
                {
                    TraceEnd(timer);
                }
            }

            TraceExecute(message);
            return await Service.CreateAsync(entity, cancellationToken);
        }

        public async Task<Entity> CreateAndReturnAsync(Entity entity, CancellationToken cancellationToken)
        {
            var message = Settings.LogDetailedRequests
                ? $"Create and Return Request for {entity.LogicalName} with Id {entity.Id} and Attributes {entity.ToStringAttributes()}"
                : "Create and Return Request";
            if (Settings.TimeRequests)
            {
                var timer = new Stopwatch();
                try
                {
                    TraceStart(message);
                    timer.Start();
                    return await Service.CreateAndReturnAsync(entity, cancellationToken);
                }
                finally
                {
                    TraceEnd(timer);
                }
            }

            TraceExecute(message);
            return await Service.CreateAndReturnAsync(entity, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<Entity> RetrieveAsync(string entityName, Guid id, ColumnSet columnSet, CancellationToken cancellationToken)
        {
            var message = Settings.LogDetailedRequests
                ? $"Retrieve Request for {entityName} with id {id} and Columns {string.Join(", ", columnSet.Columns)}"
                : "Retrieve Request";

            if (Settings.TimeRequests)
            {
                var timer = new Stopwatch();
                try
                {
                    TraceStart(message);
                    timer.Start();
                    return await Service.RetrieveAsync(entityName, id, columnSet, cancellationToken);
                }
                finally
                {
                    TraceEnd(timer);
                }
            }

            TraceExecute(message);
            return await Service.RetrieveAsync(entityName, id, columnSet, cancellationToken);
        }

        /// <inheritdoc />
        public async Task UpdateAsync(Entity entity, CancellationToken cancellationToken)
        {
            var message = Settings.LogDetailedRequests
                ? $"Update Request for {entity.LogicalName} with Id {entity.Id} and Attributes {entity.ToStringAttributes()}"
                : "Update Request";
            if (Settings.TimeRequests)
            {
                var timer = new Stopwatch();
                try
                {
                    TraceStart(message);
                    timer.Start();
                    await Service.UpdateAsync(entity, cancellationToken);
                    return;
                }
                finally
                {
                    TraceEnd(timer);
                }
            }

            TraceExecute(message);
            await Service.UpdateAsync(entity, cancellationToken);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(string entityName, Guid id, CancellationToken cancellationToken)
        {
            var message = Settings.LogDetailedRequests
                ? $"Delete Request for {entityName} with Id {id}"
                : "Delete Request";
            if (Settings.TimeRequests)
            {
                var timer = new Stopwatch();
                try
                {
                    TraceStart(message);
                    timer.Start();
                    await Service.DeleteAsync(entityName, id, cancellationToken);
                    return;
                }
                finally
                {
                    TraceEnd(timer);
                }
            }

            TraceExecute(message);
            await Service.DeleteAsync(entityName, id, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<OrganizationResponse> ExecuteAsync(OrganizationRequest request, CancellationToken cancellationToken)
        {
            var message = Settings.LogDetailedRequests
                ? GetDetailedMessage(request)
                : $"Execute Request {request.RequestName}";

            if (Settings.TimeRequests)
            {
                var timer = new Stopwatch();
                try
                {
                    TraceStart(message);
                    timer.Start();
                    return await Service.ExecuteAsync(request, cancellationToken);
                }
                finally
                {
                    TraceEnd(timer);
                }
            }

            TraceExecute(message);
            return await Service.ExecuteAsync(request, cancellationToken);
        }

        /// <inheritdoc />
        public async Task AssociateAsync(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities, CancellationToken cancellationToken)
        {
            var message = Settings.LogDetailedRequests
                ? $"Associate Request for {entityName}, with Id {entityId}, Relationship {relationship.SchemaName}, and Related Entities {relatedEntities.Select(e => e.ToStringDebug()).ToCsv()}."
                : "Associate Request";
            if (Settings.TimeRequests)
            {
                var timer = new Stopwatch();
                try
                {
                    TraceStart(message);
                    timer.Start();
                    await Service.AssociateAsync(entityName, entityId, relationship, relatedEntities, cancellationToken);
                    return;
                }
                finally
                {
                    TraceEnd(timer);
                }
            }

            TraceExecute(message);
            await Service.AssociateAsync(entityName, entityId, relationship, relatedEntities, cancellationToken);
        }

        /// <inheritdoc />
        public async Task DisassociateAsync(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities, CancellationToken cancellationToken)
        {
            var message = Settings.LogDetailedRequests
                ? $"Disassociate Request for {entityName}, with Id {entityId}, Relationship {relationship.SchemaName}, and Related Entities {relatedEntities.ToStringDebug(new StringDebugInfo(singleLine: true))}."
                : "Disassociate Request";
            if (Settings.TimeRequests)
            {
                var timer = new Stopwatch();
                try
                {
                    TraceStart(message);
                    timer.Start();
                    await Service.DisassociateAsync(entityName, entityId, relationship, relatedEntities, cancellationToken);
                    return;
                }
                finally
                {
                    TraceEnd(timer);
                }
            }

            TraceExecute(message);
            await Service.DisassociateAsync(entityName, entityId, relationship, relatedEntities, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<EntityCollection> RetrieveMultipleAsync(QueryBase query, CancellationToken cancellationToken)
        {
            var message = Settings.LogDetailedRequests
                ? "Retrieve Multiple Request " + GetDetailedMessage(query)
                : "Retrieve Multiple Request";

            if (Settings.TimeRequests)
            {
                var timer = new Stopwatch();
                try
                {
                    TraceStart(message);
                    timer.Start();
                    var timedResults = await Service.RetrieveMultipleAsync(query, cancellationToken);
                    if (Settings.LogDetailedRequests)
                    {
                        Trace("Returned: " + timedResults.Entities.Count);
                    }
                    return timedResults;
                }
                finally
                {
                    TraceEnd(timer);
                }
            }

            TraceExecute(message);
            var results = await Service.RetrieveMultipleAsync(query, cancellationToken);
            if (Settings.LogDetailedRequests)
            {
                Trace("Returned: " + results.Entities.Count);
            }
            return results;
        }


        #endregion Implementation of IOrganizationServiceAsync2

        /// <summary>
        /// Gets the detailed message for the request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected virtual string GetDetailedMessage(OrganizationRequest request)
        {
            var message = $"Execute Request for {request.RequestName} with ";
            switch (request.RequestName)
            {
                case "RetrieveMultiple":
                    var query = (QueryBase) request["Query"];
                    message += GetDetailedMessage(query);

                    break;
                default:
                    message += request.Parameters.ToStringDebug("Parameters", new StringDebugInfo(singleLine: true)) + ".";
                    break;
            }

            return message;
        }

        private string GetDetailedMessage(QueryBase query)
        {
            string message;
            switch (query)
            {
                case QueryExpression qe:
                    message = $"Query Expression: {qe.GetSqlStatement()}";
                    break;
                case FetchExpression fe:
                    message = $"Fetch Expression: {fe.Query}";
                    break;
                case QueryByAttribute ba:
                    message =
                        $"Query By Attribute for {ba.EntityName} with attributes {string.Join(", ", ba.Attributes)} and values {string.Join(", ", ba.Values)} and Columns {string.Join(", ", ba.ColumnSet.Columns)}";
                    break;
                default:
                    message = $"Unknown Query Base {query.GetType().FullName}";
                    break;
            }

            return message;
        }

        private void TraceStart(string request)
        {
            Trace(Settings.TimeStartMessageFormat, request);
        }

        /// <summary>
        /// Traces the messages to the appropriate logger/tracing service
        /// </summary>
        /// <param name="message">The Message.</param>
        /// <param name="args">The Args.</param>
        protected virtual void Trace(string message, params object[] args) {
            _tracingService?.Trace(Settings.TimeStartMessageFormat, args);
            _logger?.Log(Settings.LogLevel, message, args);
        }

        private void TraceEnd(Stopwatch timer)
        {
            timer.Stop();
            Trace(Settings.TimeEndMessageFormat, timer.ElapsedMilliseconds / 1000D);
        }

        private void TraceExecute(string message)
        {
            if (Settings.LogDetailedRequests)
            {
                Trace("Executing " + message);
            }
        }
    }
 }
#endif