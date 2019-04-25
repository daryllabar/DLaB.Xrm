using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Source.DLaB.Xrm;

namespace Core.DLaB.Xrm.Tests
{
    [TestClass]
    public class EntityHelperTests
    {
        [TestMethod]
        public void EntityHelper_GetIdAttributeName_ForCustomActivity_Should_ReturnActivityId()
        {
            Assert.AreEqual("activityid", EntityHelper.GetIdAttributeName<CustomActivity>());
            Assert.AreEqual("activityid", EntityHelper.GetIdAttributeName("customactivity"));
        }

        [TestMethod]
        public void EntityHelper_GetPrimaryFieldInfo_ForCustomActivity_Should_Subject()
        {
            Assert.AreEqual("subject", EntityHelper.GetPrimaryFieldInfo("customactivity").AttributeName);
        }
    }

    [EntityLogicalName("customactivity")]
    public class CustomActivity : Entity{

        [AttributeLogicalName("activityid")]
        public override Guid Id { get; set; }

        [AttributeLogicalName("subject")]
        public string Subject { get; set; }
    }

    public class MyConfig : IDLaBConfig
    {
        public string GetIrregularIdAttributeName(string logicalName)
        {
            return logicalName == "customactivity" ? "activityid" : null;
        }

        public PrimaryFieldInfo GetIrregularPrimaryFieldInfo(string logicalName, PrimaryFieldInfo defaultInfo = null)
        {
            if (logicalName == "customactivity")
            {
                defaultInfo = defaultInfo ?? new PrimaryFieldInfo();
                defaultInfo.AttributeName = "subject";
                return defaultInfo;
            }

            return null;
        }
    }
}
