using System;
using System.Linq;
using Source.DLaB.Xrm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
// ReSharper disable StringLiteralTypo

namespace Core.DLaB.Xrm.Tests
{
    [TestClass]
    public class ExtensionsColumnSetAddColumnsTests
    {
        #region AddColumns

        [TestMethod]
        public void Extensions_ColumnSet_AddColumns()
        {
            var cs = new ColumnSet();
            cs.AddColumns<TestColumnSetEntity>(c => new {c.TestColumnSetEntity1});
            Assert.AreEqual("testcolumnsetentity", cs.Columns.Single());

            cs = new ColumnSet();
            cs.AddColumns<TestColumnSetEntity>(c => new { c.Id });
            Assert.AreEqual("testcolumnsetentityid", cs.Columns.Single());

            cs = new ColumnSet();
            cs.AddColumns<TestColumnSetEntity>(c => new { c.CreatedOn });
            Assert.AreEqual("createdon", cs.Columns.Single());


        }

        #endregion AddColumns
    }

    [Microsoft.Xrm.Sdk.Client.EntityLogicalName("testcolumnsetentity")]
    public class TestColumnSetEntity : Entity
    {
        [AttributeLogicalName("testcolumnsetentity")]
        public string TestColumnSetEntity1 { get; set; }

        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("testcolumnsetentityid")]
        public override System.Guid Id { get; set; }

        [AttributeLogicalName("createdon")]
        public DateTime? CreatedOn { get; set; }
    }
}