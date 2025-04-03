#if NET
using DataverseUnitTest;
using DataverseUnitTest.Builders;
#else
using DLaB.Xrm.Test;
using DLaB.Xrm.Test.Builders;
#endif
using Microsoft.Xrm.Sdk;

namespace DLaB.Xrm.Test.Core.Builders
{
    public class ServiceProviderBuilder : ServiceProviderBuilderBase<ServiceProviderBuilder>
    {
        public ServiceProviderBuilder() : this(null, new FakePluginExecutionContext(), (ITestLogger?)null)
        {
            
        }

        public ServiceProviderBuilder(IOrganizationService? service, IPluginExecutionContext context, ITestLogger? logger) : base(service, context, logger)
        {
            
        }

        public ServiceProviderBuilder(IOrganizationService? service, IPluginExecutionContext context, ITracingService? trace) : base(service, context, trace)
        {

        }

        protected override ServiceProviderBuilder This => this;
    }
}
