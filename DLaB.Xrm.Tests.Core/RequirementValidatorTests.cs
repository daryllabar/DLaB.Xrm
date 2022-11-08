#if NET
using DataverseUnitTest;
#endif
using DLaB.Xrm.Entities;
using DLaB.Xrm.Plugin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Source.DLaB.Xrm.Plugin;
using System;
using System.Linq;
using Microsoft.Xrm.Sdk;
using ParameterName = Source.DLaB.Xrm.Plugin.ParameterName;
using System.Collections.Generic;
using DLaB.Xrm.Test;
using MessageType = Source.DLaB.Xrm.Plugin.MessageType;

namespace DLaB.Xrm.Tests.Core
{
    [TestClass]
    public class RequirementValidatorTests
    {
        [TestMethod]
        public void SkipExecution_MissingRequired_Should_Skip()
        {
            TestRequired(ContextEntity.CoalesceTargetPreImage);
            TestRequired(ContextEntity.CoalesceTargetPostImage);
            TestRequired(ContextEntity.PreImage);
            TestRequired(ContextEntity.PostImage);
            TestRequired(ContextEntity.Target);
        }

        [TestMethod]
        public void SkipExecution_MissingRequiredAllowNulls_Should_Skip()
        {
            TestRequiredAllowNulls(ContextEntity.CoalesceTargetPreImage);
            TestRequiredAllowNulls(ContextEntity.CoalesceTargetPostImage);
            TestRequiredAllowNulls(ContextEntity.PreImage);
            TestRequiredAllowNulls(ContextEntity.PostImage);
            TestRequiredAllowNulls(ContextEntity.Target);
        }

        [TestMethod]
        public void SkipExecution_MissingUpdated_Should_Skip()
        {
            var sut = new RequirementValidator().AllUpdated<Contact>(c => new { c.NickName });
            var withValue = new Contact { NickName = "NotNull" };
            var withNullValue = new Contact { NickName = null };
            var empty = new Contact();
            TestSkip(sut, withValue, empty);
            TestSkip(sut, withValue, withValue, $"The target did not update the column {Contact.Fields.NickName} to a non-null value!");
            TestSkip(sut, withNullValue, withValue, $"The target did not contain a required update of column {Contact.Fields.NickName} to a non-null value!");
            TestSkip(sut, empty, withValue, $"The target did not contain a required update of column {Contact.Fields.NickName}!");
        }

        [TestMethod]
        public void SkipExecution_MissingUpdatedAllowNulls_Should_Skip()
        {
            var withValue = new Contact { NickName = "NotNull" };
            var withNullValue = new Contact { NickName = null };
            var empty = new Contact();
            var sut = new RequirementValidator().AllUpdatedAllowNulls<Contact>(c => new { c.NickName });
            TestSkip(sut, withValue, empty);
            TestSkip(sut, withValue, withValue, $"The target did not update the column {Contact.Fields.NickName} to a non-null value!");
            TestSkip(sut, withNullValue, withValue);
            TestSkip(sut, empty, withValue, $"The target did not contain a required update of column {Contact.Fields.NickName}!");
        }

        [TestMethod]
        public void SkipExecution_MissingAtLeastOneUpdated_Should_Skip()
        {
            var sut = new RequirementValidator().AtLeastOneUpdated<Contact>(c => new { c.NickName, c.AssistantName });
            var possibleValues = new List<Contact>
            {
                new Contact(),
                new Contact { AssistantName = null },
                new Contact { AssistantName = "NotNull" },
                new Contact { NickName = null },
                new Contact { NickName = null, AssistantName = null },
                new Contact { NickName = null, AssistantName = "NotNull" },
                new Contact { NickName = "NotNull" },
                new Contact { NickName = "NotNull", AssistantName = null },
                new Contact { NickName = "NotNull", AssistantName = "NotNull" },
            };

            foreach (var target in possibleValues)
            {
                foreach (var preImage in possibleValues)
                {
                    var message = target.NickName == null
                                  && target.AssistantName == null
                                  || (target.NickName == preImage.NickName
                                      || target.NickName == null && preImage.NickName != null) 
                                  && (target.AssistantName == preImage.AssistantName
                                      || target.AssistantName == null && preImage.AssistantName != null)
                        ? $"The target did not update to a non-null value for at least one of the following columns: {Contact.Fields.NickName}, {Contact.Fields.AssistantName}!"
                        : null;
                    TestSkip(sut, target, preImage, message);
                }
            }
        }


        [TestMethod]
        public void SkipExecution__MissingAtLeastOneUpdatedAllowNulls_Should_Skip()
        {
            var sut = new RequirementValidator().AtLeastOneUpdatedAllowNulls<Contact>(c => new { c.NickName, c.AssistantName });
            var possibleValues = new List<Contact>
            {
                new Contact(),
                new Contact { AssistantName = null },
                new Contact { AssistantName = "NotNull" },
                new Contact { NickName = null },
                new Contact { NickName = null, AssistantName = null },
                new Contact { NickName = null, AssistantName = "NotNull" },
                new Contact { NickName = "NotNull" },
                new Contact { NickName = "NotNull", AssistantName = null },
                new Contact { NickName = "NotNull", AssistantName = "NotNull" },
            };

            foreach (var target in possibleValues)
            {
                foreach (var preImage in possibleValues)
                {
                    var message = !target.Contains(Contact.Fields.NickName)
                                  && !target.Contains(Contact.Fields.AssistantName)
                                  || (!target.Contains(Contact.Fields.NickName)
                                      || target.NickName == preImage.NickName)
                                  && (!target.Contains(Contact.Fields.AssistantName)
                                      || target.AssistantName == preImage.AssistantName)
                        ? $"The target did not update at least one of the following columns: {Contact.Fields.NickName}, {Contact.Fields.AssistantName}!"
                        : null;
                    TestSkip(sut, target, preImage, message);
                }
            }
        }

        private void TestSkip(RequirementValidator sut, Entity target, Entity preImage, string trace = null)
        {
            TestSkip(sut, target, preImage, trace, MessageType.Create);
            TestSkip(sut, target, preImage, trace, MessageType.Update);
        }

        private static void TestSkip(RequirementValidator sut, Entity target, Entity preImage, string trace, MessageType message)
        {
            var context = new FakeContext
            {
                Target = target,
                PreImage = message == MessageType.Update ? preImage : null,
                MessageName = message
            };

            if (message == MessageType.Create)
            {
                if(preImage.Attributes.Any(a => a.Value != null))
                {
                    // Skip tests for create where the Value is not null in the pre image
                    return;
                }
            }

            try
            {
                if (trace == null)
                {
                    Assert.IsFalse(sut.SkipExecution(context));
                    Assert.AreEqual(0, context.FakeTraceService.Traces.Count);
                }
                else
                {
                    Assert.IsTrue(sut.SkipExecution(context));
                    Assert.AreEqual(trace, context.FakeTraceService.Traces.Single().Trace);
                }
            }
            catch (AssertFailedException)
            {
                var logger = new DebugLogger();
                logger.WriteLine("Failed Target:");
                logger.WriteLine(target.ToStringDebug());
                logger.WriteLine("Failed PreImage:");
                logger.WriteLine(preImage.ToStringDebug());
                logger.WriteLine("Failed Trace:");
                logger.WriteLine(trace ?? "null");
                logger.WriteLine("Failed Message:");
                logger.WriteLine(message);
                throw;
            }
        }

        [TestMethod]
        public void SkipExecution_AtLeastOneRequired_Should_Skip()
        {
            TestAtLeastOneRequired(ContextEntity.CoalesceTargetPreImage);
            TestAtLeastOneRequired(ContextEntity.CoalesceTargetPostImage);
            TestAtLeastOneRequired(ContextEntity.PreImage);
            TestAtLeastOneRequired(ContextEntity.PostImage);
            TestAtLeastOneRequired(ContextEntity.Target);
        }

        [TestMethod]
        public void SkipExecution_AllLeastOneRequiredAllowNulls_Should_Skip()
        {
            TestAllLeastOneRequiredAllowNulls(ContextEntity.CoalesceTargetPreImage);
            TestAllLeastOneRequiredAllowNulls(ContextEntity.CoalesceTargetPostImage);
            TestAllLeastOneRequiredAllowNulls(ContextEntity.PreImage);
            TestAllLeastOneRequiredAllowNulls(ContextEntity.PostImage);
            TestAllLeastOneRequiredAllowNulls(ContextEntity.Target);
        }

        [TestMethod]
        public void SkipExecution_MissingPreImage_Should_Throw()
        {
            MissingPreImage(ContextEntity.CoalesceTargetPreImage);
            MissingPreImage(ContextEntity.PreImage);
        }

        private static void TestRequired(ContextEntity contextEntity)
        {
            var sut = new RequirementValidator().AllRequired<Contact>(contextEntity, c => new { c.NickName });
            var context = GetContext(contextEntity, new Contact());
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual($"The {contextEntity} entity type did not contain the required column {Contact.Fields.NickName}!", context.FakeTraceService.Traces.Single().Trace);
            context = GetContext(contextEntity, new Contact { NickName = null });
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual($"The {contextEntity} entity type contained a null value for the required column {Contact.Fields.NickName}!", context.FakeTraceService.Traces.Single().Trace);
            context = GetContext(contextEntity, new Contact { NickName = "NotNull" });
            Assert.IsFalse(sut.SkipExecution(context));
            Assert.AreEqual(0, context.FakeTraceService.Traces.Count);

            sut = new RequirementValidator().AllRequired<Contact>(contextEntity, c => new { c.NickName, c.AssistantName });
            context = GetContext(contextEntity, new Contact());
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual($"The {contextEntity} entity type did not contain the required column {Contact.Fields.NickName}!", context.FakeTraceService.Traces.Single().Trace);
            context = GetContext(contextEntity, new Contact{ NickName = null, AssistantName = null});
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual($"The {contextEntity} entity type contained a null value for the required column {Contact.Fields.NickName}!", context.FakeTraceService.Traces.Single().Trace);
            context = GetContext(contextEntity, new Contact{ NickName = "NotNull" });
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual($"The {contextEntity} entity type did not contain the required column { Contact.Fields.AssistantName}!", context.FakeTraceService.Traces.Single().Trace);
            context = GetContext(contextEntity, new Contact { NickName = "NotNull", AssistantName = null });
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual($"The {contextEntity} entity type contained a null value for the required column {Contact.Fields.AssistantName}!", context.FakeTraceService.Traces.Single().Trace);

            context = GetContext(contextEntity, new Contact { NickName = "NotNull", AssistantName = "NotNull"});
            Assert.IsFalse(sut.SkipExecution(context));
            Assert.AreEqual(0, context.FakeTraceService.Traces.Count);
        }

        private static void TestRequiredAllowNulls(ContextEntity contextEntity)
        {
            var sut = new RequirementValidator().AllRequiredAllowNulls<Contact>(contextEntity, c => new { c.NickName });
            var context = GetContext(contextEntity, new Contact());
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual($"The {contextEntity} entity type did not contain the required column {Contact.Fields.NickName}!", context.FakeTraceService.Traces.Single().Trace);
            context = GetContext(contextEntity, new Contact { NickName = null });
            Assert.IsFalse(sut.SkipExecution(context));
            Assert.AreEqual(0, context.FakeTraceService.Traces.Count);
            context = GetContext(contextEntity, new Contact { NickName = "NotNull" });
            AssertPassedValidation(sut, context);
        }

        private static void TestAtLeastOneRequired(ContextEntity contextEntity)
        {
            var sut = new RequirementValidator().AtLeastOneRequired<Contact>(contextEntity, c => new { c.NickName, c.AssistantName });
            var context = GetContext(contextEntity, new Contact());
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual($"The {contextEntity} entity type did not contain a non-null value for at least one of the following columns: {Contact.Fields.NickName}, {Contact.Fields.AssistantName}!", context.FakeTraceService.Traces.Single().Trace);
            context = GetContext(contextEntity, new Contact { NickName = null });
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual($"The {contextEntity} entity type did not contain a non-null value for at least one of the following columns: {Contact.Fields.NickName}, {Contact.Fields.AssistantName}!", context.FakeTraceService.Traces.Single().Trace);
            context = GetContext(contextEntity, new Contact { NickName = "NotNull" });
            AssertPassedValidation(sut, context);
            context = GetContext(contextEntity, new Contact { AssistantName = "NotNull" });
            AssertPassedValidation(sut, context);
            context = GetContext(contextEntity, new Contact { NickName = "NotNull", AssistantName = "NotNull" });
            AssertPassedValidation(sut, context);
        }

        private static void TestAllLeastOneRequiredAllowNulls(ContextEntity contextEntity)
        {
            var sut = new RequirementValidator().AllLeastOneRequiredAllowNulls<Contact>(contextEntity, c => new { c.NickName, c.AssistantName });
            var context = GetContext(contextEntity, new Contact());
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual($"The {contextEntity} entity type did not contain at least one of the following columns: {Contact.Fields.NickName}, {Contact.Fields.AssistantName}!", context.FakeTraceService.Traces.Single().Trace);
            context = GetContext(contextEntity, new Contact { NickName = null });
            AssertPassedValidation(sut, context);
            context = GetContext(contextEntity, new Contact { AssistantName = null });
            AssertPassedValidation(sut, context);
            context = GetContext(contextEntity, new Contact { NickName = null, AssistantName = null });
            AssertPassedValidation(sut, context);
        }

        private static void MissingPreImage(ContextEntity contextEntity)
        {
            var sut = new RequirementValidator().AllRequired<Contact>(contextEntity, c => new { c.NickName });
            var context = new FakeContext
            {
                MessageName = MessageType.Update
            };
            try
            {
                sut.SkipExecution(context);
                Assert.Fail("SkipExecutions should have thrown an exception!");
            }
            catch (InvalidPluginExecutionException ex)
            {
                Assert.AreEqual("A pre-image was required but not found!  Expected a pre-image to be registered for this step with the following columns: nickname", ex.Message);
            }

            sut.SkipExecution(GetContext(contextEntity, new Contact()));
        }

        private static void AssertPassedValidation(RequirementValidator sut, FakeContext context)
        {
            Assert.IsFalse(sut.SkipExecution(context));
            Assert.AreEqual(0, context.FakeTraceService.Traces.Count);
        }


        private static FakeContext GetContext(ContextEntity contextEntity, Contact root)
        {
            var context = new FakeContext
            {
                MessageName = MessageType.Update
            };
            switch (contextEntity)
            {
                case ContextEntity.CoalesceTargetPreImage:
                case ContextEntity.PreImage:
                    context.PreImage = root;
                    break;
                case ContextEntity.CoalesceTargetPostImage:
                case ContextEntity.PostImage:
                    context.PostImage = root;
                    break;
                case ContextEntity.Target:
                    context.Target = root;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(contextEntity), contextEntity, null);
            }

            switch (contextEntity)
            {
                case ContextEntity.CoalesceTargetPreImage:
                case ContextEntity.CoalesceTargetPostImage:
                    context.InputParameters[ParameterName.Target] = new Contact();
                    break;
            }

            return context;
        }
    }
}
