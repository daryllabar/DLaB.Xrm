

using DLaB.Xrm.Entities;
using DLaB.Xrm.Test;
using DLaB.Xrm.Test.Core.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Source.DLaB.Xrm;
using System;
using System.Linq;

namespace Core.DLaB.Xrm.Tests
{
    [TestClass]
    public class ExtendedTracingServiceTests
    {
        #region FirstOrDefault

        [TestMethod]
        public void ExtendedTracingService_TraceLongerThanMax_Should_Retrace()
        {
            const int maxLength = 30;
            var trace = new FakeTraceService(new DebugLogger());
            var sut = new ExtendedTracingService(trace, maxLength);
            var value = "1234567_10_234567_20_234567_30_234567_40_234567_50";
            sut.Trace(value);
            Assert.AreEqual(string.Join(Environment.NewLine, trace.Traces.Select(t => t.Trace)), value);
            trace.Traces.Clear();
            sut.RetraceMaxLength();
            var combinedTrace = string.Join(string.Empty, trace.Traces.Select(t => Environment.NewLine + t.Trace));
            Assert.AreEqual(maxLength, combinedTrace.Length);
            Assert.AreEqual(combinedTrace, string.Format("{0}12345{0}{0}...{0}{0}40_234567_50", Environment.NewLine));

        }

        // ReSharper disable once InconsistentNaming
        private class FirstOrDefault : TestMethodClassBase
        {
            private struct Ids
            {
                public static readonly Id<Contact> Contact = new Id<Contact>("A19CFF4C-5599-4BD4-B24A-759A50643BB3");
            }

            protected override void InitializeTestData(IOrganizationService service)
            {
                new CrmEnvironmentBuilder().WithEntities<Ids>().Create(service);
            }

            protected override void Test(IOrganizationService service)
            {
                // Test Exists
                var contact = service.GetFirstOrDefault<Contact>();
                Assert.IsNotNull(contact);

                // Test Not Exists
                service.Delete((Id)Ids.Contact);
                contact = service.GetFirstOrDefault<Contact>();
                Assert.IsNull(contact);
            }
        }

        #endregion FirstOrDefault
    }
}
