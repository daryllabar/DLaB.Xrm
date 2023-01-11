#if NET
using DataverseUnitTest;
using DataverseUnitTest.Builders;
#else
using DLaB.Xrm.Test;
using DLaB.Xrm.Test.Builders;
#endif
using Microsoft.Xrm.Sdk;

namespace DLaB.Xrm.Test
{
    public abstract class TestMethodClassBase : TestMethodClassBaseDLaB
    {
        protected override IAgnosticServiceBuilder GetOrganizationServiceBuilder(IOrganizationService service) { return new OrganizationServiceBuilder(service); }

        protected override void LoadConfigurationSettings()
        {
            TestInitializer.InitializeTestSettings();
        }

        public void Test()
        {
            Test(new DebugLogger());
        }
    }
}
