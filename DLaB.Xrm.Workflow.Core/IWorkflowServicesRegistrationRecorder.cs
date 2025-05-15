using System;
using System.Activities;
#if DLAB_UNROOT_NAMESPACE || DLAB_XRM_WORKFLOW
using DLaB.Xrm.Ioc;

namespace DLaB.Xrm.Workflow
#else
using Source.DLaB.Xrm.Ioc;

namespace Source.DLaB.Xrm.Workflow
#endif
{
    /// <summary>
    /// Represents a contract for recording the registration of plugin services.
    /// </summary>
    public interface IWorkflowServicesRegistrationRecorder
    {
        /// <summary>
        /// Registers the Workflow services in the specified IoC container.
        /// </summary>
        /// <param name="container">The IoC container.</param>
        /// <param name="codeActivity">The customer workflow Code Activity.</param>
        /// <returns>The updated IoC container.</returns>
        IIocContainer RegisterWorkflowServices(IIocContainer container, CodeActivity? codeActivity = null);
    }
}
