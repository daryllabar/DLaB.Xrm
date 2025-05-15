using System;

#if DLAB_UNROOT_NAMESPACE || DLAB_XRM_WORKFLOW
namespace DLaB.Xrm.Workflow
#else
namespace Source.DLaB.Xrm.Workflow
#endif
{
    /// <summary>
    /// Attribute used to declare the Type of <see cref="IWorkflowServicesRegistrationRecorder"/> that will be invoked and used to register services
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public sealed class WorkflowServicesRegistrationRecorderAttribute: Attribute
    {
        /// <summary>
        /// The Recorder Type to record all the registrations to the Service Provider.
        /// </summary>
        public Type Recorder { get; }

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        /// <param name="recorder">The Type of the <see cref="IWorkflowServicesRegistrationRecorder"/> class to register.</param>
        public WorkflowServicesRegistrationRecorderAttribute(Type recorder)
        {
            Recorder = recorder;
        }
    }
}
