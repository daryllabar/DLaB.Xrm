using System;
using System.Diagnostics;
using DLaB.Xrm.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk.Query;
using Sut = Source.DLaB.Xrm.Extensions;

namespace DLaB.Xrm.Tests.Core
{
    [TestClass]
    public class WhereEqualExtensionsTests
    {
        [TestMethod]
        public void WhereEqual_LogicalOr_Should_SplitConditions()
        {
            var expected = new QueryExpression(Contact.EntityLogicalName);
            var fe = expected.Criteria.AddFilter(LogicalOperator.Or);
            fe.AddFilter(LogicalOperator.And).AddCondition(Contact.Fields.FirstName, ConditionOperator.Equal, "Test");
            fe.AddFilter(LogicalOperator.And).AddCondition(Contact.Fields.FirstName, ConditionOperator.Equal, "Or'd");
            fe.AddFilter(LogicalOperator.And).AddCondition(Contact.Fields.FirstName, ConditionOperator.Equal, "Or'd2");

            var qe = new QueryExpression(Contact.EntityLogicalName);

            Sut.WhereEqual(qe,
                Contact.Fields.FirstName, "Test",
                LogicalOperator.Or,
                Contact.Fields.FirstName, "Or'd",
                LogicalOperator.Or,
                Contact.Fields.FirstName, "Or'd2"
            );

            Assert.AreEqual(expected.GetSqlStatement(), qe.GetSqlStatement());
            AssertEqualString(@"SELECT 
FROM contact
WHERE
( ( ( contact.firstname = 'Test' ) 
 Or ( contact.firstname = 'Or'd' ) 
 Or ( contact.firstname = 'Or'd2' ) 
) 
)", qe);

        }

        [TestMethod]
        public void WhereEqual_LogicalOrWithMultipleConditions_Should_SplitConditions()
        {
            var expected = new QueryExpression(Contact.EntityLogicalName);
            var fe = expected.Criteria.AddFilter(LogicalOperator.Or);
            var orFilter = fe.AddFilter(LogicalOperator.And);
            orFilter.AddCondition(Contact.Fields.FirstName, ConditionOperator.Equal, "Test");
            orFilter.AddCondition(Contact.Fields.LastName, ConditionOperator.Equal, "Test2");
            orFilter = fe.AddFilter(LogicalOperator.And);
            orFilter.AddCondition(Contact.Fields.FirstName, ConditionOperator.Equal, "Or'd");
            orFilter.AddCondition(Contact.Fields.LastName, ConditionOperator.Equal, "Or'd2");

            var qe = new QueryExpression(Contact.EntityLogicalName);

            Sut.WhereEqual(qe,
                Contact.Fields.FirstName, "Test",
                Contact.Fields.LastName, "Test2",
                LogicalOperator.Or,
                Contact.Fields.FirstName, "Or'd",
                Contact.Fields.LastName, "Or'd2"
            );

            Assert.AreEqual(expected.GetSqlStatement(), qe.GetSqlStatement());
            AssertEqualString(@"SELECT 
FROM contact
WHERE
( ( ( contact.firstname = 'Test' And contact.lastname = 'Test2' ) 
 Or ( contact.firstname = 'Or'd' And contact.lastname = 'Or'd2' ) 
) 
)", qe);
        }

        [TestMethod]
        public void WhereEqual_WithLogicalAndAndMultipleOrs_Should_SplitConditions()
        {
            var expected = new QueryExpression(Contact.EntityLogicalName);
            expected.Criteria.AddFilter(LogicalOperator.And)
                             .AddCondition(Contact.Fields.StateCode, ConditionOperator.Equal, (int)ContactState.Active);
            var fe = expected.Criteria.AddFilter(LogicalOperator.Or);
            var orFilter = fe.AddFilter(LogicalOperator.And);
            orFilter.AddCondition(Contact.Fields.FirstName, ConditionOperator.Equal, "Test");
            orFilter.AddCondition(Contact.Fields.LastName, ConditionOperator.Equal, "Test2");
            orFilter = fe.AddFilter(LogicalOperator.And);
            orFilter.AddCondition(Contact.Fields.FirstName, ConditionOperator.Equal, "Or'd");
            orFilter.AddCondition(Contact.Fields.LastName, ConditionOperator.Equal, "Or'd2");

            var qe = new QueryExpression(Contact.EntityLogicalName);

            Sut.WhereEqual(qe,
                Contact.Fields.StateCode, (int)ContactState.Active,
                LogicalOperator.And,
                Contact.Fields.FirstName, "Test",
                Contact.Fields.LastName, "Test2",
                LogicalOperator.Or,
                Contact.Fields.FirstName, "Or'd",
                Contact.Fields.LastName, "Or'd2"
            );

            Assert.AreEqual(expected.GetSqlStatement(), qe.GetSqlStatement());
            AssertEqualString(@"SELECT 
FROM contact
WHERE
( ( contact.statecode = 0 ) 
And ( ( contact.firstname = 'Test' And contact.lastname = 'Test2' ) 
 Or ( contact.firstname = 'Or'd' And contact.lastname = 'Or'd2' ) 
) 
)", qe);
        }

        [TestMethod]
        public void WhereEqual_WithLogicalAndAndOr_Should_SplitConditions()
        {
            var expected = new QueryExpression(Contact.EntityLogicalName);
            expected.Criteria.AddFilter(LogicalOperator.And)
                .AddCondition(Contact.Fields.StateCode, ConditionOperator.Equal, (int)ContactState.Active);
            var fe = expected.Criteria.AddFilter(LogicalOperator.Or);
            var orFilter = fe.AddFilter(LogicalOperator.And);
            orFilter.AddCondition(Contact.Fields.FirstName, ConditionOperator.Equal, "Test");
            orFilter = fe.AddFilter(LogicalOperator.And);
            orFilter.AddCondition(Contact.Fields.FirstName, ConditionOperator.Equal, "Or'd");

            var qe = new QueryExpression(Contact.EntityLogicalName);

            Sut.WhereEqual(qe,
                Contact.Fields.StateCode, (int)ContactState.Active,
                LogicalOperator.And,
                Contact.Fields.FirstName, "Test",
                LogicalOperator.Or,
                Contact.Fields.FirstName, "Or'd"
            );

            Assert.AreEqual(expected.GetSqlStatement(), qe.GetSqlStatement());
            AssertEqualString(@"SELECT 
FROM contact
WHERE
( ( contact.statecode = 0 ) 
And ( ( contact.firstname = 'Test' ) 
 Or ( contact.firstname = 'Or'd' ) 
) 
)", qe);
        }

        [TestMethod]
        public void WhereEqual_WithLogicalAndPlusOneOrWithTwo_Should_SplitConditions()
        {

            var expected = new QueryExpression(Contact.EntityLogicalName);
            var first = expected.Criteria.AddFilter(LogicalOperator.And);
            first.AddCondition(Contact.Fields.StateCode, ConditionOperator.Equal, (int)ContactState.Active);
            first.AddCondition(Contact.Fields.FirstName, ConditionOperator.Equal, "Test");
            var fe = expected.Criteria.AddFilter(LogicalOperator.Or);
            var orFilter = fe.AddFilter(LogicalOperator.And);
            orFilter.AddCondition(Contact.Fields.LastName, ConditionOperator.Equal, "Test2");
            orFilter = fe.AddFilter(LogicalOperator.And);
            orFilter.AddCondition(Contact.Fields.MiddleName, ConditionOperator.Equal, "Or'd");
            orFilter.AddCondition(Contact.Fields.LastName, ConditionOperator.Equal, "Or'd2");

            var qe = new QueryExpression(Contact.EntityLogicalName);
            Sut.WhereEqual(qe,
                Contact.Fields.StateCode, (int)ContactState.Active,
                Contact.Fields.FirstName, "Test",
                LogicalOperator.And,
                Contact.Fields.LastName, "Test2",
                LogicalOperator.Or,
                Contact.Fields.MiddleName, "Or'd",
                Contact.Fields.LastName, "Or'd2"
            );

            Assert.AreEqual(expected.GetSqlStatement(), qe.GetSqlStatement());
            AssertEqualString(@"SELECT 
FROM contact
WHERE
( ( contact.statecode = 0 And contact.firstname = 'Test' ) 
And ( ( contact.lastname = 'Test2' ) 
 Or ( contact.middlename = 'Or'd' And contact.lastname = 'Or'd2' ) 
) 
)", qe);
        }

        [TestMethod]
        public void WhereEqual_WithUnneededLogicalAnds_Should_Error()
        {
            try
            {
                var qe = new QueryExpression(Contact.EntityLogicalName);
                Sut.WhereEqual(qe,
                    Contact.Fields.StateCode, (int)ContactState.Active,
                    LogicalOperator.And,
                    Contact.Fields.FirstName, "Test"
                );
                Assert.Fail("Expected Exception to be thrown");
            }
            catch (ArgumentException ex)
            {
                // Expected Argument Exception
                Assert.AreEqual("LogicalOperator.And can not be the last operator in the list!", ex.Message);
            }

            try
            {
                var sut = new QueryExpression(Contact.EntityLogicalName);
                Sut.WhereEqual(sut,
                    Contact.Fields.StateCode, (int)ContactState.Active,
                    LogicalOperator.And,
                    Contact.Fields.FirstName, "Test",
                    LogicalOperator.And,
                    Contact.Fields.LastName, "Test2"
                );
                Assert.Fail("Expected Exception to be thrown");
            }
            catch (ArgumentException ex)
            {
                // Expected Argument Exception
                Assert.AreEqual("LogicalOperator.And can not be followed by another LogicalOperator.And!", ex.Message);
            }

            try
            {
                var sut = new QueryExpression(Contact.EntityLogicalName);
                Sut.WhereEqual(sut,
                    Contact.Fields.StateCode, (int)ContactState.Active,
                    Contact.Fields.FirstName, "Test",
                    LogicalOperator.And,
                    Contact.Fields.LastName, "Test2",
                    LogicalOperator.Or,
                    Contact.Fields.FirstName, "Or'd",
                    LogicalOperator.And,
                    Contact.Fields.LastName, "Or'd2"
                );
                Assert.Fail("Expected Exception to be thrown");
            }
            catch (ArgumentException ex)
            {
                // Expected Argument Exception
                Assert.AreEqual("LogicalOperator.And can not be the last operator in the list!", ex.Message);
            }
        }

        [DebuggerHidden]
        private static void AssertEqualString(string expected, QueryExpression actual)
        {
            Assert.AreEqual(expected.Replace(@"
", Environment.NewLine), actual.GetSqlStatement().Trim());
        }
    }
}
