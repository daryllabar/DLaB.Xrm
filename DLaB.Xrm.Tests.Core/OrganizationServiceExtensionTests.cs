#if NET
using DataverseUnitTest;
#else
#endif

using System;
using DLaB.Xrm.Entities;
using DLaB.Xrm.Test;
using DLaB.Xrm.Test.Core.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using OrganizationServiceBuilder = DLaB.Xrm.Test.Core.Builders.OrganizationServiceBuilder;

namespace DLaB.Xrm.Tests.Core
{
    [TestClass]
    public class OrganizationServiceExtensionTests
    {
        [TestMethod]
        public void TestAcquireLock_WithEmail()
        {
            TestInitializer.InitializeTestSettings();
            ConditionExpression condition = null;
            Email updatedEntity = null;
            var service = (IOrganizationService)TestBase.GetOrganizationService();
            var email = new Email();
            email.Id = service.Create(email);
            service = new OrganizationServiceBuilder(service)
                .WithFakeRetrieveMultiple((s, query) =>
                {
                    var qe = (QueryExpression)query;
                    Assert.AreEqual(1, qe.Criteria.Conditions.Count);
                    condition = qe.Criteria.Conditions[0];
                    qe.Criteria.Conditions.Clear();
                    qe.Criteria.AddCondition(condition.AttributeName, ConditionOperator.OnOrAfter, condition.Values[0]);
                    return s.RetrieveMultiple(qe);
                }).WithFakeUpdateForEntity(email.ToEntityReference(), e =>
                {
                    updatedEntity = e.ToEntity<Email>();
                }).Build();

            service.AcquireLock("notify@me.com");

            var date = (DateTime)condition.Values[0];
            Assert.IsTrue(DateTime.UtcNow.AddDays(-366) < date);
            Assert.IsTrue(date < DateTime.UtcNow);
            Assert.IsTrue(updatedEntity.Contains(Email.Fields.TransactionCurrencyId));
            Assert.IsNull(updatedEntity.TransactionCurrencyId);
        }

        [TestMethod]
        public void TestAcquireLock_FallBackToBusinessUnit()
        {
            TestInitializer.InitializeTestSettings();
            BusinessUnit updatedEntity = null;
            var service = (IOrganizationService)TestBase.GetOrganizationService();
            var bu = service.GetFirst<BusinessUnit>();
            bu.Address2_Fax = "123-456-7890";
            service.Update(bu);
            service = new OrganizationServiceBuilder(service)
                .WithFakeUpdateForEntity(bu.ToEntityReference(), _ =>
                {
                    updatedEntity = bu.ToEntity<BusinessUnit>();
                }).Build();

            service.AcquireLock("notify@me.com");

            Assert.AreEqual(bu.Address2_Fax, updatedEntity.Address2_Fax);
        }

        #region AcquireLock_Should_UpdateSpecificEmail

        [TestMethod]
        public void AcquireLock_Should_UpdateSpecificEmail()
        {
            new Should_UpdateSpecificEmail().Test();
        }

        // ReSharper disable once InconsistentNaming
        private class Should_UpdateSpecificEmail : TestMethodClassBase
        {
            /// <summary>
            /// The string "ABC" returned 360 as the random value with the hash code
            /// </summary>
            private const double ExpectedDayDiff = -360d;
            private struct Ids
            {
                public struct Emails
                {
                    public static readonly Id<Email> A = new Id<Email>("73E70E4F-B81F-400A-A9A0-3371043E4BFC");
                    public static readonly Id<Email> B = new Id<Email>("4EF2E668-FCE2-4ABE-B67C-A355B370678C");
                    public static readonly Id<Email> C = new Id<Email>("CC831270-0A63-41B0-8F86-F6A055B837CD");
                }
            }

            protected override void InitializeTestData(IOrganizationService service)
            {
                var oldest = DateTime.UtcNow.AddDays(ExpectedDayDiff - 1).Date;
                Ids.Emails.A.Entity.OverriddenCreatedOn = oldest;
                Ids.Emails.B.Entity.OverriddenCreatedOn = oldest.AddDays(1);
                Ids.Emails.C.Entity.OverriddenCreatedOn = oldest.AddDays(2);
                new CrmEnvironmentBuilder().WithEntities<Ids>().Create(service);
            }

            protected override void Test(IOrganizationService service)
            {
                Email updatedEmail = null;
                service = new OrganizationServiceBuilder(service)
                    .WithFakeUpdate((_, e) =>
                    {
                        updatedEmail = e.ToEntity<Email>();
                    }).Build();
                service.AcquireLock("ABC");

                Assert.IsNotNull(updatedEmail);
                Assert.AreEqual(Ids.Emails.B.Entity.Id, updatedEmail.Id);
            }
        }

        #endregion GetLock_Should_UpdateSpecificEmail    
    }
}
