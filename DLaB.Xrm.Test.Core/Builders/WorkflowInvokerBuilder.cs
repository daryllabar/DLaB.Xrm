#if !NET
using DLaB.Xrm.Test.Builders;
using System.Activities;

namespace DLaB.Xrm.Test.Core.Builders
{
    public class WorkflowInvokerBuilder : WorkflowInvokerBuilderBase<WorkflowInvokerBuilder>
    {
        protected override WorkflowInvokerBuilder This => this;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowInvokerBuilder"/> class.
        /// </summary>
        /// <param name="workflow">The workflow to invoke.</param>
        public WorkflowInvokerBuilder(Activity workflow) : base(workflow)
        {

        }
    }
}
#endif
