using System;
using System.Activities;
using System.Linq;
using System.Threading;
using Microsoft.Xrm.Sdk.Extensions;
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
    /// Non-Generic Base Class for Custom Workflow Activities
    /// </summary>
    public abstract class DLaBCodeActivityBase : DLaBCodeActivityBase<DLaBExtendedWorkflowContext>
    {
        protected DLaBCodeActivityBase(IIocContainer? container = null): base(container)
        {
        }
    }

    /// <summary>
    /// Generic Base Class for Custom Workflow Activities
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public abstract class DLaBCodeActivityBase<TContext> : CodeActivity, IContainerWrapper where TContext : IExtendedWorkflowContext
    {
        private readonly Lazy<IIocContainer> _container;
        /// <summary>
        /// Container to use for Dependency Injection
        /// </summary>
        public IIocContainer Container => _container.Value;

        protected DLaBCodeActivityBase(IIocContainer? container = null)
        {
            container ??= new IocContainer();
            _container = new Lazy<IIocContainer>(() => RegisterServices(container), LazyThreadSafetyMode.ExecutionAndPublication);
        }

        /// <summary>
        /// Registers the services for the IocContainer for the custom code activity.
        /// Searches for the WorkflowServicesRegistrationRecorderAttribute in the code activity assembly, and if not found, defaults to container.RegisterDataverseWorkflowDefaults(this)
        /// If a custom code activity requires its own specific registrations, this should be overridden and more than likely, base.RegisterServices called first before stepping on the registrations with the custom code activity specific ones.
        /// This is called only once per custom code activity instance.
        /// </summary>
        /// <param name="container">The IocContainer instance.</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected virtual IIocContainer RegisterServices(IIocContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            var attribute = GetType().Assembly.GetCustomAttributes(typeof(WorkflowServicesRegistrationRecorderAttribute), false)
                .OfType<WorkflowServicesRegistrationRecorderAttribute>().FirstOrDefault();

            if (attribute == null)
            {
                return container.RegisterDataverseWorkflowDefaults(this);
            }

            if (!typeof(IWorkflowServicesRegistrationRecorder).IsAssignableFrom(attribute.Recorder))
            {
                throw new InvalidOperationException("The type in attribute.Recorder must implement IWorkflowServicesRegistrationRecorder.");
            }

            var recorder = (IWorkflowServicesRegistrationRecorder)Activator.CreateInstance(attribute.Recorder)!;
            return recorder.RegisterWorkflowServices(container);
        }

        /// <inheritdoc />
        protected override void Execute(CodeActivityContext codeActivityContext)
        {
            var serviceProvider = Container.BuilderServiceProvider(codeActivityContext);
            var context = serviceProvider.Get<TContext>();

            PreExecute(context, serviceProvider);

            try
            {
                using (context.TraceTime("{0}.Execute()", context.CodeActivityTypeName))
                {
                    // Invoke the custom implementation 
                    Execute(context, serviceProvider);
                }
            }
            catch (Exception e)
            {
                if (ExecuteExceptionHandler(e, context, serviceProvider))
                {
                    throw;
                }
            }
            finally
            {
                PostExecute(context, serviceProvider);
            }
        }

        /// <summary>
        /// The Execute method to override
        /// </summary>
        /// <param name="context">The Context.</param>
        /// <param name="serviceProvider">The IServiceProvider instance.</param>
        protected abstract void Execute(TContext context, IServiceProvider serviceProvider);

        /// <summary>
        /// Method that gets called when an exception occurs in the Execute method.  Return true if the exception should be rethrown.
        /// This prevents losing the stack trace by rethrowing the originally caught error.
        /// </summary>
        /// <param name="ex">The exception that occurred.</param>
        /// <param name="context">The IExtendedWorkflowContext instance.</param>
        /// <param name="serviceProvider">The IServiceProvider instance.</param>
        /// <returns>True if the exception should be rethrown, otherwise false.</returns>
        protected virtual bool ExecuteExceptionHandler(Exception ex, TContext context, IServiceProvider serviceProvider)
        {
            context.LogException(ex);
            // Unexpected Exception occurred, log exception then wrap and throw new exception
            if (context.IsolationMode == (int)IsolationMode.Sandbox)
            {
                Sandbox.ExceptionHandler.AssertCanThrow(ex);
            }
            return true;
        }

        /// <summary>
        /// Method that gets called before the Execute
        /// </summary>
        /// <param name="codeActivityContext">The CodeActivityContext instance.</param>
        /// <param name="serviceProvider">The IServiceProvider instance.</param>
        protected virtual void PreExecute(IExtendedWorkflowContext codeActivityContext, IServiceProvider serviceProvider) { }

        /// <summary>
        /// Method that gets called after the Execute
        /// </summary>
        /// <param name="context">The IExtendedWorkflowContext instance.</param>
        /// <param name="serviceProvider">The IServiceProvider instance.</param>
        protected virtual void PostExecute(TContext context, IServiceProvider serviceProvider) { }
    }
}