using System;
using DLaB.Xrm.Entities;
using DLaB.Xrm.Test.Core.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Source.DLaB.Xrm.Plugin;

namespace DLaB.Xrm.Tests.Core.Plugin
{
    [TestClass]
    public class ExtensionsTests
    {

        #region GetEvent

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

        #endregion GetEvent

        #region GetTarget

        [TestMethod]
        public void GetTarget_WithTarget_Should_ReturnTarget()
        {
            //
            // Arrange
            //
            var target = new Entity(Contact.EntityLogicalName);
            var context = new PluginExecutionContextBuilder()
                .WithTarget(target)
                .Build();

            //
            // Act
            //
            var result = context.GetTarget<Entity>();

            //
            // Assert
            //
            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void GetTarget_WithNoTarget_Should_ReturnNull()
        {
            //
            // Arrange
            //
            var context = new PluginExecutionContextBuilder()
                .Build();

            //
            // Act
            //
            var target = context.GetTarget<Entity>();

            //
            // Assert
            //
            Assert.IsNull(target);
        }

        [TestMethod]
        public void GetRequiredTarget_WithTarget_Should_ReturnTarget()
        {
            //
            // Arrange
            //
            var target = new Entity(Contact.EntityLogicalName);
            var context = new PluginExecutionContextBuilder()
                .WithTarget(target)
                .Build();

            //
            // Act
            //
            var result = context.GetRequiredTarget<Entity>();

            //
            // Assert
            //
            Assert.AreEqual(target, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPluginExecutionException))]
        public void GetRequiredTarget_WithNoTarget_Should_Error()
        {
            //
            // Arrange
            //
            var context = new PluginExecutionContextBuilder()
                .Build();

            //
            // Act
            //
            context.GetRequiredTarget<Entity>();

            //
            // Assert
            //
            Assert.Fail("Exception should have been thrown!");
        }

        #endregion GetTarget

        #region GetTargetEntityReference

        [TestMethod]
        public void GetTargetEntityReference_WithTarget_Should_ReturnTarget()
        {
            //
            // Arrange
            //
            var target = new EntityReference(Contact.EntityLogicalName, Guid.NewGuid());
            var context = new PluginExecutionContextBuilder()
                .WithTarget(target)
                .Build();

            //
            // Act
            //
            var result = context.GetTargetEntityReference();

            //
            // Assert
            //
            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void GetTargetEntityReference_WithNoTarget_Should_ReturnNull()
        {
            //
            // Arrange
            //
            var context = new PluginExecutionContextBuilder().Build();

            //
            // Act
            //
            var target = context.GetTargetEntityReference();

            //
            // Assert
            //
            Assert.IsNull(target);
        }

        [TestMethod]
        public void GetRequiredTargetEntityReference_WithTarget_Should_ReturnTarget()
        {
            //
            // Arrange
            //
            var target = new EntityReference(Contact.EntityLogicalName, Guid.NewGuid());
            var context = new PluginExecutionContextBuilder()
                .WithTarget(target)
                .Build();

            //
            // Act
            //
            var result = context.GetRequiredTargetEntityReference();

            //
            // Assert
            //
            Assert.AreEqual(target, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPluginExecutionException))]
        public void GetRequiredTargetEntityReference_WithNoTarget_Should_Error()
        {
            //
            // Arrange
            //
            var context = new PluginExecutionContextBuilder()
                .Build();

            //
            // Act
            //
            context.GetRequiredTargetEntityReference();

            //
            // Assert
            //
            Assert.Fail("Exception should have been thrown!");
        }

        #endregion GetTargetEntityReference
    }
}
