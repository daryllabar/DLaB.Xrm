#if NET
using DataverseUnitTest;
#endif

using DLaB.Xrm.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk.Query;
using Source.DLaB.Xrm;

namespace DLaB.Xrm.Tests.Core
{
    [TestClass]
    public class FetchExpressionExtensionTests
    {
        [TestInitialize]
        public void Initialize()
        {
            TestInitializer.InitializeTestSettings();
        }

        [TestMethod]
        public void SetFetchTopX_NoTop_Should_Add()
        {
            var fe = new FetchExpression("<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'><entity name='account'><attribute name='name' /></entity></fetch>");
            fe.SetFetchTopX(10);
            Assert.AreEqual("<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false' top=\"10\"><entity name='account'><attribute name='name' /></entity></fetch>", fe.Query, "Top was not added to FetchExpression");
        }

        [TestMethod]
        public void SetFetchTopX_WithTop_Should_Update()
        {
            var fe = new FetchExpression("<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false' top='1'><entity name='account'><attribute name='name' /></entity></fetch>");
            fe.SetFetchTopX(10);
            Assert.AreEqual("<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false' top='10'><entity name='account'><attribute name='name' /></entity></fetch>", fe.Query, "Top was not added to FetchExpression");
        }
    }
}
