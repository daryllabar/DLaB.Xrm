#if NET
using DataverseUnitTest;
#endif
using DLaB.Xrm.Client;
using DLaB.Xrm.Entities;
using DLaB.Xrm.Test.Core.Builders;
using Microsoft.Xrm.Sdk;

namespace DLaB.Xrm.Test
{
    /// <summary>
    /// Class to Initialize all TestSettings used by the Framework
    /// </summary>
    public class TestInitializer
    {
        public static void InitializeTestSettings()
        {

#if NET
            if (!TestSettings.AssumptionJsonPath.IsConfigured)
            {
                TestSettings.AssumptionJsonPath.Configure(new PatherFinderProjectOfType(typeof(MsTestProvider), "Assumptions\\Entity Xml"));
            }
#else
            if (!TestSettings.AssumptionXmlPath.IsConfigured)
            {
                TestSettings.AssumptionXmlPath.Configure(new PatherFinderProjectOfType(typeof(MsTestProvider), "Assumptions\\Entity Xml"));
            }
#endif

            if (!TestSettings.EntityBuilder.IsConfigured)
            {
                TestSettings.EntityBuilder.ConfigureDerivedAssembly<EntityBuilder<Entity>>();
            }
            if (!TestSettings.UserTestConfigPath.IsConfigured)
            {
                TestSettings.UserTestConfigPath.Configure(new PatherFinderProjectOfType(typeof(MsTestProvider), "UnitTestSettings.user.config"));
            }
            if (!TestSettings.EarlyBound.IsConfigured)
            {
                TestSettings.EarlyBound.ConfigureDerivedAssembly<CrmContext>();
                CrmServiceUtility.GetEarlyBoundProxyAssembly(TestSettings.EarlyBound.Assembly);
            }
            if (!TestSettings.TestFrameworkProvider.IsConfigured)
            {
                TestSettings.TestFrameworkProvider.Configure(new MsTestProvider());
            }
        }
    }
}
