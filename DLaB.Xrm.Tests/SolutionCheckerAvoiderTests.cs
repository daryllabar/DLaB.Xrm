using Microsoft.VisualStudio.TestTools.UnitTesting;
using Source.DLaB.Xrm;

namespace Core.DLaB.Xrm.Tests
{
    [TestClass]
    public class SolutionCheckerAvoiderTests
    {
        [TestMethod]
        public void IlSpecifyColumn_CreatesColumnSetWithAllColumnsTrue()
        {
            var sut = SolutionCheckerAvoider.CreateColumnSetWithAllColumns();
            Assert.IsTrue(sut.AllColumns, "All columns should have been set to true.");
        }
    }
}
