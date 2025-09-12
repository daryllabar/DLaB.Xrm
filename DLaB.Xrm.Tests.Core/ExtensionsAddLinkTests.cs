using DLaB.Xrm.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk.Query;
using Sut = Source.DLaB.Xrm.Extensions;
using System.Linq;

namespace DLaB.Xrm.Tests.Core
{
    [TestClass]
    public class ExtensionsAddLinkTests
    {
        private enum LinkParams
        {
            Column,
            DifferentAttributes,
            LeftJoin,
        }

        [TestMethod]
        public void LinkEntityAddLink_MultipleLinks_Should_AliasAsNumberedAttributeName()
        {
            var link = new LinkEntity();
            // Add existing links that would cause alias conflicts
            Sut.AddLink(link, TransactionCurrency.EntityLogicalName, Account.Fields.TransactionCurrencyId);
            Sut.AddLink(link, TransactionCurrency.EntityLogicalName, Account.Fields.ModifiedBy);

            AssertValid(Sut.AddLink(link, TransactionCurrency.EntityLogicalName, Account.Fields.TransactionCurrencyId), "_1");
            AssertValid(Sut.AddLink<TransactionCurrency>(link, Account.Fields.TransactionCurrencyId), "_2");
            AssertValid(Sut.AddLink<TransactionCurrency>(link, Account.Fields.TransactionCurrencyId, c => new { c.CurrencyPrecision }), "_3", LinkParams.Column);
            AssertValid(Sut.AddLink<TransactionCurrency>(link, Account.Fields.ModifiedBy, TransactionCurrency.Fields.CreatedBy), "_1", LinkParams.DifferentAttributes);
            AssertValid(Sut.AddLink<TransactionCurrency>(link, Account.Fields.ModifiedBy, TransactionCurrency.Fields.CreatedBy, c => new { c.CurrencyPrecision }), "_2", LinkParams.DifferentAttributes, LinkParams.Column);

            AssertValid(Sut.AddLink(link, TransactionCurrency.EntityLogicalName, Account.Fields.TransactionCurrencyId, JoinOperator.LeftOuter), "_4", LinkParams.LeftJoin);
            AssertValid(Sut.AddLink<TransactionCurrency>(link, Account.Fields.TransactionCurrencyId, JoinOperator.LeftOuter), "_5", LinkParams.LeftJoin);
            AssertValid(Sut.AddLink<TransactionCurrency>(link, Account.Fields.TransactionCurrencyId, JoinOperator.LeftOuter, c => new { c.CurrencyPrecision }), "_6", LinkParams.LeftJoin, LinkParams.Column);
            AssertValid(Sut.AddLink<TransactionCurrency>(link, Account.Fields.ModifiedBy, TransactionCurrency.Fields.CreatedBy, JoinOperator.LeftOuter), "_3", LinkParams.LeftJoin, LinkParams.DifferentAttributes);
            AssertValid(Sut.AddLink<TransactionCurrency>(link, Account.Fields.ModifiedBy, TransactionCurrency.Fields.CreatedBy, JoinOperator.LeftOuter, c => new { c.CurrencyPrecision }), "_4", LinkParams.LeftJoin, LinkParams.DifferentAttributes, LinkParams.Column);
        }

        [TestMethod]
        public void LinkEntityAddLink_SingleLink_Should_AliasAsAttributeName()
        {
            var link = new LinkEntity();
            AssertValid(link, Sut.AddLink(link, TransactionCurrency.EntityLogicalName, Account.Fields.TransactionCurrencyId));
            AssertValid(link, Sut.AddLink<TransactionCurrency>(link, Account.Fields.TransactionCurrencyId));
            AssertValid(link, Sut.AddLink<TransactionCurrency>(link, Account.Fields.TransactionCurrencyId, c => new { c.CurrencyPrecision }), LinkParams.Column);
            AssertValid(link, Sut.AddLink<TransactionCurrency>(link, Account.Fields.ModifiedBy, TransactionCurrency.Fields.CreatedBy), LinkParams.DifferentAttributes);
            AssertValid(link, Sut.AddLink<TransactionCurrency>(link, Account.Fields.ModifiedBy, TransactionCurrency.Fields.CreatedBy, c => new { c.CurrencyPrecision }), LinkParams.DifferentAttributes, LinkParams.Column);
            
            AssertValid(link, Sut.AddLink(link, TransactionCurrency.EntityLogicalName, Account.Fields.TransactionCurrencyId, JoinOperator.LeftOuter), LinkParams.LeftJoin);
            AssertValid(link, Sut.AddLink<TransactionCurrency>(link, Account.Fields.TransactionCurrencyId, JoinOperator.LeftOuter), LinkParams.LeftJoin);
            AssertValid(link, Sut.AddLink<TransactionCurrency>(link, Account.Fields.TransactionCurrencyId, JoinOperator.LeftOuter, c => new { c.CurrencyPrecision }), LinkParams.LeftJoin, LinkParams.Column);
            AssertValid(link, Sut.AddLink<TransactionCurrency>(link, Account.Fields.ModifiedBy, TransactionCurrency.Fields.CreatedBy, JoinOperator.LeftOuter), LinkParams.LeftJoin, LinkParams.DifferentAttributes);
            AssertValid(link, Sut.AddLink<TransactionCurrency>(link, Account.Fields.ModifiedBy, TransactionCurrency.Fields.CreatedBy, JoinOperator.LeftOuter, c => new { c.CurrencyPrecision }), LinkParams.LeftJoin, LinkParams.DifferentAttributes, LinkParams.Column);
        }

        [TestMethod]
        public void QueryExpressionAddLink_MultipleLinks_Should_AliasAsNumberedAttributeName()
        {
            var qe = new QueryExpression(Account.EntityLogicalName);
            // Add existing links that would cause alias conflicts
            Sut.AddLink(qe, TransactionCurrency.EntityLogicalName, Account.Fields.TransactionCurrencyId);
            Sut.AddLink(qe, TransactionCurrency.EntityLogicalName, Account.Fields.ModifiedBy);

            AssertValid(Sut.AddLink(qe, TransactionCurrency.EntityLogicalName, Account.Fields.TransactionCurrencyId), "_1");
            AssertValid(Sut.AddLink<TransactionCurrency>(qe, Account.Fields.TransactionCurrencyId), "_2");
            AssertValid(Sut.AddLink<TransactionCurrency>(qe, Account.Fields.TransactionCurrencyId, c => new { c.CurrencyPrecision }), "_3", LinkParams.Column);
            AssertValid(Sut.AddLink<TransactionCurrency>(qe, Account.Fields.ModifiedBy, TransactionCurrency.Fields.CreatedBy), "_1", LinkParams.DifferentAttributes);
            AssertValid(Sut.AddLink<TransactionCurrency>(qe, Account.Fields.ModifiedBy, TransactionCurrency.Fields.CreatedBy, c => new { c.CurrencyPrecision }), "_2", LinkParams.DifferentAttributes, LinkParams.Column);

            AssertValid(Sut.AddLink(qe, TransactionCurrency.EntityLogicalName, Account.Fields.TransactionCurrencyId, JoinOperator.LeftOuter), "_4", LinkParams.LeftJoin);
            AssertValid(Sut.AddLink<TransactionCurrency>(qe, Account.Fields.TransactionCurrencyId, JoinOperator.LeftOuter), "_5", LinkParams.LeftJoin);
            AssertValid(Sut.AddLink<TransactionCurrency>(qe, Account.Fields.TransactionCurrencyId, JoinOperator.LeftOuter, c => new { c.CurrencyPrecision }), "_6", LinkParams.LeftJoin, LinkParams.Column);
            AssertValid(Sut.AddLink<TransactionCurrency>(qe, Account.Fields.ModifiedBy, TransactionCurrency.Fields.CreatedBy, JoinOperator.LeftOuter), "_3", LinkParams.LeftJoin, LinkParams.DifferentAttributes);
            AssertValid(Sut.AddLink<TransactionCurrency>(qe, Account.Fields.ModifiedBy, TransactionCurrency.Fields.CreatedBy, JoinOperator.LeftOuter, c => new { c.CurrencyPrecision }), "_4", LinkParams.LeftJoin, LinkParams.DifferentAttributes, LinkParams.Column);
        }

        [TestMethod]
        public void QueryExpressionAddLink_SingleLink_Should_AliasAsAttributeName()
        {
            var qe = new QueryExpression(Account.EntityLogicalName);
            AssertValid(qe, Sut.AddLink(qe, TransactionCurrency.EntityLogicalName, Account.Fields.TransactionCurrencyId));
            AssertValid(qe, Sut.AddLink<TransactionCurrency>(qe,                   Account.Fields.TransactionCurrencyId));
            AssertValid(qe, Sut.AddLink<TransactionCurrency>(qe,                   Account.Fields.TransactionCurrencyId, c => new { c.CurrencyPrecision }), LinkParams.Column);
            AssertValid(qe, Sut.AddLink<TransactionCurrency>(qe,                   Account.Fields.ModifiedBy, TransactionCurrency.Fields.CreatedBy), LinkParams.DifferentAttributes);
            AssertValid(qe, Sut.AddLink<TransactionCurrency>(qe,                   Account.Fields.ModifiedBy, TransactionCurrency.Fields.CreatedBy, c => new { c.CurrencyPrecision }), LinkParams.DifferentAttributes, LinkParams.Column);

            AssertValid(qe, Sut.AddLink(qe, TransactionCurrency.EntityLogicalName, Account.Fields.TransactionCurrencyId, JoinOperator.LeftOuter), LinkParams.LeftJoin);
            AssertValid(qe, Sut.AddLink<TransactionCurrency>(qe,                   Account.Fields.TransactionCurrencyId, JoinOperator.LeftOuter), LinkParams.LeftJoin);
            AssertValid(qe, Sut.AddLink<TransactionCurrency>(qe,                   Account.Fields.TransactionCurrencyId, JoinOperator.LeftOuter, c => new { c.CurrencyPrecision }), LinkParams.LeftJoin, LinkParams.Column);
            AssertValid(qe, Sut.AddLink<TransactionCurrency>(qe,                   Account.Fields.ModifiedBy, TransactionCurrency.Fields.CreatedBy, JoinOperator.LeftOuter), LinkParams.LeftJoin, LinkParams.DifferentAttributes);
            AssertValid(qe, Sut.AddLink<TransactionCurrency>(qe,                   Account.Fields.ModifiedBy, TransactionCurrency.Fields.CreatedBy, JoinOperator.LeftOuter, c => new { c.CurrencyPrecision }), LinkParams.LeftJoin, LinkParams.DifferentAttributes, LinkParams.Column);
        }

        [TestMethod]
        public void TypedQueryExpressionAddLink_MultipleLinks_Should_AliasAsNumberedAttributeName()
        {
            var qe = Source.DLaB.Xrm.QueryExpressionFactory.Create<Account>();
            // Add existing links that would cause alias conflicts
            Sut.AddLink(qe, TransactionCurrency.EntityLogicalName, Account.Fields.TransactionCurrencyId);
            Sut.AddLink(qe, TransactionCurrency.EntityLogicalName, Account.Fields.ModifiedBy);

            AssertValid(Sut.AddLink(qe, TransactionCurrency.EntityLogicalName, Account.Fields.TransactionCurrencyId), "_1");
            AssertValid(Sut.AddLink<TransactionCurrency>(qe, Account.Fields.TransactionCurrencyId), "_2");
            AssertValid(Sut.AddLink<TransactionCurrency>(qe, Account.Fields.TransactionCurrencyId, c => new { c.CurrencyPrecision }), "_3", LinkParams.Column);
            AssertValid(Sut.AddLink<TransactionCurrency>(qe, Account.Fields.ModifiedBy, TransactionCurrency.Fields.CreatedBy), "_1", LinkParams.DifferentAttributes);
            AssertValid(Sut.AddLink<TransactionCurrency>(qe, Account.Fields.ModifiedBy, TransactionCurrency.Fields.CreatedBy, c => new { c.CurrencyPrecision }), "_2", LinkParams.DifferentAttributes, LinkParams.Column);

            AssertValid(Sut.AddLink(qe, TransactionCurrency.EntityLogicalName, Account.Fields.TransactionCurrencyId, JoinOperator.LeftOuter), "_4", LinkParams.LeftJoin);
            AssertValid(Sut.AddLink<TransactionCurrency>(qe, Account.Fields.TransactionCurrencyId, JoinOperator.LeftOuter), "_5", LinkParams.LeftJoin);
            AssertValid(Sut.AddLink<TransactionCurrency>(qe, Account.Fields.TransactionCurrencyId, JoinOperator.LeftOuter, c => new { c.CurrencyPrecision }), "_6", LinkParams.LeftJoin, LinkParams.Column);
            AssertValid(Sut.AddLink<TransactionCurrency>(qe, Account.Fields.ModifiedBy, TransactionCurrency.Fields.CreatedBy, JoinOperator.LeftOuter), "_3", LinkParams.LeftJoin, LinkParams.DifferentAttributes);
            AssertValid(Sut.AddLink<TransactionCurrency>(qe, Account.Fields.ModifiedBy, TransactionCurrency.Fields.CreatedBy, JoinOperator.LeftOuter, c => new { c.CurrencyPrecision }), "_4", LinkParams.LeftJoin, LinkParams.DifferentAttributes, LinkParams.Column);
        }

        [TestMethod]
        public void TypedQueryExpressionAddLink_SingleLink_Should_AliasAsAttributeName()
        {
            var qe = Source.DLaB.Xrm.QueryExpressionFactory.Create<Account>();
            AssertValid(qe, Sut.AddLink(qe, TransactionCurrency.EntityLogicalName, Account.Fields.TransactionCurrencyId));
            AssertValid(qe, Sut.AddLink<TransactionCurrency>(qe, Account.Fields.TransactionCurrencyId));
            AssertValid(qe, Sut.AddLink<TransactionCurrency>(qe, Account.Fields.TransactionCurrencyId, c => new { c.CurrencyPrecision }), LinkParams.Column);
            AssertValid(qe, Sut.AddLink<TransactionCurrency>(qe, Account.Fields.ModifiedBy, TransactionCurrency.Fields.CreatedBy), LinkParams.DifferentAttributes);
            AssertValid(qe, Sut.AddLink<TransactionCurrency>(qe, Account.Fields.ModifiedBy, TransactionCurrency.Fields.CreatedBy, c => new { c.CurrencyPrecision }), LinkParams.DifferentAttributes, LinkParams.Column);

            AssertValid(qe, Sut.AddLink(qe, TransactionCurrency.EntityLogicalName, Account.Fields.TransactionCurrencyId, JoinOperator.LeftOuter), LinkParams.LeftJoin);
            AssertValid(qe, Sut.AddLink<TransactionCurrency>(qe, Account.Fields.TransactionCurrencyId, JoinOperator.LeftOuter), LinkParams.LeftJoin);
            AssertValid(qe, Sut.AddLink<TransactionCurrency>(qe, Account.Fields.TransactionCurrencyId, JoinOperator.LeftOuter, c => new { c.CurrencyPrecision }), LinkParams.LeftJoin, LinkParams.Column);
            AssertValid(qe, Sut.AddLink<TransactionCurrency>(qe, Account.Fields.ModifiedBy, TransactionCurrency.Fields.CreatedBy, JoinOperator.LeftOuter), LinkParams.LeftJoin, LinkParams.DifferentAttributes);
            AssertValid(qe, Sut.AddLink<TransactionCurrency>(qe, Account.Fields.ModifiedBy, TransactionCurrency.Fields.CreatedBy, JoinOperator.LeftOuter, c => new { c.CurrencyPrecision }), LinkParams.LeftJoin, LinkParams.DifferentAttributes, LinkParams.Column);
        }

        private void AssertValid(LinkEntity parent, LinkEntity link, params LinkParams[] testParams)
        {
            AssertValid(link, (string?)null, testParams);
            parent.LinkEntities.Clear();
        }

        private void AssertValid(QueryExpression qe, LinkEntity link, params LinkParams[] testParams)
        {
            AssertValid(link, (string?)null, testParams);
            qe.LinkEntities.Clear();
        }

        private void AssertValid(LinkEntity link, string? aliasPostfix, params LinkParams[] testParams)
        {
            var fromAttribute = testParams.Contains(LinkParams.DifferentAttributes) ? Account.Fields.ModifiedBy : Account.Fields.TransactionCurrencyId;
            var expectedLink = new LinkEntity
            {
                Columns = testParams.Contains(LinkParams.Column) ? new ColumnSet(TransactionCurrency.Fields.CurrencyPrecision) : new ColumnSet(false),
                EntityAlias = fromAttribute + aliasPostfix,
                JoinOperator = testParams.Contains(LinkParams.LeftJoin) ? JoinOperator.LeftOuter : JoinOperator.Inner,
                LinkFromAttributeName = fromAttribute,
                LinkToEntityName = TransactionCurrency.EntityLogicalName,
                LinkToAttributeName = testParams.Contains(LinkParams.DifferentAttributes) ? TransactionCurrency.Fields.CreatedBy : TransactionCurrency.Fields.TransactionCurrencyId,
            };

            var callType = string.Join(", ", testParams.Select(p => p.ToString()));
            Assert.AreEqual(expectedLink.Columns.Columns.Count, link.Columns.Columns.Count, callType);
            Assert.AreEqual(expectedLink.Columns.Columns.FirstOrDefault(), link.Columns.Columns.FirstOrDefault(), callType);
            Assert.AreEqual(expectedLink.EntityAlias, link.EntityAlias, callType);
            Assert.AreEqual(expectedLink.JoinOperator, link.JoinOperator, callType);
            Assert.AreEqual(expectedLink.LinkFromAttributeName, link.LinkFromAttributeName, callType);
            Assert.AreEqual(expectedLink.LinkToEntityName, link.LinkToEntityName, callType);
            Assert.AreEqual(expectedLink.LinkToAttributeName, link.LinkToAttributeName, callType);
        }
    }
}
