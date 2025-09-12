#if !NET
using DLaB.Xrm.Entities;
#endif
using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Source.DLaB.Xrm;

namespace DLaB.Xrm.Tests.Core
{
    [TestClass]
    public class ExtensionsEntityTests
    {
#if !NET
        [TestMethod]
        public void Extensions_Entity_Clone()
        {
            var entity = new Contact
            {
                ParentCustomerId = new EntityReference(Account.EntityLogicalName, Guid.NewGuid()),
                AccountRoleCodeEnum = Contact_AccountRoleCode.DecisionMaker,
                Address1_Longitude = 10,
                Address1_City = "Smallsville",
                AnnualIncome = new Money(10m),
                business_unit_contacts = new BusinessUnit { Address1_City = "Detroit"},
            };
            entity.KeyAttributes[Contact.Fields.EmployeeId] = "MyEmpId";
            entity.FormattedValues.Add("First", "1");
            entity.RowVersion = "123";
            var clone = entity.Clone(true);

            Assert.AreEqual(entity.ParentCustomerId.LogicalName, clone.ParentCustomerId.LogicalName);
            Assert.AreEqual(entity.ParentCustomerId.Id, clone.ParentCustomerId.Id);
            entity.ParentCustomerId.Id = Guid.NewGuid();
            Assert.AreNotEqual(entity.ParentCustomerId.Id, clone.ParentCustomerId.Id);
            Assert.AreEqual(entity.AccountRoleCode.Value, clone.AccountRoleCode.Value);
            Assert.AreEqual(entity.Address1_Longitude, clone.Address1_Longitude);
            Assert.AreEqual(entity.Address1_City, clone.Address1_City);
            Assert.AreEqual(entity.AnnualIncome.Value, clone.AnnualIncome.Value);
            entity.AnnualIncome.Value += 10;
            Assert.AreNotEqual(entity.AnnualIncome.Value, clone.AnnualIncome.Value);
            Assert.AreEqual(entity.business_unit_contacts.Address1_City, clone.business_unit_contacts.Address1_City);
            entity.business_unit_contacts.Address1_City += " City";
            Assert.AreNotEqual(entity.business_unit_contacts.Address1_City, clone.business_unit_contacts.Address1_City);
            Assert.AreEqual(entity.FormattedValues["First"], clone.FormattedValues["First"]);
            entity.FormattedValues["First"] = "2";
            Assert.AreNotEqual(entity.FormattedValues["First"], clone.FormattedValues["First"]);
            Assert.AreEqual(entity.KeyAttributes[Contact.Fields.EmployeeId], clone.KeyAttributes[Contact.Fields.EmployeeId]);
        }

#endif
        [TestMethod]
        public void Extensions_Entity_CloneNestedActivityParty()
        {
            var collection = new EntityCollection();
            collection.Entities.Add(new MyEarlyBoundEntity());
            var sut = new Entity {["from"] = collection};
            var clone = sut.Clone(true);
            Assert.AreEqual(typeof(MyEarlyBoundEntity), clone.GetAttributeValue<EntityCollection>("from").Entities[0].GetType());
        }

        [TestMethod]
        public void Extensions_Entity_ToEntityInterface()
        {
            var sut = new Entity(MyEarlyBoundEntity.EntityLogicalName);
            var output = sut.ToEntityInterface<IMyInterface>(Assembly.GetExecutingAssembly(), "DLaB.Xrm.Tests.Core");
            Assert.IsNotNull(output);
            Assert.AreNotEqual<object>(sut, output);
            Assert.AreEqual(output, ((Entity)output).ToEntityInterface<IMyInterface>(Assembly.GetExecutingAssembly(), "DLaB.Xrm.Tests.Core"));
        }

        [TestMethod]
        public void Extensions_Entity_ToEarlyBoundEntity()
        {
            var sut = new Entity(MyEarlyBoundEntity.EntityLogicalName);
            var entity = sut.ToEarlyBoundEntity(Assembly.GetExecutingAssembly(), "DLaB.Xrm.Tests.Core");
            Assert.IsNotNull(entity);
            Assert.AreNotEqual(sut, entity);
            Assert.AreEqual(entity, entity.ToEarlyBoundEntity(Assembly.GetExecutingAssembly(), "DLaB.Xrm.Tests.Core"));
        }

        [TestMethod]
        public void Extensions_Entity_CloneLate()
        {
            var sut = new Entity
            {
                ["ParentCustomerId"] = new EntityReference("account", Guid.NewGuid()),
                ["AccountRoleCode"] = new OptionSetValue(1),
                ["Address1_Longitude"] = 10,
                ["Address1_City"] = "Smallsville",
                ["AnnualIncome"] = new Money(10m),
                ["business_unit_contacts"] = new Entity("BusinessUnit") { ["Address1_City"] = "Detroit" },
                ["EC"] = new EntityCollection(new List<Entity>{ new Entity("BusinessUnit") { ["Address1_City"] = "Chicago" }}),
                RowVersion = "123"
            };
            sut.FormattedValues.Add("First", "1");
            var clone = sut.Clone(true);

            var parentCustomer = sut.GetAttributeValue<EntityReference>("ParentCustomerId");
            var parentCustomerClone = clone.GetAttributeValue<EntityReference>("ParentCustomerId");
            Assert.IsFalse(ReferenceEquals(parentCustomer, parentCustomerClone));
            Assert.AreEqual(parentCustomer.LogicalName, parentCustomerClone.LogicalName);
            Assert.AreEqual(parentCustomer.Id, parentCustomerClone.Id);
            parentCustomer.Id = Guid.NewGuid();
            Assert.AreNotEqual(parentCustomer.Id, parentCustomerClone.Id);

            Assert.AreEqual(sut.GetAttributeValue<OptionSetValue>("AccountRoleCode").Value, clone.GetAttributeValue<OptionSetValue>("AccountRoleCode").Value);
            Assert.AreEqual(sut["Address1_Longitude"], clone["Address1_Longitude"]);
            Assert.AreEqual(sut["Address1_City"], clone["Address1_City"]);

            var annualIncome = sut.GetAttributeValue<Money>("AnnualIncome");
            var annualIncomeClone = clone.GetAttributeValue<Money>("AnnualIncome");
            Assert.AreEqual(annualIncome.Value, annualIncomeClone.Value);
            annualIncome.Value += 10;
            Assert.AreNotEqual(annualIncome.Value, annualIncomeClone.Value);

            var bus = sut.GetAttributeValue<Entity>("business_unit_contacts");
            var busClone= clone.GetAttributeValue<Entity>("business_unit_contacts");
            Assert.AreEqual(bus["Address1_City"], busClone["Address1_City"]);
            bus["Address1_City"] += " City";
            Assert.AreNotEqual(bus["Address1_City"], busClone["Address1_City"]);

            Assert.AreEqual(sut.FormattedValues["First"], clone.FormattedValues["First"]);
            sut.FormattedValues["First"] = "2";
            Assert.AreNotEqual(sut.FormattedValues["First"], clone.FormattedValues["First"]);

            var ec = sut.GetAttributeValue<EntityCollection>("EC");
            var ecClone = clone.GetAttributeValue<EntityCollection>("EC");
            Assert.IsFalse(ReferenceEquals(ec, ecClone));
            Assert.AreEqual(ec.Entities[0].LogicalName, ecClone.Entities[0].LogicalName);
            Assert.AreEqual(ec.Entities[0].Id, ecClone.Entities[0].Id);
            ec.Entities[0].Id = Guid.NewGuid();
            Assert.AreNotEqual(ec.Entities[0].Id, ecClone.Entities[0].Id);
        }
    }

    [EntityLogicalName("myearlyboundentity")]
    public class MyEarlyBoundEntity : Entity, IMyInterface
    {
        public const string EntityLogicalName = "myearlyboundentity";
    }

    public interface IMyInterface { }

}
