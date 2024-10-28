#if NET
using DataverseUnitTest;
#endif
using DLaB.Xrm.Entities;
using DLaB.Xrm.Test;
using DLaB.Xrm.Test.Core.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Source.DLaB.Xrm;
using Source.DLaB.Xrm.Plugin;

namespace DLaB.Xrm.Tests.Core.Plugin
{
    [TestClass]
    public class ExtendedExecutionContextTests
    {
        private IOrganizationService _service;

        [TestInitialize]
        public void TestInitialize()
        {
            TestInitializer.InitializeTestSettings();
            _service = TestBase.GetOrganizationService();
        }

        [TestMethod]
        public void AcquireLock_MultipleCalls_Should_AcquireLockOnce()
        {
            //
            // Arrange
            //
            var context = new PluginExecutionContextBuilder()
                .WithRegisteredEvent((int)PipelineStage.PostOperation, MessageType.Create, Contact.EntityLogicalName)
                .IsInTransaction(true)
                .Build();
            var serviceProvider = new ServiceProviderBuilder(_service, context, new DebugLogger())
                .WithService(new OrganizationServicesWrapper(_service, _service, _service, _service)).Build();
            var sut = new DLaBExtendedPluginContextBase(serviceProvider, "None", new RegisteredEvent(PipelineStage.PostOperation, MessageType.Create));

            //
            // Act
            //
            var key = "Key";
            Assert.IsTrue(sut.AcquireLock(key));

            //
            // Assert
            //
            Assert.IsFalse(sut.AcquireLock(key));
        }

        [TestMethod]
        public void AcquireLock_OutsideOfTransaction_Should_Fail()
        {
            //
            // Arrange
            //
            var context = new PluginExecutionContextBuilder()
                .WithRegisteredEvent((int)PipelineStage.PostOperation, MessageType.Create, Contact.EntityLogicalName)
                .IsInTransaction(false)
                .Build();
            var serviceProvider = new ServiceProviderBuilder(_service, context, new DebugLogger())
                .WithService(new OrganizationServicesWrapper(_service, _service, _service, _service)).Build();
            var sut = new DLaBExtendedPluginContextBase(serviceProvider, "None", new RegisteredEvent(PipelineStage.PostOperation, MessageType.Create));

            //
            // Act/Assert
            //
            try
            {
                sut.AcquireLock("key");
                Assert.Fail();
            }
            catch (InvalidPluginExecutionException ex)
            {
                if (ex.Message != "A lock can only be called within a transaction, which is not possible in pre-validation!")
                {
                    throw;
                }
            }
        }
    }
}
