#if NET
using DataverseUnitTest;
#endif
using DLaB.Xrm.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Source.DLaB.Xrm.Plugin;

namespace DLaB.Xrm.Tests.Core.Plugin
{
    [TestClass]
    public class RegisteredEventBuilderTests
    {
        [DataRow(false, RegisteredEvent.ContextMode.Async, DisplayName = "Any Entity, Async")]
        [DataRow(false, RegisteredEvent.ContextMode.Sync,  DisplayName = "Any Entity, Sync")]
        [DataRow(true, RegisteredEvent.ContextMode.Async,  DisplayName = "Entity Specific, Async")]
        [DataRow(true, RegisteredEvent.ContextMode.Sync,   DisplayName = "Entity Specific, Sync")]
        [TestMethod]
        public void Build_WithMode_Should_BuildMode(bool entitySpecific, int? mode)
        {
            // Arrange
            var builder = new RegisteredEventBuilder(PipelineStage.PostOperation, MessageType.Create);
            if (mode == RegisteredEvent.ContextMode.Sync)
            {
                builder.ForSyncOnly();
            }
            else
            {
                builder.ForAsyncOnly();
            }
            if (entitySpecific)
            {
                builder.ForEntities(Contact.EntityLogicalName);
            }

            // Act
            var registeredEvent = builder.Build().Single();

            // Assert
            Assert.AreEqual(mode == RegisteredEvent.ContextMode.Sync, registeredEvent.IsSync);
            Assert.AreEqual(mode == RegisteredEvent.ContextMode.Async, registeredEvent.IsAsync);
        }
    }
}
