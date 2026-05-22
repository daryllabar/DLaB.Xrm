// These tests use the local ActivePropertyInfo source (not NuGet), which only applies to the net462 build.
// In the net8.0 DotNetCore.Tests project, the NuGet DLaB.Xrm assembly provides Source.DLaB.Xrm.ActivePropertyInfo,
// which shadows the locally-compiled source and cannot be overridden.
#if !NET
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;

namespace DLaB.Xrm.Tests.Core
{
    [TestClass]
    public class ActivePropertyInfoTests
    {
        [TestMethod]
        public void ActivePropertyInfo_MultiUnderscoredEntityWithStateCodeProperty_ShouldNotBeJoinEntity()
        {
            // An entity with a multi-underscored name that has a property named StateCode
            // should NOT be identified as a join entity
            var info = new Source.DLaB.Xrm.ActivePropertyInfo<MultiUnderscoredEntityWithStateCodeProperty>();
            Assert.AreEqual(Source.DLaB.Xrm.ActiveAttributeType.StateCode, info.ActiveAttribute, "Entity with multi-underscored name but with a StateCode-named property should be treated as having a StateCode active attribute.");
        }

        [TestMethod]
        public void ActivePropertyInfo_MultiUnderscoredEntityWithStateCodeAttribute_ShouldNotBeJoinEntity()
        {
            // An entity with a multi-underscored name that has a property decorated with
            // [AttributeLogicalName("statecode")] should NOT be identified as a join entity
            var info = new Source.DLaB.Xrm.ActivePropertyInfo<MultiUnderscoredEntityWithStateCodeAttribute>();
            Assert.AreEqual(Source.DLaB.Xrm.ActiveAttributeType.StateCode, info.ActiveAttribute, "Entity with multi-underscored name but with [AttributeLogicalName(\"statecode\")] property should be treated as having a StateCode active attribute.");
        }

        [TestMethod]
        public void ActivePropertyInfo_MultiUnderscoredEntityWithoutStateCode_ShouldBeJoinEntity()
        {
            // An entity with a multi-underscored name that does NOT have a StateCode property or attribute
            // should be identified as a join entity
            var info = new Source.DLaB.Xrm.ActivePropertyInfo<MultiUnderscoredEntityWithoutStateCode>();
            Assert.AreEqual(Source.DLaB.Xrm.ActiveAttributeType.None, info.ActiveAttribute, "Entity with multi-underscored name and no StateCode property should be treated as a join entity with no active attribute.");
        }
    }

    /// <summary>Entity with a multi-underscored name and a property literally named StateCode.</summary>
    [EntityLogicalName("new_multi_underscored_entity")]
    public class MultiUnderscoredEntityWithStateCodeProperty : Entity
    {
        [AttributeLogicalName("statecode")]
        public int? StateCode
        {
            get => GetAttributeValue<int?>("statecode");
            set => SetAttributeValue("statecode", value);
        }
    }

    /// <summary>Entity with a multi-underscored name where the statecode property has a non-standard C# name but the correct attribute.</summary>
    [EntityLogicalName("new_multi_underscored_stateattr")]
    public class MultiUnderscoredEntityWithStateCodeAttribute : Entity
    {
        [AttributeLogicalName("statecode")]
        public int? Status
        {
            get => GetAttributeValue<int?>("statecode");
            set => SetAttributeValue("statecode", value);
        }
    }

    /// <summary>Entity with a multi-underscored name and no statecode property at all.</summary>
    [EntityLogicalName("new_multi_underscored_nonstateentity")]
    public class MultiUnderscoredEntityWithoutStateCode : Entity
    {
        [AttributeLogicalName("new_name")]
        public string? Name
        {
            get => GetAttributeValue<string>("new_name");
            set => SetAttributeValue("new_name", value);
        }
    }
}
#endif
