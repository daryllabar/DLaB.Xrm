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
        private static List<Contact> PossibleValues
        {
            get
            {
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
                return possibleValues;
            }
        }

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
        public void SkipExecution_MissingRequiredNull_Should_Skip()
        {
            TestRequiredNull(ContextEntity.CoalesceTargetPreImage);
            TestRequiredNull(ContextEntity.CoalesceTargetPostImage);
            TestRequiredNull(ContextEntity.PreImage);
            TestRequiredNull(ContextEntity.PostImage);
            TestRequiredNull(ContextEntity.Target);
        }

        [TestMethod]
        public void SkipExecution_MissingUpdated_Should_Skip()
        {
            var sut = new RequirementValidator().Updated<Contact>(c => new { c.NickName });
            var withValue = new Contact { NickName = "NotNull" };
            var withNullValue = new Contact { NickName = null };
            var empty = new Contact();
            TestSkip(sut, withValue, empty);
            TestSkip(sut, withValue, withValue, $"The target did not update the column {Contact.Fields.NickName} to a non-null value!");
            TestSkip(sut, withNullValue, withValue, $"The target did not contain a required update of column {Contact.Fields.NickName} to a non-null value!");
            TestSkip(sut, empty, withValue, $"The target did not contain a required update of column {Contact.Fields.NickName}!");
        }

        [TestMethod]
        public void SkipExecution_MissingChanged_Should_Skip()
        {
            var withValue = new Contact { NickName = "NotNull" };
            var withNullValue = new Contact { NickName = null };
            var empty = new Contact();
            var sut = new RequirementValidator().Changed<Contact>(c => new { c.NickName });
            TestSkip(sut, withValue, empty);
            TestSkip(sut, withValue, withValue, $"The target did not update the column {Contact.Fields.NickName} to a non-null value!");
            TestSkip(sut, withNullValue, withValue);
            TestSkip(sut, empty, withValue, $"The target did not contain a required update of column {Contact.Fields.NickName}!");
        }

        [TestMethod]
        public void SkipExecution_MissingCleared_Should_Skip()
        {
            var withValue = new Contact { NickName = "NotNull" };
            var withNullValue = new Contact { NickName = null };
            var empty = new Contact();
            var sut = new RequirementValidator().Cleared<Contact>(c => new { c.NickName });
            TestSkip(sut, withValue, empty, $"The target contained a non-null value for column {Contact.Fields.NickName} that was required to be updated to null!");
            TestSkip(sut, withValue, withValue, $"The target contained a non-null value for column {Contact.Fields.NickName} that was required to be updated to null!");
            TestSkip(sut, withNullValue, withValue);
            TestSkip(sut, withNullValue, withNullValue, $"The target contained a null value for column {Contact.Fields.NickName} that was required to be updated to null, but it was already null!");
            TestSkip(sut, empty, withValue, $"The target did not contain a required update of column {Contact.Fields.NickName} to null!");
        }

        [TestMethod]
        public void SkipExecution_MissingUpdatedAny_Should_Skip()
        {
            var sut = new RequirementValidator().UpdatedAny<Contact>(c => new { c.NickName, c.AssistantName });

            foreach (var target in PossibleValues)
            {
                foreach (var preImage in PossibleValues)
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
        public void SkipExecution_MissingChangedAny_Should_Skip()
        {
            var sut = new RequirementValidator().ChangedAny<Contact>(c => new { c.NickName, c.AssistantName });

            foreach (var target in PossibleValues)
            {
                foreach (var preImage in PossibleValues)
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

        [TestMethod]
        public void SkipExecution_MissingClearedAny_Should_Skip()
        {
            var sut = new RequirementValidator().ClearedAny<Contact>(c => new { c.NickName, c.AssistantName });

            foreach (var target in PossibleValues)
            {
                foreach (var preImage in PossibleValues)
                {
                    var nickNameNulled = target.Contains(Contact.Fields.NickName)
                                         && target.NickName == null
                                         && preImage.NickName != null;
                    var assistantNulled = target.Contains(Contact.Fields.AssistantName)
                                          && target.AssistantName == null
                                          && preImage.AssistantName != null;
                    var message = !nickNameNulled
                                  && !assistantNulled
                        ? $"The target did not update at least one of the following columns to null: {Contact.Fields.NickName}, {Contact.Fields.AssistantName}!"
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
        public void SkipExecution_ContainsAny_Should_Skip()
        {
            TestContainsAny(ContextEntity.CoalesceTargetPreImage);
            TestContainsAny(ContextEntity.CoalesceTargetPostImage);
            TestContainsAny(ContextEntity.PreImage);
            TestContainsAny(ContextEntity.PostImage);
            TestContainsAny(ContextEntity.Target);
        }

        [TestMethod]
        public void SkipExecution_ContainsAnyNullable_Should_Skip()
        {
            TestContainsAnyNullable(ContextEntity.CoalesceTargetPreImage);
            TestContainsAnyNullable(ContextEntity.CoalesceTargetPostImage);
            TestContainsAnyNullable(ContextEntity.PreImage);
            TestContainsAnyNullable(ContextEntity.PostImage);
            TestContainsAnyNullable(ContextEntity.Target);
        }

        [TestMethod]
        public void SkipExecution_ContainsAnyNull_Should_Skip()
        {
            TestContainsAnyNull(ContextEntity.CoalesceTargetPreImage);
            TestContainsAnyNull(ContextEntity.CoalesceTargetPostImage);
            TestContainsAnyNull(ContextEntity.PreImage);
            TestContainsAnyNull(ContextEntity.PostImage);
            TestContainsAnyNull(ContextEntity.Target);
        }

        [TestMethod]
        public void SkipExecution_MissingPreImage_Should_Throw()
        {
            MissingPreImage(ContextEntity.CoalesceTargetPreImage);
            MissingPreImage(ContextEntity.PreImage);
        }

        private static void TestRequired(ContextEntity contextEntity)
        {
            var sut = new RequirementValidator().Contains<Contact>(contextEntity, c => new { c.NickName });
            var context = GetContext(contextEntity, new Contact());
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual($"The {contextEntity} entity type did not contain the required column {Contact.Fields.NickName}!", context.FakeTraceService.Traces.Single().Trace);
            context = GetContext(contextEntity, new Contact { NickName = null });
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual($"The {contextEntity} entity type contained a null value for the required column {Contact.Fields.NickName}!", context.FakeTraceService.Traces.Single().Trace);
            context = GetContext(contextEntity, new Contact { NickName = "NotNull" });
            Assert.IsFalse(sut.SkipExecution(context));
            Assert.AreEqual(0, context.FakeTraceService.Traces.Count);

            sut = new RequirementValidator().Contains<Contact>(contextEntity, c => new { c.NickName, c.AssistantName });
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
            var sut = new RequirementValidator().ContainsNullable<Contact>(contextEntity, c => new { c.NickName });
            var context = GetContext(contextEntity, new Contact());
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual($"The {contextEntity} entity type did not contain the required column {Contact.Fields.NickName}!", context.FakeTraceService.Traces.Single().Trace);
            context = GetContext(contextEntity, new Contact { NickName = null });
            Assert.IsFalse(sut.SkipExecution(context));
            Assert.AreEqual(0, context.FakeTraceService.Traces.Count);
            context = GetContext(contextEntity, new Contact { NickName = "NotNull" });
            AssertPassedValidation(sut, context);
        }

        private static void TestRequiredNull(ContextEntity contextEntity)
        {
            var sut = new RequirementValidator().ContainsNull<Contact>(contextEntity, c => new { c.NickName });
            var context = GetContext(contextEntity, new Contact());
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual($"The {contextEntity} entity type did not contain the required to be null column {Contact.Fields.NickName}!", context.FakeTraceService.Traces.Single().Trace);
            context = GetContext(contextEntity, new Contact { NickName = null });
            Assert.IsFalse(sut.SkipExecution(context));
            Assert.AreEqual(0, context.FakeTraceService.Traces.Count);
            context = GetContext(contextEntity, new Contact { NickName = "NotNull" });
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual($"The {contextEntity} entity type contained a non-null value for the required to be null column {Contact.Fields.NickName}!", context.FakeTraceService.Traces.Single().Trace);
        }

        private static void TestContainsAny(ContextEntity contextEntity)
        {
            var sut = new RequirementValidator().ContainsAny<Contact>(contextEntity, c => new { c.NickName, c.AssistantName });
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

        private static void TestContainsAnyNullable(ContextEntity contextEntity)
        {
            var sut = new RequirementValidator().ContainsAnyNullable<Contact>(contextEntity, c => new { c.NickName, c.AssistantName });
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

        private static void TestContainsAnyNull(ContextEntity contextEntity)
        {
            var sut = new RequirementValidator().ContainsAnyNull<Contact>(contextEntity, c => new { c.NickName, c.AssistantName });
            var context = GetContext(contextEntity, new Contact());
            var expectedSkipMessage = $"The {contextEntity} entity type did not contain a null value for at least one of the following columns: {Contact.Fields.NickName}, {Contact.Fields.AssistantName}!";
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual(expectedSkipMessage, context.FakeTraceService.Traces.Single().Trace);
            context = GetContext(contextEntity, new Contact { NickName = null });
            AssertPassedValidation(sut, context);
            context = GetContext(contextEntity, new Contact { NickName = "NotNull" });
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual(expectedSkipMessage, context.FakeTraceService.Traces.Single().Trace);
            context = GetContext(contextEntity, new Contact { AssistantName = null });
            AssertPassedValidation(sut, context);
            context = GetContext(contextEntity, new Contact { AssistantName = "NotNull" });
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual(expectedSkipMessage, context.FakeTraceService.Traces.Single().Trace);
            context = GetContext(contextEntity, new Contact { NickName = null, AssistantName = null });
            AssertPassedValidation(sut, context);
            context = GetContext(contextEntity, new Contact { NickName = "NotNull", AssistantName = "NotNull" });
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual(expectedSkipMessage, context.FakeTraceService.Traces.Single().Trace);
        }

        private static void MissingPreImage(ContextEntity contextEntity)
        {
            var sut = new RequirementValidator().Contains<Contact>(contextEntity, c => new { c.NickName });
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
