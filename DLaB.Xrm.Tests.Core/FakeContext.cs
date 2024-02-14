#if NET
using DataverseUnitTest;
#endif
using DLaB.Xrm.Test;
using Microsoft.Xrm.Sdk;
using Source.DLaB.Xrm.Plugin;
using System;

namespace DLaB.Xrm.Tests.Core
{
    internal class FakeContext : IExtendedPluginContext
    {
        public Entity PostImage
        {
            get => this.GetPreEntity<Entity>();
            set => PostEntityImages["PostImage"] = value;
        }

        public Entity PreImage {
            get => this.GetPreEntity<Entity>();
            set => PreEntityImages["PreImage"] = value;
        }

        public Entity Target
        {
            get => this.GetTarget<Entity>();
            set => InputParameters[ParameterName.Target] = value;
        }

        #region IExtendedPluginContext Implementation

        public int Mode { get; set; }
        IsolationMode IExtendedPluginContext.IsolationMode => IsolationMode.Sandbox;
        public string PluginTypeName { get; set; }
        public EntityReference PrimaryEntity { get; set; }
        public RegisteredEvent Event { get; set; }
        public IServiceProvider ServiceProvider { get; set; }
        int IExecutionContext.IsolationMode => (int)IsolationMode.Sandbox;
        public int Depth { get; set; }
        public string MessageName { get; set; }
        public string PrimaryEntityName { get; set; }
        public Guid? RequestId { get; set; }
        public string SecondaryEntityName { get; set; }
        public ParameterCollection InputParameters { get; set; } = new ParameterCollection();
        public ParameterCollection OutputParameters { get; set; } = new ParameterCollection();
        public ParameterCollection SharedVariables { get; set; } = new ParameterCollection();
        public Guid UserId { get; set; }
        public Guid InitiatingUserId { get; set; }
        public Guid BusinessUnitId { get; set; }
        public Guid OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public Guid PrimaryEntityId { get; set; }
        public EntityImageCollection PreEntityImages { get; set; } = new EntityImageCollection();
        public EntityImageCollection PostEntityImages { get; set; } = new EntityImageCollection();
        public EntityReference OwningExtension { get; set; }
        public Guid CorrelationId { get; set; }
        public bool IsExecutingOffline { get; set; }
        public bool IsOfflinePlayback { get; set; }
        public bool IsInTransaction { get; set; }
        public Guid OperationId { get; set; }
        public DateTime OperationCreatedOn { get; set; }
        public int Stage { get; set; }
        public IPluginExecutionContext ParentContext { get; set; }
        public IOrganizationService InitiatingUserOrganizationService { get; set; }
        public bool IsAsync { get; set; }
        public bool IsSync { get; set; }
        public IOrganizationService OrganizationService { get; set; }
        public IOrganizationServiceFactory ServiceFactory { get; set; }
        public IOrganizationService CachedOrganizationService { get; set; }
        public IOrganizationService SystemOrganizationService { get; set; }
        public FakeTraceService FakeTraceService { get; set; } = new FakeTraceService(new DebugLogger());
        public ITracingService TracingService => FakeTraceService;


        public void LogException(Exception ex)
        {
            throw new NotImplementedException();
        }

        public void Trace(string format, params object[] args)
        {
            TracingService.Trace(format, args);
        }

        public void TraceContext()
        {
            throw new NotImplementedException();
        }

        public IDisposable TraceTime(string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        #region ServiceProvider

        public object GetService(Type serviceType)
        {
            return ServiceProvider.GetService(serviceType);
        }

        #endregion ServiceProvider

        #endregion IExtendedPluginContext Implementation

    }
}
