#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Extensions;

#if DLAB_UNROOT_NAMESPACE || DLAB_XRM
using DLaB.Xrm.Ioc;

namespace DLaB.Xrm.Plugin
#else
using Source.DLaB.Xrm.Ioc;

namespace Source.DLaB.Xrm.Plugin
#endif

{
    /// <inheritdoc />
    /// <summary>
    /// Plugin Base.  Allows for Registered Events, preventing infinite loops, and auto logging.
    /// </summary>
#if !DLAB_XRM_DEBUG
    [DebuggerNonUserCode]
#endif
    public abstract class DLaBGenericPluginBase<T> : IExtendedPlugin where T : IExtendedPluginContext
    {
        #region Constants

        /// <summary>
        /// Key to look for in the Security Settings for Tracing the Pre Context
        /// </summary>
        public const string TracePreContext = "PluginBase.TracePreContext";
        /// <summary>
        /// Key to look for in the Security Settings for Tracing the Pre and Post Context
        /// </summary>
        public const string TracePrePostContext = "PluginBase.TraceContext";
        /// <summary>
        /// Key to look for in the Security Settings for Tracing the Post Context
        /// </summary>
        public const string TracePostContext = "PluginBase.TracePostContext";

        #endregion Constants

        #region Properties

        private readonly Lazy<IIocContainer> _container;
        /// <summary>
        /// Container to use for Dependency Injection.  Exposed for tests to be able to override registrations prior to execution.
        /// </summary>
        public IIocContainer Container => _container.Value;

        private IEnumerable<RegisteredEvent>? _events;
        /// <inheritdoc />
        public IEnumerable<RegisteredEvent> RegisteredEvents => _events ??= CreateEvents();

        /// <summary>
        /// Gets or sets the secure configuration.
        /// </summary>
        /// <value>
        /// The secure configuration.
        /// </value>
        public string? SecureConfig { get; }
        /// <summary>
        /// Gets or sets the unsecure configuration.
        /// </summary>
        /// <value>
        /// The unsecure configuration.
        /// </value>
        public string? UnsecureConfig { get; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the GenericPluginBase class.
        /// </summary>
        /// <param name="unsecureConfig"></param>
        /// <param name="secureConfig"></param>
        /// <param name="container"></param>
        protected DLaBGenericPluginBase(string? unsecureConfig, string? secureConfig, IIocContainer? container = null)
        {
            _lazyIsInitialized = new Lazy<bool>(LazyTriggeredInitialize);
            SecureConfig = secureConfig;
            UnsecureConfig = unsecureConfig;

            container ??= new IocContainer();
            // Because child classes should be able to override the registration, Lazy is used here so the initialization of the IIocContainer and subsequent call to RegisterServices
            // is delayed until after the base class is fully instantiated.
            _container = new Lazy<IIocContainer>(() => RegisterServices(container), LazyThreadSafetyMode.ExecutionAndPublication);
        }

        #endregion Constructors

        #region Abstract Methods

        /// <summary>
        /// The default method to be executed by the plugin.  The Registered Event could specify a different method.
        /// </summary>
        /// <param name="context">The plugin context.</param>
        protected abstract void ExecuteInternal(T context);

        /// <summary>
        /// Create the Registered Events for the plugin to operate on.
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerable<RegisteredEvent> CreateEvents();

        #endregion Abstract Methods

        #region Initialize

        // This used to be a simple lock, but the Solution Checker doesn't like locks, so this is an ugly Lazy implementation
        private volatile IServiceProvider _initializerServiceProvider = null!; // Maybe set multiple
        private readonly Lazy<bool> _lazyIsInitialized; // Initialized in Constructor

        private bool LazyTriggeredInitialize()
        {
            Initialize(_initializerServiceProvider);
            return true;
        }

        /// <summary>
        /// Called once directly before the plugin instance is executed for the first time.
        /// </summary>
        protected virtual void Initialize(IServiceProvider serviceProvider) { }

        private void CallInitializeOnFirstExecutionOnly(IServiceProvider serviceProvider)
        {
            if (!_lazyIsInitialized.IsValueCreated)
            {
                _initializerServiceProvider = serviceProvider;
                if (!_lazyIsInitialized.Value)
                {
                    throw new InvalidOperationException("Lazy Initialization Failed for plugin!");
                }
            }
        }

        #endregion Initialize

        /// <summary>
        /// Registers the services for the IocContainer for the plugin.
        /// Searches for the PluginServicesRegistrationRecorderAttribute in the plugin assembly, and if not found, defaults to container.RegisterDataversePluginDefaults(UnsecureConfig, SecureConfig, this)
        /// If a plugin requires its own specific registrations, this should be overridden and more than likely, base.RegisterServices called first before stepping on the registrations with the plugin specific ones.
        /// This is called only once per plugin instance.
        /// This must be overriden if T is not IExtendedPluginContext
        /// </summary>
        /// <param name="container">The IocContainer instance.</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected virtual IIocContainer RegisterServices(IIocContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            var attribute = GetType().Assembly.GetCustomAttributes(typeof(PluginServicesRegistrationRecorderAttribute), false)
                .OfType<PluginServicesRegistrationRecorderAttribute>().FirstOrDefault();
            
            if (attribute == null)
            {
                return container.RegisterDataversePluginDefaults(UnsecureConfig, SecureConfig, this);
            }

            if (!typeof(IPluginServicesRegistrationRecorder).IsAssignableFrom(attribute.Recorder))
            {
                throw new InvalidOperationException("The type in attribute.Recorder must implement IPluginServicesRegistrationRecorder.");
            }

            var recorder = (IPluginServicesRegistrationRecorder)Activator.CreateInstance(attribute.Recorder)!;
            return recorder.RegisterPluginServices(container, this, UnsecureConfig, SecureConfig);
        }

        #region Execute

        /// <summary>
        /// Executes the plug-in.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <exception cref="T:System.ArgumentNullException"></exception>
        /// <remarks>
        /// For improved performance, Microsoft Dynamics CRM caches plug-in instances.
        /// The plug-in's Execute method should be written to be stateless as the constructor is not called for every invocation of the plug-in.
        /// Also, multiple system threads could execute the plug-in at the same time.
        /// All per invocation state information is stored in the context.
        /// This means that you should not use class level fields/properties in plug-ins that are not multi-thread safe or execution context specific.
        /// </remarks>
        public void Execute(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            serviceProvider = InjectServiceProvider(serviceProvider);

            CallInitializeOnFirstExecutionOnly(serviceProvider);

            PreExecute(serviceProvider);

            var context = serviceProvider.Get<T>();
            AssertContextIsNotNull(context);

            try
            {
                using (context.TraceTime("{0}.Execute()", context.PluginTypeName))
                {
                    if (IsPreContextTraced(context))
                    {
                        context.TraceContext();
                    }

                    if (context.Event == null)
                    {
                        context.Trace("No Registered Event Found for Event: {0}, Entity: {1}, and Stage: {2}!", context.MessageName, context.PrimaryEntityName, context.Stage);
                        return;
                    }

                    if (PreventRecursiveCall(context))
                    {
                        context.Trace("Duplicate Recursive Call Prevented!");
                        return;
                    }

                    if (context.HasPluginExecutionBeenPrevented())
                    {
                        context.Trace("Context has Specified Call to be Prevented!");
                        return;
                    }

                    if (SkipExecution(context))
                    {
                        context.Trace("Execution Has Been Skipped!");
                        return;
                    }

                    ExecuteRegisteredEvent(context);

                    if (IsPostContextTraced(context))
                    {
                        context.TraceContext();
                    }
                }
            }
            catch (Exception ex)
            {
                if (ExecuteExceptionHandler(ex, context))
                {
                    throw;
                }
            }
            finally
            {
                PostExecute(context);
            }
        }

        /// <summary>
        /// Allows for Injecting the Service Provider for the Plugin Execution via the IIoCServiceProviderBuilder.  Sets the created injected service via the IIoCServiceProviderBuilder 
        /// </summary>
        /// <param name="serviceProvider">The Service Provider from Dataverse.</param>
        /// <returns></returns>
        protected virtual IServiceProvider InjectServiceProvider(IServiceProvider serviceProvider)
        {
            // The default dataverse serviceProvider will not have an IIoCServiceProviderBuilder, so the default Container.BuildServiceProvider will be used, but tests can inject their own builder that overrides registrations.
            var serviceProviderBuilder = serviceProvider.Get<IIocServiceProviderBuilder>();
            if (serviceProviderBuilder == null)
            {
                return Container.BuildServiceProvider(serviceProvider);
            }

            return serviceProviderBuilder.BuildServiceProvider(serviceProvider, Container);
        }

        private static void AssertContextIsNotNull(T context)
        {
            if (context == null)
            {
                if (typeof(T) == typeof(IExtendedPluginContext))
                {
                    throw new InvalidPluginExecutionException(
                        $"Unable to create context of type {typeof(T).FullName}!  When overriding RegisterServices either call base.RegisterServices() or explicitly define a registration for IExtendedPluginContext with the container.");
                }

                throw new InvalidPluginExecutionException(
                    $"Unable to create context of type {typeof(T).FullName}!  Override the RegisterServices method and explicitly define a registration for IExtendedPluginContext with the container.");
            }
        }

        /// <summary>
        /// Method that gets called when an exception occurs in the Execute method.  Return true if the exception should be rethrown.
        /// This prevents losing the stack trace by rethrowing the originally caught error.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual bool ExecuteExceptionHandler(Exception ex, T context)
        {
            context.LogException(ex);
            // Unexpected Exception occurred, log exception then wrap and throw new exception
            if (context.IsolationMode == IsolationMode.Sandbox)
            {
                Sandbox.ExceptionHandler.AssertCanThrow(ex);
            }
            return true;
        }

        /// <summary>
        /// Method that gets called before the Execute
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        protected virtual void PreExecute(IServiceProvider serviceProvider) { }

        /// <summary>
        /// Method that gets called in the finally block of the Execute
        /// </summary>
        /// <param name="context">The context.</param>
        protected virtual void PostExecute(T context)
        {
            if (context.TracingService is IMaxLengthTracingService maxLengthService)
            {
                maxLengthService.RetraceMaxLength();
            }
        }

        /// <summary>
        /// Method that gets called directly before Execute(context).  Returning true will skip the Execute(context) from getting called.  
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual bool SkipExecution(T context)
        {
            var @event = context.Event;
            var skip = @event.RequirementValidator != null 
                       && @event.RequirementValidator.SkipExecution(context);

            if (skip)
            {
                context.Trace("The requirements for plugin execution were not met!  Skipping execution...");
            }
            else
            {
                @event.AssertRequirements(context);
            }

            return skip;
        }

        /// <summary>
        /// Traces the Execution of the registered event of the context.
        /// </summary>
        /// <param name="context">The context.</param>
        private void ExecuteRegisteredEvent(T context)
        {
            var execute = context.Event.Execute == null ? ExecuteInternal : new Action<T>(c => context.Event.Execute(c));

            context.Trace("{0}.{1} is Executing for Entity: {2}, Message: {3}",
                context.PluginTypeName,
                context.Event.ExecuteMethodName,
                context.PrimaryEntityName,
                context.MessageName);

            execute(context);
        }

        /// <summary>
        /// Allows Plugin to trigger itself.  Delete Message Types always return False since you can't delete something twice, all other message types return true if the execution key is found in the shared parameters.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual bool PreventRecursiveCall(T context)
        {
            if (context.Event.Message == MessageType.Delete)
            {
                return false;
            }

            var key = $"{context.PluginTypeName}|{context.Event.MessageName}|{context.Event.Stage}|{context.PrimaryEntityId}";
            if (context.GetFirstSharedVariable<int>(key) > 0)
            {
                return true;
            }

            context.SharedVariables.Add(key, 1);
            return false;
        }

        /// <summary>
        /// Determines if the Context should be traced Pre Execution of the plugin logic
        /// </summary>
        protected virtual bool IsPreContextTraced(T context) { return ContainsAnyIgnoreCase(SecureConfig, TracePreContext, TracePrePostContext); }
        /// <summary>
        /// Determines if the Context should be traced Post Execution of the plugin logic
        /// </summary>
        protected virtual bool IsPostContextTraced(T context) { return ContainsAnyIgnoreCase(SecureConfig, TracePostContext, TracePrePostContext); }

        private static bool ContainsAnyIgnoreCase(string? source, params string[] values)
        {
            return source != null
                && values.Any(v => CultureInfo.InvariantCulture.CompareInfo.IndexOf(source, v, CompareOptions.IgnoreCase) >= 0);
        }

        #endregion Execute
    }
}
