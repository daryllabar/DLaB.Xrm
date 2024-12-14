#nullable enable
using Microsoft.Xrm.Sdk.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

#if DLAB_UNROOT_NAMESPACE || DLAB_XRM
namespace DLaB.Xrm.Ioc
#else
namespace Source.DLaB.Xrm.Ioc
#endif
{
    /// <summary>
    /// Allows for registering or overriding service registrations
    /// </summary>
    public interface IIocServiceProviderBuilder
    {
        /// <summary>
        /// Gets the list of built service providers.
        /// </summary>
        /// <value>
        /// The list of built service providers.
        /// </value>
        public List<IServiceProvider> BuiltServiceProviders { get; }

        /// <summary>
        /// Creates the service provider.
        /// </summary>
        /// <param name="provider">The Dataverse provider.</param>
        /// <param name="container">The container.</param>
        /// <returns>The created service provider.</returns>
        IServiceProvider BuildServiceProvider(IServiceProvider provider, IIocContainer container);
    }

    
    /// <summary>
    /// Extension methods for IIocServiceProviderBuilder.
    /// </summary>
    public static class IIocServiceProviderBuilderExtensions
    {
        /// <summary>
        /// Determines whether the specified service provider has any services built using an IIocServiceProviderBuilder
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <returns>
        ///   <c>true</c> if the specified service provider has any IoC services; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasIocService(this IServiceProvider serviceProvider)
        {
            var builder = serviceProvider.Get<IIocServiceProviderBuilder>();
            return builder.BuiltServiceProviders.Any();
        }

        /// <summary>
        /// Gets the latest service provider build using the IIocServiceProviderBuilder in the service provider.  Throws an exception if the service provider does not have a IIocServiceProviderBuilder.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <returns>The latest IoC service.</returns>
        public static IServiceProvider GetLatestIocService(this IServiceProvider serviceProvider)
        {
            var builder = serviceProvider.GetRequiredService<IIocServiceProviderBuilder>();
            return builder.BuiltServiceProviders.Last();
        }
    }
}
