using Microsoft.Xrm.Sdk;

#if DLAB_UNROOT_NAMESPACE || DLAB_XRM
namespace DLaB.Xrm.Plugin
#else
namespace Source.DLaB.Xrm.Plugin
#endif
{
    /// <summary>
    /// Resolves the Wrapped Lazy creation of different Organization Services
    /// </summary>
    public class OrganizationServicesResolver: IOrganizationServicesWrapper
    {
        private readonly OrganizationServicesWrapper _services;
        /// <inheritdoc />
        public IOrganizationService Cached => _services.Cached.Value;
        /// <inheritdoc />
        public IOrganizationService InitiatingUser => _services.InitiatingUser.Value;
        /// <inheritdoc />
        public IOrganizationService Organization => _services.Organization.Value;
        /// <inheritdoc />
        public IOrganizationService System => _services.System.Value;

        /// <summary>
        /// Initializes a new instance of the OrganizationServicesResolver class.
        /// </summary>
        /// <param name="services">The OrganizationServicesWrapper to use the Lazy implementations of to resolve the OrganizationServices.</param>
        public OrganizationServicesResolver(OrganizationServicesWrapper services)
        {
            _services = services;
        }
    }

    public interface IOrganizationServicesWrapper
    {   
        /// <summary>
        /// A service that will cache the retrieve/retrieve multiple results and reuse them.  Uses the System Organization Service to prevent different users from retrieving different results.
        /// </summary>
        public IOrganizationService Cached { get; }
        /// <summary>
        /// The IOrganizationService of the plugin, Impersonated as the user that triggered the services using the PluginExecutionContext.InitiatingUserId.
        /// </summary>
        public IOrganizationService InitiatingUser { get; }
        /// <summary>
        /// The IOrganizationService of the plugin, Impersonated as the user that plugin is registered to run as, using the PluginExecutionContext.UserId.
        /// </summary>
        public IOrganizationService Organization { get; }
        /// <summary>
        /// The IOrganizationService of the plugin, using the System User by not specifying a UserId.
        /// </summary>
        public IOrganizationService System { get; }

    }
}
