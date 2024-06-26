using DLaB.Xrm.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Source.DLaB.Xrm;
using System;
using System.Linq;

namespace Core.DLaB.Xrm.Tests
{
    [TestClass]
    public class ExtendedTracingServiceTests
    {
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

        [TestMethod]
        public void GetTraceHistory_Should_ReturnTraces()
        {
            var trace = new FakeTraceService(new DebugLogger());
            var sut = new ExtendedTracingService(trace);
            sut.Trace("Hello");
            sut.Trace("World");
            var expected = "Hello" + Environment.NewLine + "World" + Environment.NewLine;
            Assert.AreEqual(expected, sut.GetTraceHistory());
            
            sut.Trace("Goodbye {0}", "Cruel World!");
            Assert.AreEqual(expected + "Goodbye Cruel World!" + Environment.NewLine, sut.GetTraceHistory());
        }
    }
}
