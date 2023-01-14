#if NET
using DataverseUnitTest;
#endif
using DLaB.Xrm.Entities;
using DLaB.Xrm.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk.Query;
using Source.DLaB.Xrm;

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
    }
}
