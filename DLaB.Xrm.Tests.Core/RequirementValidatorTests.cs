using DLaB.Xrm.Entities;
using DLaB.Xrm.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Source.DLaB.Xrm.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MessageType = Source.DLaB.Xrm.Plugin.MessageType;
using ParameterName = Source.DLaB.Xrm.Plugin.ParameterName;

namespace DLaB.Xrm.Tests.Core
{
    [TestClass]
    public class RequirementValidatorTests
    {
        private static List<Contact> PossibleValues { get; } = new()
        {
            new (),
            new () { AssistantName = null },
            new () { AssistantName = string.Empty },
            new () { AssistantName = "NotNull" },
            new () { NickName = null },
            new () { NickName = null, AssistantName = null },
            new () { NickName = null, AssistantName = "NotNull" },
            new () { NickName = null, AssistantName = string.Empty },
            new () { NickName = "NotNull" },
            new () { NickName = string.Empty },
            new () { NickName = "NotNull", AssistantName = null },
            new () { NickName = string.Empty, AssistantName = null },
            new () { NickName = "NotNull", AssistantName = "NotNull" },
            new () { NickName = string.Empty, AssistantName = string.Empty }
        };

        private static List<Contact> PossiblePreImageValues { get; } = new()
        {
            new (),
            new () { AssistantName = "NotNull" },
            new () { NickName = "NotNull" },
            new () { NickName = "NotNull", AssistantName = "NotNull" },
        };

        private static List<ContextEntity> EntityTypes { get; } = new()
        {
            ContextEntity.CoalesceTargetPreImage,
            ContextEntity.CoalesceTargetPostImage,
            ContextEntity.PreImage,
            ContextEntity.PostImage,
            ContextEntity.Target
        };

        private static readonly Expression<Func<Contact, object>> ContactNickName = c => new { c.NickName };

        #region Test Contains

        [TestMethod]
        public void SkipExecution_MissingRequired_Should_Skip()
        {
            EntityTypes.ForEach(TestContains);
        }

        [TestMethod]
        public void SkipExecution_MissingRequiredAllowNulls_Should_Skip()
        {
            EntityTypes.ForEach(TestContainsNullable);
        }

        [TestMethod]
        public void SkipExecution_MissingRequiredNull_Should_Skip()
        {
            EntityTypes.ForEach(TestContainsNull);
        }

        [TestMethod]
        public void SkipExecution_MissingRequiredValue_Should_Skip()
        {
            EntityTypes.ForEach(TestContainsValue);
        }

        [TestMethod]
        public void SkipExecution_DoesNotContainAny_Should_Skip()
        {
            EntityTypes.ForEach(TestContainsAny);
        }

        [TestMethod]
        public void SkipExecution_DoesNotContainAnyNullable_Should_Skip()
        {
            EntityTypes.ForEach(TestContainsAnyNullable);
        }

        [TestMethod]
        public void SkipExecution_DoesNotContainAnyNull_Should_Skip()
        {
            EntityTypes.ForEach(TestContainsAnyNull);
        }

        [TestMethod]
        public void SkipExecution_DoesNotContainAnyValue_Should_Skip()
        {
            EntityTypes.ForEach(TestContainsAnyValue);
        }

        #endregion Test Contains

        #region Test Missing

        [TestMethod]
        public void SkipExecution_MissingOrNullPresent_Should_Skip()
        {
            EntityTypes.ForEach(TestMissingOrNull);
        }

        [TestMethod]
        public void SkipExecution_MissingPresent_Should_Skip()
        {
            EntityTypes.ForEach(TestMissing);
        }
        
        [TestMethod]
        public void SkipExecution_MissingAnyAllPresent_Should_Skip()
        {
            EntityTypes.ForEach(TestMissingAny);
        }

        [TestMethod]
        public void SkipExecution_MissingOrNullAnyAllPresent_Should_Skip()
        {
            EntityTypes.ForEach(TestMissingOrNullAny);
        }

        //[TestMethod]
        //public void SkipExecution_DoesNotContainAnyNullable_Should_Skip()
        //{
        //    EntityTypes.ForEach(TestMissingAnyNullable);
        //}
        //
        //[TestMethod]
        //public void SkipExecution_DoesNotContainAnyNull_Should_Skip()
        //{
        //    EntityTypes.ForEach(TestMissingAnyNull);
        //}
        //
        //[TestMethod]
        //public void SkipExecution_DoesNotContainAnyValue_Should_Skip()
        //{
        //    EntityTypes.ForEach(TestMissingAnyValue);
        //}

        #endregion Test Missing

        #region Test Not

        [TestMethod]
        public void SkipExecution_ContainsInvalidValue_Should_Skip()
        {
            EntityTypes.ForEach(TestNotValue);
        }

        [TestMethod]
        public void SkipExecution_ContainsAnyInvalidValue_Should_Skip()
        {
            EntityTypes.ForEach(TestNotAnyValue);
        }

        #endregion Test Not

        #region Test Updated (Non-Null) / Changed (Nullable) / Cleared (Null) / Updated (Value)

        [TestMethod]
        public void SkipExecution_MissingUpdated_Should_Skip()
        {
            var sut = new RequirementValidator().Updated<Contact>(c => new { c.NickName });
            var withValue = new Contact { NickName = "NotNull" };
            var withEmptyString = new Contact { NickName = string.Empty };
            var withNullValue = new Contact { NickName = null };
            var empty = new Contact();
            TestSkip(sut, withValue, empty);
            TestSkip(sut, withValue, withValue,       $"The target did not update the column {Contact.Fields.NickName} to a non-null value!");
            TestSkip(sut, withNullValue, withValue,   $"The target did not contain a required update of column {Contact.Fields.NickName} to a non-null value!");
            TestSkip(sut, withEmptyString, withValue, $"The target did not contain a required update of column {Contact.Fields.NickName} to a non-null value!");
            TestSkip(sut, empty, withValue,           $"The target did not contain a required update of column {Contact.Fields.NickName}!");
        }

        [TestMethod]
        public void SkipExecution_MissingChanged_Should_Skip()
        {
            var withValue = new Contact { NickName = "NotNull" };
            var withNullValue = new Contact { NickName = null };
            var withEmptyString = new Contact { NickName = string.Empty };
            var empty = new Contact();
            var sut = new RequirementValidator().Changed<Contact>(c => new { c.NickName });
            TestSkip(sut, withValue, empty);
            TestSkip(sut, withValue, withValue, $"The target did not update the column {Contact.Fields.NickName} to a non-null value!");
            TestSkip(sut, withNullValue, withValue);
            TestSkip(sut, withEmptyString, withValue);
            TestSkip(sut, empty, withValue,     $"The target did not contain a required update of column {Contact.Fields.NickName}!");
        }

        [TestMethod]
        public void SkipExecution_MissingCleared_Should_Skip()
        {
            var withValue = new Contact { NickName = "NotNull" };
            var withNullValue = new Contact { NickName = null };
            var withEmptyString = new Contact { NickName = string.Empty };
            var empty = new Contact();
            var sut = new RequirementValidator().Cleared<Contact>(c => new { c.NickName });
            TestSkip(sut, withValue, empty,             $"The target contained a non-null value for column {Contact.Fields.NickName} that was required to be updated to null!");
            TestSkip(sut, withValue, withValue,         $"The target contained a non-null value for column {Contact.Fields.NickName} that was required to be updated to null!");
            TestSkip(sut, withNullValue, withValue);
            TestSkip(sut, withEmptyString, withValue);
            TestSkip(sut, withNullValue, empty,         $"The target contained a null value for column {Contact.Fields.NickName} that was required to be updated to null, but it was already null!");
            TestSkip(sut, withEmptyString, empty,       $"The target contained a null value for column {Contact.Fields.NickName} that was required to be updated to null, but it was already null!");
            TestSkip(sut, empty, withValue,             $"The target did not contain a required update of column {Contact.Fields.NickName} to null!");
        }

        [TestMethod]
        public void SkipExecution_MissingUpdatedValue_Should_Skip()
        {
            var withValue = new Contact { NickName = "NotNull" };
            var withNullValue = new Contact { NickName = null };
            var withEmptyString = new Contact { NickName = string.Empty };
            var empty = new Contact();
            var sut = new RequirementValidator().Updated(withValue);
            TestSkip(sut, withValue, empty);
            TestSkip(sut, withValue, withValue,       $"The target contained value \"NotNull\" for column {Contact.Fields.NickName} which it was be updated to, but it already had that value!");
            TestSkip(sut, withNullValue, withValue,   $"The target contained value null for column {Contact.Fields.NickName} that was required to be updated to \"NotNull\"!");
            TestSkip(sut, withEmptyString, withValue, $"The target contained value \"\" for column {Contact.Fields.NickName} that was required to be updated to \"NotNull\"!");
            TestSkip(sut, withNullValue, empty,       $"The target contained value null for column {Contact.Fields.NickName} that was required to be updated to \"NotNull\"!");
            TestSkip(sut, withEmptyString, empty,     $"The target contained value \"\" for column {Contact.Fields.NickName} that was required to be updated to \"NotNull\"!");
            TestSkip(sut, empty, withValue,           $"The target did not contain a required update of column {Contact.Fields.NickName} to \"NotNull\"!");
        }

        [TestMethod]
        public void SkipExecution_MissingUpdatedAny_Should_Skip()
        {
            var sut = new RequirementValidator().UpdatedAny<Contact>(c => new { c.NickName, c.AssistantName });

            foreach (var target in PossibleValues)
            {
                foreach (var preImage in PossiblePreImageValues)
                {
                    var message = string.IsNullOrWhiteSpace(target.NickName)
                                  && string.IsNullOrWhiteSpace(target.AssistantName)
                                  || (target.NickName == preImage.NickName
                                      || string.IsNullOrWhiteSpace(target.NickName) && !string.IsNullOrWhiteSpace(preImage.NickName)) 
                                  && (target.AssistantName == preImage.AssistantName
                                      || string.IsNullOrWhiteSpace(target.AssistantName) && !string.IsNullOrWhiteSpace(preImage.AssistantName))
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
                foreach (var preImage in PossiblePreImageValues)
                {
                    var message = !target.Contains(Contact.Fields.NickName)
                                  && !target.Contains(Contact.Fields.AssistantName)
                                  || (!target.Contains(Contact.Fields.NickName)
                                      || target.NickName == preImage.NickName
                                      || string.IsNullOrWhiteSpace(target.NickName) && preImage.NickName == null)
                                  && (!target.Contains(Contact.Fields.AssistantName)
                                      || target.AssistantName == preImage.AssistantName
                                      || string.IsNullOrWhiteSpace(target.AssistantName) && preImage.AssistantName == null)
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
                foreach (var preImage in PossiblePreImageValues)
                {
                    var nickNameNulled = target.Contains(Contact.Fields.NickName)
                                         && string.IsNullOrWhiteSpace(target.NickName)
                                         && preImage.NickName != null;
                    var assistantNulled = target.Contains(Contact.Fields.AssistantName)
                                          && string.IsNullOrWhiteSpace(target.AssistantName)
                                          && preImage.AssistantName != null;
                    var message = !nickNameNulled
                                  && !assistantNulled
                        ? $"The target did not update at least one of the following columns to null: {Contact.Fields.NickName}, {Contact.Fields.AssistantName}!"
                        : null;
                    TestSkip(sut, target, preImage, message);
                }
            }
        }

        #endregion Test Updated (Non-Null) / Changed (Nullable) / Cleared (Null) / Updated (Value)

        #region Missing PreImage Should Throw

        [TestMethod]
        public void SkipExecutionContains_MissingPreImage_Should_Throw()
        {
            MissingPreImage(ContextEntity.CoalesceTargetPreImage, new RequirementValidator().Contains(ContextEntity.CoalesceTargetPreImage, ContactNickName));
            MissingPreImage(ContextEntity.PreImage, new RequirementValidator().Contains(ContextEntity.PreImage, ContactNickName));
        }

        [TestMethod]
        public void SkipExecutionContainsNullable_MissingPreImage_Should_Throw()
        {
            MissingPreImage(ContextEntity.CoalesceTargetPreImage, new RequirementValidator().ContainsNullable(ContextEntity.CoalesceTargetPreImage, ContactNickName));
            MissingPreImage(ContextEntity.PreImage, new RequirementValidator().ContainsNullable(ContextEntity.PreImage, ContactNickName));
        }

        [TestMethod]
        public void SkipExecutionContainsNull_MissingPreImage_Should_Throw()
        {
            MissingPreImage(ContextEntity.CoalesceTargetPreImage, new RequirementValidator().ContainsNull(ContextEntity.CoalesceTargetPreImage, ContactNickName));
            MissingPreImage(ContextEntity.PreImage, new RequirementValidator().ContainsNull(ContextEntity.PreImage, ContactNickName));
        }

        [TestMethod]
        public void SkipExecutionContainsValues_MissingPreImage_Should_Throw()
        {
            var value = new Contact { NickName = "NotNull" };
            MissingPreImage(ContextEntity.CoalesceTargetPreImage, new RequirementValidator().Contains(ContextEntity.CoalesceTargetPreImage, value));
            MissingPreImage(ContextEntity.PreImage, new RequirementValidator().Contains(ContextEntity.PreImage, value));
        }

        [TestMethod]
        public void SkipExecutionContainsAny_MissingPreImage_Should_Throw()
        {
            MissingPreImage(ContextEntity.CoalesceTargetPreImage, new RequirementValidator().ContainsAny(ContextEntity.CoalesceTargetPreImage, ContactNickName));
            MissingPreImage(ContextEntity.PreImage, new RequirementValidator().ContainsAny(ContextEntity.PreImage, ContactNickName));
        }

        [TestMethod]
        public void SkipExecutionContainsAnyNullable_MissingPreImage_Should_Throw()
        {
            MissingPreImage(ContextEntity.CoalesceTargetPreImage, new RequirementValidator().ContainsAnyNullable(ContextEntity.CoalesceTargetPreImage, ContactNickName));
            MissingPreImage(ContextEntity.PreImage, new RequirementValidator().ContainsAnyNullable(ContextEntity.PreImage, ContactNickName));
        }

        [TestMethod]
        public void SkipExecutionContainsAnyNull_MissingPreImage_Should_Throw()
        {
            MissingPreImage(ContextEntity.CoalesceTargetPreImage, new RequirementValidator().ContainsAnyNull(ContextEntity.CoalesceTargetPreImage, ContactNickName));
            MissingPreImage(ContextEntity.PreImage, new RequirementValidator().ContainsAnyNull(ContextEntity.PreImage, ContactNickName));
        }

        [TestMethod]
        public void SkipExecutionContainsAnyValues_MissingPreImage_Should_Throw()
        {
            var value = new Contact { NickName = "NotNull" };
            MissingPreImage(ContextEntity.CoalesceTargetPreImage, new RequirementValidator().ContainsAny(ContextEntity.CoalesceTargetPreImage, value));
            MissingPreImage(ContextEntity.PreImage, new RequirementValidator().ContainsAny(ContextEntity.PreImage, value));
        }

        #endregion Missing PreImage Should Throw

        private void TestSkip(RequirementValidator sut, Entity target, Entity preImage, string? trace = null)
        {
            TestSkip(sut, target, preImage, trace, MessageType.Create);
            TestSkip(sut, target, preImage, trace, MessageType.Update);
        }

        private static void TestSkip(RequirementValidator sut, Entity target, Entity preImage, string? trace, MessageType message)
        {
            var context = new FakeContext
            {
                Target = target,
                PreImage = message == MessageType.Update ? preImage : null,
                MessageName = message
            };

            if (message == MessageType.Create)
            {
                if (preImage.Attributes.Any(a => a.Value != null))
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

        private static void TestContains(ContextEntity contextEntity)
        {
            var sut = new RequirementValidator().Contains<Contact>(contextEntity, c => new { c.NickName });
            var context = GetContext(contextEntity, new Contact());
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual($"The {contextEntity} entity type did not contain the required column {Contact.Fields.NickName}!", context.FakeTraceService.Traces.Single().Trace);
            context = GetContext(contextEntity, new Contact { NickName = null });
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual($"The {contextEntity} entity type contained a null value for the required column {Contact.Fields.NickName}!", context.FakeTraceService.Traces.Single().Trace);
            context = GetContext(contextEntity, new Contact { NickName = string.Empty });
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual($"The {contextEntity} entity type contained a null value for the required column {Contact.Fields.NickName}!", context.FakeTraceService.Traces.Single().Trace);

            AssertValid(sut, contextEntity, new Contact { NickName = "NotNull" });

            sut = new RequirementValidator().Contains<Contact>(contextEntity, c => new { c.NickName, c.AssistantName });
            context = GetContext(contextEntity, new Contact());
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual($"The {contextEntity} entity type did not contain the required column {Contact.Fields.NickName}!", context.FakeTraceService.Traces.Single().Trace);
            foreach(var possibility in new Contact[] {
                new() { NickName = null, AssistantName = null},
                new() { NickName = string.Empty, AssistantName = null},
                new() { NickName = null, AssistantName = string.Empty},
                new() { NickName = string.Empty, AssistantName = string.Empty}})
            {
                context = GetContext(contextEntity, possibility);
                Assert.IsTrue(sut.SkipExecution(context));
                var firstMissingField = string.IsNullOrWhiteSpace(possibility.NickName) ? Contact.Fields.NickName : Contact.Fields.AssistantName;
                Assert.AreEqual($"The {contextEntity} entity type contained a null value for the required column {firstMissingField}!", context.FakeTraceService.Traces.Single().Trace);
            }
            context = GetContext(contextEntity, new Contact{ NickName = "NotNull" });
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual($"The {contextEntity} entity type did not contain the required column { Contact.Fields.AssistantName}!", context.FakeTraceService.Traces.Single().Trace);
            context = GetContext(contextEntity, new Contact { NickName = "NotNull", AssistantName = null });
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual($"The {contextEntity} entity type contained a null value for the required column {Contact.Fields.AssistantName}!", context.FakeTraceService.Traces.Single().Trace);

            AssertValid(sut, contextEntity, new Contact { NickName = "NotNull", AssistantName = "NotNull" });
        }

        private static void TestContainsNullable(ContextEntity contextEntity)
        {
            var sut = new RequirementValidator().ContainsNullable<Contact>(contextEntity, c => new { c.NickName });
            var context = GetContext(contextEntity, new Contact());
            if (contextEntity is ContextEntity.PreImage or ContextEntity.CoalesceTargetPreImage)
            {
                // Contains Null For PreImages Should Allow Not Contains #50
                AssertValid(sut, contextEntity, new Contact());
            }
            else
            {
                Assert.IsTrue(sut.SkipExecution(context));
                Assert.AreEqual($"The {contextEntity} entity type did not contain the required column {Contact.Fields.NickName}!", context.FakeTraceService.Traces.Single().Trace);
            }

            AssertValid(sut, contextEntity, new Contact { NickName = null });
            AssertValid(sut, contextEntity, new Contact { NickName = string.Empty });
            AssertValid(sut, contextEntity, new Contact { NickName = "NotNull" });
        }

        private static void TestContainsNull(ContextEntity contextEntity)
        {
            var sut = new RequirementValidator().ContainsNull<Contact>(contextEntity, c => new { c.NickName });
            var context = GetContext(contextEntity, new Contact());
            if (contextEntity is ContextEntity.PreImage or ContextEntity.CoalesceTargetPreImage)
            {
                // Contains Null For PreImages Should Allow Not Contains #50
                AssertValid(sut, contextEntity, new Contact());
            }
            else
            {
                Assert.IsTrue(sut.SkipExecution(context));
                Assert.AreEqual($"The {contextEntity} entity type did not contain the required to be null column {Contact.Fields.NickName}!", context.FakeTraceService.Traces.Single().Trace);
            }

            AssertValid(sut, contextEntity, new Contact { NickName = null });
            AssertValid(sut, contextEntity, new Contact { NickName = string.Empty });
            context = GetContext(contextEntity, new Contact { NickName = "NotNull" });
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual($"The {contextEntity} entity type contained a non-null value for the required to be null column {Contact.Fields.NickName}!", context.FakeTraceService.Traces.Single().Trace);
        }

        private static void TestContainsValue(ContextEntity contextEntity)
        {
            var sut = new RequirementValidator().Contains(contextEntity, new Contact{ NickName = "NotNull", ManagerName = null });
            var context = GetContext(contextEntity, new Contact());
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual($"The {contextEntity} entity type did not contain a value for column {Contact.Fields.NickName} which was required to be \"NotNull\"!", context.FakeTraceService.Traces.Single().Trace);
            context = GetContext(contextEntity, new Contact { NickName = null });
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual($"The {contextEntity} entity type was required to contain a value of \"NotNull\" for column nickname but contained the value null!", context.FakeTraceService.Traces.Single().Trace);
            if (contextEntity is ContextEntity.PreImage or ContextEntity.CoalesceTargetPreImage)
            {
                AssertValid(sut, contextEntity, new Contact { NickName = "NotNull" });
            }
            else
            {
                context = GetContext(contextEntity, new Contact { NickName = "NotNull" });
                Assert.IsTrue(sut.SkipExecution(context));
                Assert.AreEqual($"The {contextEntity} entity type did not contain a value for column managername which was required to be null!", context.FakeTraceService.Traces.Single().Trace);
            }
            AssertValid(sut, contextEntity, new Contact { NickName = "NotNull", ManagerName = null });
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
            AssertValid(sut, contextEntity, new Contact { NickName = "NotNull" });
            AssertValid(sut, contextEntity, new Contact { AssistantName = "NotNull" });
            AssertValid(sut, contextEntity, new Contact { NickName = "NotNull", AssistantName = "NotNull" });
        }

        private static void TestContainsAnyNullable(ContextEntity contextEntity)
        {
            var sut = new RequirementValidator().ContainsAnyNullable<Contact>(contextEntity, c => new { c.NickName, c.AssistantName });
            if (contextEntity is ContextEntity.PreImage or ContextEntity.CoalesceTargetPreImage)
            {
                // Contains Null For PreImages Should Allow Not Contains #50
                AssertValid(sut, contextEntity, new Contact());
                AssertValid(sut, contextEntity, new Contact { NickName = null });
                AssertValid(sut, contextEntity, new Contact { AssistantName = null });
            }
            else
            {
                var context = GetContext(contextEntity, new Contact());
                Assert.IsTrue(sut.SkipExecution(context));
                Assert.AreEqual($"The {contextEntity} entity type did not contain at least one of the following columns: {Contact.Fields.NickName}, {Contact.Fields.AssistantName}!", context.FakeTraceService.Traces.Single().Trace);

                AssertValid(sut, contextEntity, new Contact { NickName = null });
                AssertValid(sut, contextEntity, new Contact { AssistantName = null });
            }

            AssertValid(sut, contextEntity, new Contact { NickName = null, AssistantName = null });
        }

        private static void TestContainsAnyNull(ContextEntity contextEntity)
        {
            var sut = new RequirementValidator().ContainsAnyNull<Contact>(contextEntity, c => new { c.NickName, c.AssistantName });
            var expectedSkipMessage = $"The {contextEntity} entity type did not contain a null value for at least one of the following columns: {Contact.Fields.NickName}, {Contact.Fields.AssistantName}!";
            var context = GetContext(contextEntity, new Contact());
            if (contextEntity is ContextEntity.PreImage or ContextEntity.CoalesceTargetPreImage)
            {
                // Contains Null For PreImages Should Allow Not Contains #50
                AssertValid(sut, contextEntity, new Contact());
                AssertValid(sut, contextEntity, new Contact { NickName = "NotNull" });
                AssertValid(sut, contextEntity, new Contact { AssistantName = "NotNull" });
            }
            else
            {
                Assert.IsTrue(sut.SkipExecution(context));
                Assert.AreEqual(expectedSkipMessage, context.FakeTraceService.Traces.Single().Trace);
                context = GetContext(contextEntity, new Contact { NickName = "NotNull" });
                Assert.IsTrue(sut.SkipExecution(context));
                Assert.AreEqual(expectedSkipMessage, context.FakeTraceService.Traces.Single().Trace);
                context = GetContext(contextEntity, new Contact { AssistantName = "NotNull" });
                Assert.IsTrue(sut.SkipExecution(context));
                Assert.AreEqual(expectedSkipMessage, context.FakeTraceService.Traces.Single().Trace);
            }

            AssertValid(sut, contextEntity, new Contact { NickName = null });
            AssertValid(sut, contextEntity, new Contact { AssistantName = null });
            AssertValid(sut, contextEntity, new Contact { NickName = null, AssistantName = null });
            context = GetContext(contextEntity, new Contact { NickName = "NotNull", AssistantName = "NotNull" });
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual(expectedSkipMessage, context.FakeTraceService.Traces.Single().Trace);
        }

        private static void TestContainsAnyValue(ContextEntity contextEntity)
        {
            var sut = new RequirementValidator().ContainsAny(contextEntity, new Contact{ NickName = "NotNull", AssistantName = "NotNull", ManagerName = null});
            var expectedSkipMessage = $"The {contextEntity} entity type did not contain the required value for at least one of the following columns: {Contact.Fields.NickName}, {Contact.Fields.AssistantName}, {Contact.Fields.ManagerName}!";
            var missingManagerNameContacts = new List<Contact>
            {
                new(),
                new() { NickName = null },
                new() { NickName = string.Empty },
                new() { AssistantName = null },
                new() { AssistantName = string.Empty },
                new() { NickName = null, AssistantName = null },
                new() { NickName = null, AssistantName = string.Empty  },
                new() { NickName = string.Empty, AssistantName = null },
                new() { NickName = string.Empty, AssistantName = string.Empty  },
            };
            FakeContext context;
            if (contextEntity is ContextEntity.PreImage or ContextEntity.CoalesceTargetPreImage)
            {
                // Contains Null For PreImages Should Allow Not Contains #50
                foreach (var contact in missingManagerNameContacts)
                {
                    AssertValid(sut, contextEntity, contact);
                }

                foreach (var contact in missingManagerNameContacts)
                {
                    contact.ManagerName = "NotNull";
                    context = GetContext(contextEntity, contact);
                    Assert.IsTrue(sut.SkipExecution(context));
                    Assert.AreEqual(expectedSkipMessage, context.FakeTraceService.Traces.Single().Trace);
                }
            }
            else
            {
                foreach (var contact in missingManagerNameContacts)
                {
                    context = GetContext(contextEntity, contact);
                    Assert.IsTrue(sut.SkipExecution(context));
                    Assert.AreEqual(expectedSkipMessage, context.FakeTraceService.Traces.Single().Trace);
                }
            }

            AssertValid(sut, contextEntity, new Contact { NickName = "NotNull" });
            AssertValid(sut, contextEntity, new Contact { AssistantName = "NotNull" });
            AssertValid(sut, contextEntity, new Contact { NickName = "NotNull", AssistantName = "NotNull" });
        }

        private static void TestMissing(ContextEntity contextEntity)
        {
            var sut = new RequirementValidator().Missing<Contact>(contextEntity, c => new { c.NickName });
            var context = GetContext(contextEntity, new Contact { NickName = null });
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual($"The {contextEntity} entity type contains column {Contact.Fields.NickName} that was expected to be missing!", context.FakeTraceService.Traces.Single().Trace);
            context = GetContext(contextEntity, new Contact { NickName = "NotNull" });
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual($"The {contextEntity} entity type contains column {Contact.Fields.NickName} that was expected to be missing!", context.FakeTraceService.Traces.Single().Trace);
            
            AssertValid(sut, contextEntity, new Contact());

            sut = new RequirementValidator().Missing<Contact>(contextEntity, c => new { c.NickName, c.AssistantName });
            context = GetContext(contextEntity, new Contact { NickName = "NotNull", AssistantName = "NotNull" });
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual($"The {contextEntity} entity type contains columns {Contact.Fields.NickName}, {Contact.Fields.AssistantName} that were expected to be missing!", context.FakeTraceService.Traces.Single().Trace);
            context = GetContext(contextEntity, new Contact { NickName = null, AssistantName = null });
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual($"The {contextEntity} entity type contains columns {Contact.Fields.NickName}, {Contact.Fields.AssistantName} that were expected to be missing!", context.FakeTraceService.Traces.Single().Trace);
            context = GetContext(contextEntity, new Contact { NickName = "NotNull" });
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual($"The {contextEntity} entity type contains column {Contact.Fields.NickName} that was expected to be missing!", context.FakeTraceService.Traces.Single().Trace);
            context = GetContext(contextEntity, new Contact { NickName = "NotNull", AssistantName = null });
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual($"The {contextEntity} entity type contains columns {Contact.Fields.NickName}, {Contact.Fields.AssistantName} that were expected to be missing!", context.FakeTraceService.Traces.Single().Trace);

            AssertValid(sut, contextEntity, new Contact());
        }

        private static void TestMissingAny(ContextEntity contextEntity)
        {
            var sut = new RequirementValidator().MissingAny<Contact>(contextEntity, c => new { c.NickName, c.AssistantName });
            var context = GetContext(contextEntity, new Contact { NickName = "NotNull", AssistantName = "NotNull" });
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual($"The {contextEntity} entity type contained all columns ({Contact.Fields.NickName}, {Contact.Fields.AssistantName}) when at least one was expected to be missing!", context.FakeTraceService.Traces.Single().Trace);
            context = GetContext(contextEntity, new Contact { NickName = null, AssistantName = null });
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual($"The {contextEntity} entity type contained all columns ({Contact.Fields.NickName}, {Contact.Fields.AssistantName}) when at least one was expected to be missing!", context.FakeTraceService.Traces.Single().Trace);

            AssertValid(sut, contextEntity, new Contact());
            AssertValid(sut, contextEntity, new Contact { NickName = null });
            AssertValid(sut, contextEntity, new Contact { NickName = string.Empty });
            AssertValid(sut, contextEntity, new Contact { NickName = "NotNull" });
            AssertValid(sut, contextEntity, new Contact { AssistantName = null });
            AssertValid(sut, contextEntity, new Contact { AssistantName = string.Empty });
            AssertValid(sut, contextEntity, new Contact { AssistantName = "NotNull" });
        }

        private static void TestMissingOrNullAny(ContextEntity contextEntity)
        {
            var sut = new RequirementValidator().MissingOrNullAny<Contact>(contextEntity, c => new { c.NickName, c.AssistantName });
            var context = GetContext(contextEntity, new Contact { NickName = "NotNull", AssistantName = "NotNull" });
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual($"The {contextEntity} entity type contained all columns ({Contact.Fields.NickName}, {Contact.Fields.AssistantName}) when at least one was expected to be missing or null!", context.FakeTraceService.Traces.Single().Trace);

            AssertValid(sut, contextEntity, new Contact());
            AssertValid(sut, contextEntity, new Contact { NickName = null });
            AssertValid(sut, contextEntity, new Contact { NickName = string.Empty });
            AssertValid(sut, contextEntity, new Contact { NickName = "NotNull" });
            AssertValid(sut, contextEntity, new Contact { AssistantName = null });
            AssertValid(sut, contextEntity, new Contact { AssistantName = string.Empty });
            AssertValid(sut, contextEntity, new Contact { AssistantName = "NotNull" });
            AssertValid(sut, contextEntity, new Contact { NickName = null, AssistantName = null });
            AssertValid(sut, contextEntity, new Contact { NickName = string.Empty, AssistantName = null });
            AssertValid(sut, contextEntity, new Contact { NickName = null, AssistantName = string.Empty });
            AssertValid(sut, contextEntity, new Contact { NickName = string.Empty, AssistantName = string.Empty });
        }
        private static void TestMissingOrNull(ContextEntity contextEntity)
        {
            var sut = new RequirementValidator().MissingOrNull<Contact>(contextEntity, c => new { c.NickName });
            var context = GetContext(contextEntity, new Contact { NickName = "NotNull" });
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual($"The {contextEntity} entity type contains column {Contact.Fields.NickName} that was expected to be missing or null!", context.FakeTraceService.Traces.Single().Trace);
            
            AssertValid(sut, contextEntity, new Contact { NickName = null });
            AssertValid(sut, contextEntity, new Contact { NickName = string.Empty });
            AssertValid(sut, contextEntity, new Contact());

            sut = new RequirementValidator().MissingOrNull<Contact>(contextEntity, c => new { c.NickName, c.AssistantName });
            context = GetContext(contextEntity, new Contact { NickName = "NotNull", AssistantName = "NotNull" });
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual($"The {contextEntity} entity type contains columns {Contact.Fields.NickName}, {Contact.Fields.AssistantName} that were expected to be missing or null!", context.FakeTraceService.Traces.Single().Trace);
            context = GetContext(contextEntity, new Contact { NickName = "NotNull" });
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual($"The {contextEntity} entity type contains column {Contact.Fields.NickName} that was expected to be missing or null!", context.FakeTraceService.Traces.Single().Trace);
            context = GetContext(contextEntity, new Contact { NickName = "NotNull", AssistantName = null });
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual($"The {contextEntity} entity type contains column {Contact.Fields.NickName} that was expected to be missing or null!", context.FakeTraceService.Traces.Single().Trace);

            context = GetContext(contextEntity, new Contact { NickName = "NotNull", AssistantName = string.Empty });
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual($"The {contextEntity} entity type contains column {Contact.Fields.NickName} that was expected to be missing or null!", context.FakeTraceService.Traces.Single().Trace);

            AssertValid(sut, contextEntity, new Contact { NickName = null, AssistantName = null });
            AssertValid(sut, contextEntity, new Contact { NickName = string.Empty, AssistantName = null });
            AssertValid(sut, contextEntity, new Contact { NickName = null, AssistantName = string.Empty });
            AssertValid(sut, contextEntity, new Contact { NickName = string.Empty, AssistantName = string.Empty });
            AssertValid(sut, contextEntity, new Contact()); 
        }

        private static void MissingPreImage(ContextEntity contextEntity, RequirementValidator sut)
        {
            try
            {
                sut.SkipExecution(new FakeContext
                {
                    MessageName = MessageType.Update
                });
                Assert.Fail("SkipExecutions should have thrown an exception!");
            }
            catch (InvalidPluginExecutionException ex)
            {
                Assert.AreEqual("A pre-image was required but not found!  Expected a pre-image to be registered for this step with the following columns: nickname", ex.Message);
            }

            sut.SkipExecution(GetContext(contextEntity, new Contact()));
        }

        private static void TestNotValue(ContextEntity contextEntity)
        {
            var sut = new RequirementValidator().Not(contextEntity, new Contact { NickName = "NotNull", ManagerName = "Not Null" });
            var context = GetContext(contextEntity, new Contact { NickName = "NotNull", ManagerName = "Not Null" });
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual($"The {contextEntity} entity type matched all invalid values: {Contact.Fields.NickName}: \"NotNull\", {Contact.Fields.ManagerName}: \"Not Null\"!", context.FakeTraceService.Traces.Single().Trace);
            
            context = GetContext(contextEntity, new Contact { NickName = "NotNull", ManagerName = null });
            Assert.IsFalse(sut.SkipExecution(context));

            context = GetContext(contextEntity, new Contact { NickName = "NotNull", ManagerName = string.Empty });
            Assert.IsFalse(sut.SkipExecution(context));
        }

        private static void TestNotAnyValue(ContextEntity contextEntity)
        {
            var sut = new RequirementValidator().NotAny(contextEntity, new Contact { NickName = "NotNull", ManagerName = "Not Null" });
            var context = GetContext(contextEntity, new Contact { NickName = "NotNull", ManagerName = "Not Null" });
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual($"The {contextEntity} entity type contained at least one invalid value for columns {Contact.Fields.NickName}, {Contact.Fields.ManagerName} which was not allowed!", context.FakeTraceService.Traces.Single().Trace);

            context = GetContext(contextEntity, new Contact { NickName = "NotNull", ManagerName = null });
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual($"The {contextEntity} entity type contained at least one invalid value for columns {Contact.Fields.NickName}, {Contact.Fields.ManagerName} which was not allowed!", context.FakeTraceService.Traces.Single().Trace);

            context = GetContext(contextEntity, new Contact { NickName = "NotNull", ManagerName = string.Empty });
            Assert.IsTrue(sut.SkipExecution(context));
            Assert.AreEqual($"The {contextEntity} entity type contained at least one invalid value for columns {Contact.Fields.NickName}, {Contact.Fields.ManagerName} which was not allowed!", context.FakeTraceService.Traces.Single().Trace);

            context = GetContext(contextEntity, new Contact { NickName = "Not Null", ManagerName = "NotNull" });
            Assert.IsFalse(sut.SkipExecution(context));
        }

        private static void AssertValid(RequirementValidator sut, ContextEntity contextEntity, Contact root)
        {
            var context = GetContext(contextEntity, root);
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
