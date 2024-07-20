#if NET
extern alias DLaBXrm;
using DataverseUnitTest;
using DataverseUnitTest.Builders;
#else
using DLaB.Xrm.Test.Builders;
using Source.DLaB.Xrm;
#endif

using System;
using DLaB.Xrm.Entities;
using DLaB.Xrm.Test;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;

namespace DLaB.Xrm.Tests.Core
{
    [TestClass]
    public class ReadOnlyCachedServiceTests
    {
        [TestMethod]
        public void Retrieve_Should_CacheValue()
        {
            TestInitializer.InitializeTestSettings();
            var service = TestBase.GetOrganizationService();
            var id = service.Create(new Contact());
            var sut = new ReadOnlyCachedService(service);
            var contact = sut.GetEntity<Contact>(id);
            contact.FirstName = "First";
            Assert.IsNotNull(contact);
            service.Delete(contact);
            var contact2 = sut.GetEntity<Contact>(id);
            Assert.AreNotEqual(contact.FirstName, contact2.FirstName);
        }

        [TestMethod]
        public void RetrieveMultiple_Should_CacheValues()
        {
            TestInitializer.InitializeTestSettings();
            var service = TestBase.GetOrganizationService();
            service.Create(new Contact{ FirstName = "1"});
            service.Create(new Contact { FirstName = "2" });
            var sut = new ReadOnlyCachedService(service);
            
            var contacts = sut.GetEntities<Contact>();
            Assert.AreEqual(2, contacts.Count);
            service.Delete(contacts[0]);
            service.Delete(contacts[1]);
            
            contacts = sut.GetEntities<Contact>();
            Assert.AreEqual(2, contacts.Count);
            Assert.AreEqual("1", contacts[0].FirstName);
            Assert.AreEqual("2", contacts[1].FirstName);
            
            // Different Select (Query Expression changes to Select *)
            contacts = sut.GetEntities<Contact>(c => new { c.FirstName });
            Assert.AreEqual(2, contacts.Count);

            // Different Where
            contacts = sut.GetEntities<Contact>(new ConditionExpression(Contact.Fields.FirstName, ConditionOperator.NotNull));
            Assert.AreEqual(0, contacts.Count);
        }

        [TestMethod]
        public void RetrieveEntityRequest_Should_CacheValue()
        {
            TestInitializer.InitializeTestSettings();
            var contactCount = 0;
            var nonContactCount = 0;
            var service = new OrganizationServiceBuilder(TestBase.GetOrganizationService())
                .WithFakeExecute((_, r) =>
                {
                    var name = ((RetrieveEntityRequest)r).LogicalName;
                    if (name == Contact.EntityLogicalName)
                    {
                        contactCount++;
                    }
                    else
                    {
                        nonContactCount++;
                    }

                    var retrieveResponse = new RetrieveEntityResponse();
                    retrieveResponse.Results.Add(nameof(RetrieveEntityResponse.EntityMetadata), new EntityMetadata
                    {
                        LogicalName = name,
                    });
                    return retrieveResponse;
                }).Build();


            var response = (RetrieveEntityResponse)service.Execute(new RetrieveEntityRequest{ LogicalName = Contact.EntityLogicalName });
            Assert.AreEqual(Contact.EntityLogicalName, response.EntityMetadata.LogicalName);
            Assert.AreEqual(1, contactCount);
            Assert.AreEqual(0, nonContactCount);

            var sut = new ReadOnlyCachedService(service);
            response = (RetrieveEntityResponse)sut.Execute(new RetrieveEntityRequest { LogicalName = Contact.EntityLogicalName });
            Assert.AreEqual(Contact.EntityLogicalName, response.EntityMetadata.LogicalName);
            Assert.AreEqual(2, contactCount);
            Assert.AreEqual(0, nonContactCount);

            // Cached
            response = (RetrieveEntityResponse)sut.Execute(new RetrieveEntityRequest { LogicalName = Contact.EntityLogicalName });
            Assert.AreEqual(Contact.EntityLogicalName, response.EntityMetadata.LogicalName);
            Assert.AreEqual(2, contactCount);
            Assert.AreEqual(0, nonContactCount);

            // Different Entity, new request that gets cached
            response = (RetrieveEntityResponse)sut.Execute(new RetrieveEntityRequest { LogicalName = Account.EntityLogicalName });
            Assert.AreEqual(Account.EntityLogicalName, response.EntityMetadata.LogicalName);
            Assert.AreEqual(2, contactCount);
            Assert.AreEqual(1, nonContactCount);

            // Cached
            response = (RetrieveEntityResponse)sut.Execute(new RetrieveEntityRequest { LogicalName = Account.EntityLogicalName });
            Assert.AreEqual(Account.EntityLogicalName, response.EntityMetadata.LogicalName);
            Assert.AreEqual(2, contactCount);
            Assert.AreEqual(1, nonContactCount);
        }

        [TestMethod]
        public void NonRetrieveRequest_Should_Error()
        {
            TestInitializer.InitializeTestSettings();
            var service = TestBase.GetOrganizationService();
            var sut = new ReadOnlyCachedService(service);

            try
            {
                sut.Execute(new DeleteEntityRequest());
            }
            catch (NotImplementedException)
            {
                return;
            }
            Assert.Fail("Should have thrown NotImplementedException!");
        }

                [TestMethod]
        public void RetrievePrincipalAccessResponse_Should_Cache()
        {
            TestInitializer.InitializeTestSettings();
            var count = 0;
            var service = new OrganizationServiceBuilder(TestBase.GetOrganizationService())
                .WithFakeExecute((s, r) =>
                {
                    count++;
                    return new RetrievePrincipalAccessResponse();
                }).Build();
            var sut = new ReadOnlyCachedService(service);

            Assert.AreEqual(0, count);
            var request1 = new RetrievePrincipalAccessRequest
            {
                Principal = new EntityReference(SystemUser.EntityLogicalName, Guid.NewGuid()),
                Target = new EntityReference(Contact.EntityLogicalName, Guid.NewGuid())
            };
            var request2 = new RetrievePrincipalAccessRequest
            {
                Principal = new EntityReference(SystemUser.EntityLogicalName, Guid.NewGuid()),
                Target = request1.Target
            };

            sut.Execute(request1);
            Assert.AreEqual(1, count);
            sut.Execute(request1);
            Assert.AreEqual(1, count);
            sut.Execute(request2);
            Assert.AreEqual(2, count);
            sut.Execute(request2);
            Assert.AreEqual(2, count);
            sut.Execute(request1);
            Assert.AreEqual(2, count);
        }
    }
}
