#nullable enable
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
#if DLAB_UNROOT_COMMON_NAMESPACE
using DLaB.Common;
#else
using Source.DLaB.Common;
#endif

namespace DLaB.Xrm.Tests.Core
{
    [TestClass]
    public class ToCsvExtensionsTests
    {
        [TestMethod]
        public void ToCsv_WithOptions_SkipNullAndEmpty()
        {
            var values = new List<string?> {"One", null, "", "Two"};

            var csv = values.ToCsv(new ToCsvOptions
            {
                SkipNullValues = true,
                SkipEmptyStrings = true
            });

            Assert.AreEqual("One, Two", csv);
        }

        [TestMethod]
        public void ToCsv_WithOptions_DefaultValueWhenNoValuesRemain()
        {
            var values = new List<string?> {null, ""};

            var csv = values.ToCsv(new ToCsvOptions
            {
                SkipNullValues = true,
                SkipEmptyStrings = true,
                DefaultValue = "None"
            });

            Assert.AreEqual("None", csv);
        }
    }
}
