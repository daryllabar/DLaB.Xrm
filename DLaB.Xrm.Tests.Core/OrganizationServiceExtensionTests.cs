#if NET
using DataverseUnitTest;
using DataverseUnitTest.Builders;
#else
using DLaB.Xrm.Test.Builders;
#endif

using System;
using DLaB.Xrm.Entities;
using DLaB.Xrm.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Source.DLaB.Xrm;

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
                .WithFakeUpdateForEntity(bu.ToEntityReference(), e =>
                {
                    updatedEntity = bu.ToEntity<BusinessUnit>();
                }).Build();

            service.AcquireLock("notify@me.com");

            Assert.AreEqual(bu.Address2_Fax, updatedEntity.Address2_Fax);
        }
    }
}
