#if !NETCOREAPP
using System;
using DLaB.Xrm.Entities;
#endif
using System;
using System.Globalization;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Source.DLaB.Xrm;

namespace DLaB.Xrm.Tests.Core
{
    [TestClass]
    public class ExtensionsEntity
    {
#if !NETCOREAPP
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
            entity.FormattedValues.Add("First", "1");
            entity.RowVersion = "123";
            var clone = entity.Clone();

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
        }
#endif

        [TestMethod]
        public void Extensions_Entity_ToEntityInterface()
        {
            var sut = new Entity(MyEarlyBoundEntity.EntityLogicalName);
            sut.ToEntityInterface<IMyInterface>(Assembly.GetExecutingAssembly(), "DLaB.Xrm.Tests.Core");
            var output = sut.ToEntityInterface<IMyInterface>(Assembly.GetExecutingAssembly(), "DLaB.Xrm.Tests.Core");
            Assert.IsNotNull(output);
            Assert.AreNotEqual(sut, output);
            Assert.AreEqual(output, ((Entity)output).ToEntityInterface<IMyInterface>(Assembly.GetExecutingAssembly(), "DLaB.Xrm.Tests.Core"));
        }
    }

    [EntityLogicalName("myearlyboundentity")]
    public class MyEarlyBoundEntity : Entity, IMyInterface
    {
        public const string EntityLogicalName = "myearlyboundentity";
    }

    public interface IMyInterface { }

}
