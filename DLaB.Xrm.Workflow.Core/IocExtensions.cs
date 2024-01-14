using Microsoft.Xrm.Sdk.Extensions;
using System;
using System.Activities;

#if DLAB_UNROOT_NAMESPACE || DLAB_XRM
using DLaB.Xrm.Ioc;
using DLaB.Xrm.Plugin;
#else
using Source.DLaB.Xrm.Ioc;
using Source.DLaB.Xrm.Plugin;
#endif

#if DLAB_UNROOT_NAMESPACE || DLAB_XRM_WORKFLOW
namespace DLaB.Xrm.Workflow
#else
namespace Source.DLaB.Xrm.Workflow
#endif
{
    /// <summary>
    /// Extensions for Ioc
    /// </summary>
    public static class IocExtensions
    {
        public static IServiceProvider BuilderServiceProvider(this IIocContainer container, CodeActivityContext context, Lifetime defaultLifetime = Lifetime.Scoped)
        {
            return container.BuildServiceProvider(new CodeActivityContextServiceProvider(context), defaultLifetime);
        }


        /// <summary>
        /// Registers the default types for use within the context of a Dataverse Workflow Custom Code Activity.
        /// Assumes the container will have a service provider with an IPluginExecutionContext, IOrganizationServiceFactory, and ITracingService registered.
        /// </summary>
        /// <param name="container">The IoC container.</param>
        /// <param name="codeActivity">The Code Activity</param>
        /// <returns>The IoC container.</returns>
        public static IIocContainer RegisterDataverseWorkflowDefaults(this IIocContainer container, CodeActivity codeActivity)
        {
            container.RegisterDataversePluginDefaults()
                // CodeActivity
                .AddSingleton(codeActivity)

                // IExtendedWorkflowContext
                .AddScoped<IExtendedWorkflowContext, DLaBExtendedWorkflowContext>();

            return container;
        }
    }
}
