

using System;
using System.Collections.Generic;
using System.Linq;
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
        public void Extensions_IOgranizationService_CreateOrMinimalUpdate()
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
                    NumberOfChildren = 3,
                    Address1_City = "Any Town",
                    Address1_AddressTypeCodeEnum = Contact_Address1_AddressTypeCode.Primary,
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
                Assert.AreEqual(1, updater.MostRecentUnchangedAttributes.Count);
                Assert.AreEqual(Contact.Fields.Address1_Country, updater.MostRecentUnchangedAttributes.First());
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
