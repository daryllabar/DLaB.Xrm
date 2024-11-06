using System;
using System.Diagnostics;
using DLaB.Xrm.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk.Query;

namespace DLaB.Xrm.Tests.Core
{
    [TestClass]
    public class WhereEqualExtensionsTests
    {
        [TestMethod]
        public void WhereEqual_LogicalOr_Should_SplitConditions()
        {
            var sut = new QueryExpression(Contact.EntityLogicalName);

            Source.DLaB.Xrm.Extensions.WhereEqual(sut,
                Contact.Fields.FirstName, "Test",
                LogicalOperator.Or,
                Contact.Fields.FirstName, "Or'd",
                LogicalOperator.Or,
                Contact.Fields.FirstName, "Or'd Twice"
            );

            AssertEqualString(@"SELECT 
FROM contact
WHERE
( ( ( contact.firstname = 'Test' ) 
 Or ( contact.firstname = 'Or'd' ) 
 Or ( contact.firstname = 'Or'd Twice' ) 
) 
)", sut);
        }

        [TestMethod]
        public void WhereEqual_LogicalOrWithMultipleConditions_Should_SplitConditions()
        {
            var sut = new QueryExpression(Contact.EntityLogicalName);

            Source.DLaB.Xrm.Extensions.WhereEqual(sut,
                Contact.Fields.FirstName, "Test",
                Contact.Fields.LastName, "Test2",
                LogicalOperator.Or,
                Contact.Fields.FirstName, "Or'd",
                Contact.Fields.LastName, "Or'd2"
            );

            AssertEqualString(@"SELECT 
FROM contact
WHERE
( ( ( contact.firstname = 'Test' And contact.lastname = 'Test2' ) 
 Or ( contact.firstname = 'Or'd' And contact.lastname = 'Or'd2' ) 
) 
)", sut);
        }

        [TestMethod]
        public void WhereEqual_WithLogicalAndAndMultipleOrs_Should_SplitConditions()
        {
            var sut = new QueryExpression(Contact.EntityLogicalName);

            Source.DLaB.Xrm.Extensions.WhereEqual(sut,
                Contact.Fields.StateCode, (int)ContactState.Active,
                LogicalOperator.And,
                Contact.Fields.FirstName, "Test",
                Contact.Fields.LastName, "Test2",
                LogicalOperator.Or,
                Contact.Fields.FirstName, "Or'd",
                Contact.Fields.LastName, "Or'd2"
            );

            AssertEqualString(@"SELECT 
FROM contact
WHERE
( ( contact.statecode = 0 ) 
And ( ( contact.firstname = 'Test' And contact.lastname = 'Test2' ) 
 Or ( contact.firstname = 'Or'd' And contact.lastname = 'Or'd2' ) 
) 
)", sut);
        }

        [TestMethod]
        public void WhereEqual_WithLogicalAndAndOr_Should_SplitConditions()
        {
            var sut = new QueryExpression(Contact.EntityLogicalName);

            Source.DLaB.Xrm.Extensions.WhereEqual(sut,
                Contact.Fields.StateCode, (int)ContactState.Active,
                LogicalOperator.And,
                Contact.Fields.FirstName, "Test",
                LogicalOperator.Or,
                Contact.Fields.FirstName, "Or'd"
            );

            AssertEqualString(@"SELECT 
FROM contact
WHERE
( ( contact.statecode = 0 ) 
And ( ( contact.firstname = 'Test' ) 
 Or ( contact.firstname = 'Or'd' ) 
) 
)", sut);
        }

        [TestMethod]
        public void WhereEqual_WithUnneededLogicalAnds_Should_Error()
        {
            try
            {
                var sut = new QueryExpression(Contact.EntityLogicalName);
                Source.DLaB.Xrm.Extensions.WhereEqual(sut,
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
                Source.DLaB.Xrm.Extensions.WhereEqual(sut,
                    Contact.Fields.StateCode, (int)ContactState.Active,
                    LogicalOperator.And,
                    Contact.Fields.FirstName, "Test",
                    LogicalOperator.And,
                    Contact.Fields.LastName, "Test2"
                );
                Assert.Fail("Expected Exception to be thrown");
            }
            catch(ArgumentException ex)
            {
                // Expected Argument Exception
                Assert.AreEqual("LogicalOperator.And can not be followed by another LogicalOperator.And!", ex.Message);
            }

            try
            {
                var sut = new QueryExpression(Contact.EntityLogicalName);
                Source.DLaB.Xrm.Extensions.WhereEqual(sut,
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

        [TestMethod]
        public void WhereEqual_WithLogicalAndPlusOneOrWithTwo_Should_SplitConditions()
        {
            var sut = new QueryExpression(Contact.EntityLogicalName);
            Source.DLaB.Xrm.Extensions.WhereEqual(sut,
                Contact.Fields.StateCode, (int)ContactState.Active,
                Contact.Fields.FirstName, "Test",
                LogicalOperator.And,
                Contact.Fields.LastName, "Test2",
                LogicalOperator.Or,
                Contact.Fields.FirstName, "Or'd",
                Contact.Fields.LastName, "Or'd2"
            );

            AssertEqualString(@"SELECT 
FROM contact
WHERE
( ( contact.statecode = 0 And contact.firstname = 'Test' ) 
And ( ( contact.lastname = 'Test2' ) 
 Or ( contact.firstname = 'Or'd' And contact.lastname = 'Or'd2' ) 
) 
)", sut);
        }

        [DebuggerHidden]
        private static void AssertEqualString(string expected, QueryExpression actual)
        {
            Assert.AreEqual(expected.Replace(@"
", Environment.NewLine), actual.GetSqlStatement().Trim());
        }
    }
}
