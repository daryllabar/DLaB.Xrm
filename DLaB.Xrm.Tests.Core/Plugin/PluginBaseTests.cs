using System;
using System.Collections.Generic;
using System.Linq;
using DLaB.Xrm.Test;
using DLaB.Xrm.Test.Core.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Extensions;
#if DLAB_UNROOT_NAMESPACE || DLAB_XRM
using DLaB.Xrm.Ioc;
using DLaB.Xrm.Plugin
#else
using DataverseUnitTest;
using Source.DLaB.Xrm.Ioc;
using Source.DLaB.Xrm.Plugin;
#endif



namespace DLaB.Xrm.Tests.Core.Plugin
{
    [TestClass]
    public class PluginBaseTests
    {
        [TestMethod]
        public void TestPlugin_Should_Execute()
        {
            var plugin = new TestPlugin(null, null);
            var context = new PluginExecutionContextBuilder()
                .WithFirstRegisteredEvent(plugin)
                .Build();
            var serviceProvider = new ServiceProviderBuilder(null, context, new DebugLogger())
                .Build();

            plugin.Execute(serviceProvider);
            var traces = serviceProvider.GetFake<FakeTraceService>().Traces;
            
            Assert.IsTrue(traces.Any(t => t.Trace == TestPlugin.Traces.Executed));
        }
    }

    public class TestPlugin : DLaBPluginBase
    {
        public struct Traces
        {
            public const string Executed = "Test Plugin Executed!";
        }
        public TestPlugin(string unsecureConfig, string secureConfig, IIocContainer container = null) : base(unsecureConfig, secureConfig, container)
        {
        }

        protected override void ExecuteInternal(IExtendedPluginContext context)
        {
            context.Trace(Traces.Executed);

            var traceHistory = context.GetRequiredService<Source.DLaB.Xrm.IHistoricalTracingService>().GetTraceHistory();
            if (!traceHistory.Contains(Traces.Executed))
            {
                throw new Exception("Unexpected Trace History: " + traceHistory);
            }
        }

        protected override IEnumerable<RegisteredEvent> CreateEvents()
        {
            return new List<RegisteredEvent> {
                new (PipelineStage.PreOperation, MessageType.Create)
            };
        }
    }

    public class TestIocServiceProviderBuilder: IIocServiceProviderBuilder
    {
        public List<IServiceProvider> BuiltServiceProviders { get; } = new();

        public IServiceProvider BuildServiceProvider(IServiceProvider provider, IIocContainer container)
        {
            container.AddSingleton<ITracingService>(provider.Get<ExtendedTracingService>());
            var newProvider = container.BuildServiceProvider(provider);
            BuiltServiceProviders.Add(newProvider);
            return newProvider;
        }
    }
}
