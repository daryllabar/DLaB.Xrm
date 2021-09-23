using DLaB.Xrm.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Source.DLaB.Xrm;

namespace Core.DLaB.Xrm.Tests
{
    [TestClass]
    public class ExtensionsTypedEntityTests
    {
        #region FirstOrDefault

        [TestMethod]
        public void Extensions_TypedEntity_CoalesceEntity()
        {
            var target = new Contact
            {
                AccountRoleCodeEnum = Contact_AccountRoleCode.Influencer
            };
            var preImage = new Contact
            {
                AccountRoleCodeEnum = Contact_AccountRoleCode.Employee
            };
            preImage.FormattedValues[Contact.Fields.AccountRoleCode] = preImage.AccountRoleCodeEnum.ToString();

            Assert.IsTrue(!target.CoalesceEntity(preImage).FormattedValues.ContainsKey(Contact.Fields.AccountRoleCode));

            target.AccountRoleCodeEnum = preImage.AccountRoleCodeEnum;
            Assert.AreEqual(preImage.FormattedValues[Contact.Fields.AccountRoleCode], target.FormattedValues[Contact.Fields.AccountRoleCode]);
        }

        #endregion FirstOrDefault
    }
}
