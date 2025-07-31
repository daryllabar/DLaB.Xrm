#if NET
using DataverseUnitTest;
#endif
using DLaB.Xrm.Test;
using Microsoft.Xrm.Sdk;
using System;
using Source.DLaB.Xrm.Plugin;

namespace DLaB.Xrm.Tests.Core
{
    internal class FakeContext : IExtendedPluginContext
    {
        public Entity? PostImage
        {
            get => this.GetPreEntity<Entity>();
            set => PostEntityImages["PostImage"] = value;
        }

        public Entity? PreImage {
            get => this.GetPreEntity<Entity>();
            set => PreEntityImages["PreImage"] = value;
        }

        public Entity? Target
        {
            get => this.GetTarget<Entity>();
            set => InputParameters[ParameterName.Target] = value;
        }

        #region IExtendedPluginContext Implementation

        public int Mode { get; set; }
        IsolationMode IExtendedPluginContext.IsolationMode => IsolationMode.Sandbox;
        public string PluginTypeName { get; set; } = "Undefined";
        public EntityReference PrimaryEntity { get; set; } = new EntityReference();
        public RegisteredEvent Event { get; set; } = null!;
        public IServiceProvider ServiceProvider { get; set; } = null!;
        int IExecutionContext.IsolationMode => (int)IsolationMode.Sandbox;
        public int Depth { get; set; }
        public string MessageName { get; set; } = "Undefined";
        public string PrimaryEntityName { get; set; } = "Undefined";
        public Guid? RequestId { get; set; }
        public string SecondaryEntityName { get; set; } = "Undefined";
        public ParameterCollection InputParameters { get; set; } = new ParameterCollection();
        public ParameterCollection OutputParameters { get; set; } = new ParameterCollection();
        public ParameterCollection SharedVariables { get; set; } = new ParameterCollection();
        public Guid UserId { get; set; }
        public Guid InitiatingUserId { get; set; }
        public Guid BusinessUnitId { get; set; }
        public Guid OrganizationId { get; set; }
        public string OrganizationName { get; set; } = "Undefined";
        public Guid PrimaryEntityId { get; set; }
        public EntityImageCollection PreEntityImages { get; set; } = new EntityImageCollection();
        public EntityImageCollection PostEntityImages { get; set; } = new EntityImageCollection();
        public EntityReference OwningExtension { get; set; } = new EntityReference();
        public Guid CorrelationId { get; set; }
        public bool IsExecutingOffline { get; set; }
        public bool IsOfflinePlayback { get; set; }
        public bool IsInTransaction { get; set; }
        public Guid OperationId { get; set; }
        public DateTime OperationCreatedOn { get; set; }
        public int Stage { get; set; }
        public IPluginExecutionContext ParentContext { get; set; } = null!;
        public IOrganizationService InitiatingUserOrganizationService { get; set; } = null!;
        public bool IsAsync { get; set; }
        public bool IsSync { get; set; }
        public IOrganizationService OrganizationService { get; set; } = null!;
        public IOrganizationServiceFactory ServiceFactory { get; set; } = null!;
        public IOrganizationService CachedOrganizationService { get; set; } = null!;
        public IOrganizationService SystemOrganizationService { get; set; } = null!;
        public FakeTraceService FakeTraceService { get; set; } = new FakeTraceService(new DebugLogger());
        public ITracingService TracingService => FakeTraceService;
        #if !(XRM_2013 || XRM_2015 || XRM_2016)
        public IManagedIdentityService ManagedIdentityService { get; set; } = null!;
        #endif

        public void LogException(Exception ex)
        {
            throw new NotImplementedException();
        }

        public void Trace(string format, params object?[] args)
        {
            TracingService.Trace(format, args);
        }

        public void TraceContext()
        {
            throw new NotImplementedException();
        }

        public IDisposable TraceTime(string format, params object?[] args)
        {
            throw new NotImplementedException();
        }

        #region ServiceProvider

        public object? GetService(Type serviceType)
        {
            return ServiceProvider.GetService(serviceType);
        }

        #endregion ServiceProvider

        #endregion IExtendedPluginContext Implementation

    }
}
