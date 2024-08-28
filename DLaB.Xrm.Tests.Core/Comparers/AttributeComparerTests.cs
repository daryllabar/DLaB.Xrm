#if NET
using DataverseUnitTest;
#else
using DLaB.Xrm.Test;
#endif
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using Source.DLaB.Xrm;
using Source.DLaB.Xrm.Comparers;

namespace DLaB.Xrm.Tests.Core.Comparers
{
    [TestClass]
    public class AttributeComparerTests
    {
        private static readonly object[] EntityReferences =  new object[]
        {
            null,
            new EntityReference(),
            new EntityReference("contact", Guid.NewGuid()),
            new EntityReference("contact", Guid.NewGuid()),
            new EntityReference("contact", "telephone1", "555-867-5309"),
            new EntityReference("contact", "telephone2", "555-867-5309"),
            new EntityReference("contact", "telephone1", "800-867-5309"),
        };

        private static readonly object[] Dictionaries =
        {
            null,
            new Dictionary<string, string> { { "A", "A" }, { "B", "B" }, },
            new Dictionary<string, string> { { "A", "B" }, { "B", "B" }, },
            new Dictionary<string, string> { { "A", "A" }, { "B", "A" }, },
        };

        private static readonly object[] Bytes =
        {
            null,
            new byte[] { 0, 1, 2 },
            new byte[] { 1, 2, 3 },
        };

        private static readonly object[] Enumerables =
        {
            null,
            new List<OptionSetValue> { new OptionSetValue(1), new OptionSetValue(2) },
            new List<OptionSetValue> { new OptionSetValue(2), new OptionSetValue(3) },
        };

        private static readonly object[] OptionSets =
        {
            null,
            new OptionSetValue(),
            new OptionSetValue(1),
            new OptionSetValue(2),
        };

        private static readonly object[] Moneys =
        {
            null,
            new Money(),
            new Money(1m),
            new Money(2m)
        };

        [TestMethod]
        public void ValuesAreEqual_Should_CompareEntityReferences()
        {
            TestValues(EntityReferences);
        }

        [TestMethod]
        public void ValuesAreEqual_Should_CompareDictionaries()
        {
            TestValues(Dictionaries);
        }


        [TestMethod]
        public void ValuesAreEqual_Should_CompareBytes()
        {
            TestValues(Bytes);
        }

        [TestMethod]
        public void ValuesAreEqual_Should_CompareEnumerables()
        {
            TestValues(Enumerables);
        }

        [TestMethod]
        public void ValuesAreEqual_Should_CompareOptionSets()
        {
            TestValues(OptionSets);
        }

        [TestMethod]
        public void ValuesAreEqual_Should_CompareMoneys()
        {
            TestValues(Moneys);
        }

        private void TestValues(object[] values)
        {
            Test.TestInitializer.InitializeTestSettings();
            var service = TestBase.GetOrganizationService();
            for (var i = 0; i < values.Length; i++)
            {
                for (var j = 0; j < values.Length; j++)
                {
                    Assert.AreEqual(i == j, AttributeComparer.ValuesAreEqual(service, values[i], values[j]), $"Comparison with values[{i}] {values[i].ObjectToStringDebug()} and values[{j}] {values[j].ObjectToStringDebug()}");
                }
            }
        }
    }
}
