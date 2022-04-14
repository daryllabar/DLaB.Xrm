using System;
using System.Collections.Generic;
using DLaB.Xrm.Entities;
using DLaB.Xrm.Test;
using DLaB.Xrm.Test.Core.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Source.DLaB.Xrm;

namespace Core.DLaB.Xrm.Tests
{
    [TestClass]
    public class ExtensionsIOrganizationServiceTests
    {
        #region GetEntityOrDefault

        [TestMethod]
        public void Extensions_IOrganizationService_GetEntityOrDefault()
        {
            new GetEntityOrDefault().Test();
        }

        // ReSharper disable once InconsistentNaming
        private class GetEntityOrDefault : TestMethodClassBase
        {
            private struct Ids
            {
                public static readonly Id<Account> Account  = new Id<Account>("9A9722A0-C8E9-40C2-9A59-F93303BEB272");
                public static readonly Id<Contact> Contact = new Id<Contact>("62A1E913-EA48-4141-98AC-BB9995A9A22D");
            }

            protected override void InitializeTestData(IOrganizationService service)
            {
                Ids.Contact.Entity.MobilePhone = "888-999-2222";
                new CrmEnvironmentBuilder().WithChildEntities(Ids.Account, Ids.Contact).Create(service);
            }

            protected override void Test(IOrganizationService service)
            {
                // Test Exists
                var keys = new KeyAttributeCollection { { Contact.Fields.MobilePhone, Ids.Contact.Entity.MobilePhone } };
                TestExists(service, keys);
                var entityRefKey = new KeyAttributeCollection { { Contact.Fields.ParentCustomerId, Ids.Account.EntityReference } };
                TestExists(service, entityRefKey);
                var guidKey = new KeyAttributeCollection { { Contact.Fields.ParentCustomerId, Ids.Account.EntityId} };
                TestExists(service, guidKey);
                var stringKey = new KeyAttributeCollection { { Contact.Fields.ParentCustomerId, Ids.Account.EntityId.ToString() } };
                TestExists(service, stringKey);

                // Test Not Exists
                service.Delete((Id)Ids.Contact);
                TestNotExists(service, keys);
                TestNotExists(service, entityRefKey);
                TestNotExists(service, guidKey);
                TestNotExists(service, stringKey);
            }

            private static void TestNotExists(IOrganizationService service, KeyAttributeCollection keys)
            {
                Contact contact;
                Entity entity;
                contact = service.GetEntityOrDefault<Contact>(keys);
                Assert.IsNull(contact);

                // Test NonGeneric
                entity = service.GetEntityOrDefault(Contact.EntityLogicalName, keys);
                Assert.IsNull(entity);
            }

            private static void TestExists(IOrganizationService service, KeyAttributeCollection keys)
            {
                var contact = service.GetEntityOrDefault<Contact>(keys);
                Assert.IsNotNull(contact);

                // Test NonGeneric
                var entity = service.GetEntityOrDefault(Contact.EntityLogicalName, keys);
                Assert.IsNotNull(entity);
            }
        }

        #endregion GetEntityOrDefault

        #region First

        [TestMethod]
        public void Extensions_IOrganizationService_First()
        {
            new First().Test();
        }

        // ReSharper disable once InconsistentNaming
        private class First : TestMethodClassBase
        {
            private struct Ids
            {
                public static readonly Id<Contact> Contact = new Id<Contact>("9B53265E-8A29-4109-B62C-67A007AD3DAA");
            }

            protected override void InitializeTestData(IOrganizationService service)
            {
                new CrmEnvironmentBuilder().WithEntities<Ids>().Create(service);
            }

            protected override void Test(IOrganizationService service)
            {
                // Test Exists EarlyBound
                var contact = service.GetFirst<Contact>();
                Assert.IsNotNull(contact);

                // Test Exists LateBound
                var entity = service.GetFirst(Contact.EntityLogicalName, Contact.Fields.ContactId, contact.Id);
                Assert.IsNotNull(entity);

                // Test Not Exists EarlyBound
                service.Delete((Id)Ids.Contact);
                var noError = false;
                try
                {
                    service.GetFirst<Contact>();
                    noError = true;
                }
                catch (InvalidOperationException ex)
                {
                    Assert.IsTrue(ex.Message.StartsWith("No contact found where"));
                }

                if (noError)
                {
                    Assert.Fail("Exception Expected!");
                }

                // Test Not Exists Latebound
                try
                {
                    service.GetFirst(Contact.EntityLogicalName, Contact.Fields.ContactId, contact.Id);
                    noError = true;
                }
                catch (InvalidOperationException ex)
                {
                    Assert.IsTrue(ex.Message.StartsWith("No contact found where"));
                }

                if (noError)
                {
                    Assert.Fail("Exception Expected!");
                }
            }
        }

        #endregion First

        #region FirstOrDefault

        [TestMethod]
        public void Extensions_IOrganizationService_FirstOrDefault()
        {
            new FirstOrDefault().Test();
        }

        // ReSharper disable once InconsistentNaming
        private class FirstOrDefault : TestMethodClassBase
        {
            private struct Ids
            {
                public static readonly Id<Contact> Contact = new Id<Contact>("A19CFF4C-5599-4BD4-B24A-759A50643BB3");
            }

            protected override void InitializeTestData(IOrganizationService service)
            {
                new CrmEnvironmentBuilder().WithEntities<Ids>().Create(service);
            }

            protected override void Test(IOrganizationService service)
            {
                // Test Exists
                var contact = service.GetFirstOrDefault<Contact>();
                Assert.IsNotNull(contact);

                // Test Not Exists
                service.Delete((Id)Ids.Contact);
                contact = service.GetFirstOrDefault<Contact>();
                Assert.IsNull(contact);
            }
        }

        #endregion FirstOrDefault

        #region CreateOrMinimalUpdate

        [TestMethod]
        public void Extensions_IOrganizationService_CreateOrMinimalUpdate()
        {
            new CreateOrMinimalUpdate().Test();
        }

        // ReSharper disable once InconsistentNaming
        private class CreateOrMinimalUpdate : TestMethodClassBase
        {
            private struct Ids
            {
                public static readonly Id<Contact> Contact = new Id<Contact>("74B36BB1-92F6-4E47-801B-75120931B8C5");
            }

            protected override void Test(IOrganizationService service)
            {
                service = new OrganizationServiceBuilder(service).WithIdsDefaultedForCreate(Ids.Contact).Build();
                var entity = new Contact
                {
                    Address1_City = "Any Town",
                    Address1_AddressTypeCodeEnum = Contact_Address1_AddressTypeCode.Primary,
                    FirstName = "Hi",
                    LastName = "Yah",
                    NumberOfChildren = 3,
                };

                // Should Create
                service.CreateOrMinimumUpdate(entity);
                AssertCrm.Exists(Ids.Contact);

                // Should not update or create
                var readOnly = new OrganizationServiceBuilder(service).IsReadOnly().Build();
                readOnly.CreateOrMinimumUpdate(entity);

                // Should update only single value;
                var entitiesById = new Dictionary<Guid, Contact>
                {
                    {entity.Id, entity.Clone(true)}
                };
                entity.Address1_Country = "USA";

                var updater = new TestUpdater(entitiesById);
                service.CreateOrMinimumUpdate(entity, updater);
                var unchanged = updater.MostRecentUnchangedAttributes;
                Assert.AreEqual(5, unchanged.Count);
                AssertContains(unchanged, Contact.Fields.Address1_City);
                AssertContains(unchanged, Contact.Fields.NumberOfChildren);
                AssertContains(unchanged, Contact.Fields.Address1_AddressTypeCode);
                AssertContains(unchanged, Contact.Fields.FirstName);
                AssertContains(unchanged, Contact.Fields.LastName);
                Assert.IsFalse(unchanged.Contains(Contact.Fields.Address1_Country));

                // No existing, should Update everything:
                updater.MostRecentUnchangedAttributes.Clear();
                service.CreateOrMinimumUpdate(new Contact{
                    Id = Ids.Contact,
                    FirstName = "Updated"
                }, new Dictionary<Guid, Contact>());
                Assert.AreEqual(0, updater.MostRecentUnchangedAttributes.Count);
                var value = service.GetEntity(Ids.Contact);
                Assert.AreEqual("Updated", value.FirstName);
            }

            private void AssertContains(List<string> list, string value)
            {
                Assert.IsTrue(list.Contains(value), "Missing " +value);
            }
        }

        public class TestUpdater : MinimumUpdaterDefault<Contact>
        {
            public TestUpdater(Dictionary<Guid, Contact> dict) : base(dict) { }

            public List<string> MostRecentUnchangedAttributes { get; private set; }

            public override void PreMinimalUpdate(Contact entity, Contact minimalChangesEntity, List<string> unchangedAttributes)
            {
                base.PreMinimalUpdate(entity, minimalChangesEntity, unchangedAttributes);
                MostRecentUnchangedAttributes = unchangedAttributes;
            }
        }

        #endregion CreateOrMinimalUpdate
    }
}
