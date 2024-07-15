using DLaB.Xrm.Entities;
using DLaB.Xrm.Test.Core.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Source.DLaB.Xrm.Plugin;

namespace DLaB.Xrm.Tests.Core.Plugin
{
    [TestClass]
    public class ExtensionTests
    {
		[DataRow(RegisteredEvent.ContextMode.Async, RegisteredEvent.ContextMode.Async, DisplayName = "Async Mode")]
		[DataRow(null, RegisteredEvent.ContextMode.Async, DisplayName = "Null Mode with Async Context")]
		[DataRow(null, RegisteredEvent.ContextMode.Sync, DisplayName = "Null Mode with Sync Context")]
		[DataRow(RegisteredEvent.ContextMode.Sync, RegisteredEvent.ContextMode.Sync, DisplayName = "Sync Mode")]
        [TestMethod]
        public void GetEvent_MatchingMode_Should_RetrieveEvent(int? eventMode, int contextMode)
        {
			//
			// Arrange
			//
			var builder = new RegisteredEventBuilder(PipelineStage.PostOperation, MessageType.Create)
				.ForEntities(Contact.EntityLogicalName);
			switch (eventMode)
            {
                case RegisteredEvent.ContextMode.Async:
                    builder.ForAsyncOnly();
                    break;
                case RegisteredEvent.ContextMode.Sync:
                    builder.ForSyncOnly();
                    break;
            }
			var registrations = builder.Build();

			var context = new PluginExecutionContextBuilder()
				.WithRegisteredEvent((int)PipelineStage.PostOperation, MessageType.Create, Contact.EntityLogicalName)
				.WithMode(contextMode)
				.Build();

			//
			// Act
			//
			var @event = context.GetEvent(registrations);

			//
			// Assert
			//
			Assert.IsNotNull(@event);
        }

        [DataRow(RegisteredEvent.ContextMode.Async, RegisteredEvent.ContextMode.Sync, DisplayName = "Async Mode")]
        [DataRow(RegisteredEvent.ContextMode.Sync, RegisteredEvent.ContextMode.Async, DisplayName = "Sync Mode")]
        [TestMethod]
        public void GetEvent_UnmatchedMode_Should_SkipEvent(int? eventMode, int contextMode)
        {
            //
            // Arrange
            //
            var builder = new RegisteredEventBuilder(PipelineStage.PostOperation, MessageType.Create)
                .ForEntities(Contact.EntityLogicalName);
            switch (eventMode)
            {
                case RegisteredEvent.ContextMode.Async:
                    builder.ForAsyncOnly();
                    break;
                case RegisteredEvent.ContextMode.Sync:
                    builder.ForSyncOnly();
                    break;
            }
            var registrations = builder.Build();

            var context = new PluginExecutionContextBuilder()
                .WithRegisteredEvent((int)PipelineStage.PostOperation, MessageType.Create, Contact.EntityLogicalName)
                .WithMode(contextMode)
                .Build();

            //
            // Act
            //
            var @event = context.GetEvent(registrations);

            //
            // Assert
            //
            Assert.IsNull(@event);
        }
    }
}
