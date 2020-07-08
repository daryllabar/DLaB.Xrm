/*
 * Pulled from https://github.com/carltoncolter/DynamicsPlugin/blob/master/DynamicsPlugin.Tests/PluginContainer.cs with modifications to not be plugin specific
 */
using System;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Text.RegularExpressions;

namespace Core.DLaB.Xrm.Tests.Sandbox
{
    public static class SandboxWrapper
    {
        public static T Instantiate<T>(object[] constructorArguments = null)
        {
            return new SandboxWrapper<T>().Instantiate(constructorArguments);
        }

        public static T InstantiatePlugin<T>(string unsecureConfig = null, string secureConfig = null)
        {
            object[] args = null;
            if (secureConfig == null)
            {
                if (unsecureConfig != null)
                {
                    args = new object[] {unsecureConfig};
                }
            }
            else
            {
                args = new object[]{unsecureConfig, secureConfig};
            }

            return new SandboxWrapper<T>().Instantiate(args);
        }
    }

    public class SandboxWrapper<T> : MarshalByRefObject, IDisposable
    {
        private const string DomainSuffix = "Sandbox";
        /// <summary>
        /// The Sandbox AppDomain to execute the plugin
        /// </summary>
        public AppDomain SandboxedAppDomain { get; private set; }

        public T Instantiate(object[] constructorArguments = null)
        {
            /*
             * Sandboxed plug-ins and custom workflow activities can access the network through the HTTP and HTTPS protocols. This capability provides 
               support for accessing popular web resources like social sites, news feeds, web services, and more. The following web access restrictions
               apply to this sandbox capability.
                * Only the HTTP and HTTPS protocols are allowed.
                * Access to localhost (loopback) is not permitted.
                * IP addresses cannot be used. You must use a named web address that requires DNS name resolution.
                * Anonymous authentication is supported and recommended. There is no provision for prompting the 
                  on user for credentials or saving those credentials.
             */
            constructorArguments = constructorArguments ?? new object[] { };
            var type = typeof(T);
            var source = type.Assembly.Location;
            var sourceAssembly = Assembly.UnsafeLoadFrom(source);

            var setup = new AppDomainSetup
            {
                ApplicationBase = AppDomain.CurrentDomain.BaseDirectory,
                ApplicationName = $"{sourceAssembly.GetName().Name}{DomainSuffix}",
                DisallowBindingRedirects = true,
                DisallowCodeDownload = true,
                DisallowPublisherPolicy = true
            };

            var ps = new PermissionSet(PermissionState.None);
            ps.AddPermission(new SecurityPermission(SecurityPermissionFlag.SerializationFormatter));
            ps.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
            ps.AddPermission(new FileIOPermission(PermissionState.None));
            ps.AddPermission(new ReflectionPermission(ReflectionPermissionFlag.RestrictedMemberAccess));

            //RegEx pattern taken from: https://msdn.microsoft.com/en-us/library/gg334752.aspx
            ps.AddPermission(new WebPermission(NetworkAccess.Connect,
                new Regex(
                    @"^http[s]?://(?!((localhost[:/])|(\[.*\])|([0-9]+[:/])|(0x[0-9a-f]+[:/])|(((([0-9]+)|(0x[0-9A-F]+))\.){3}(([0-9]+)|(0x[0-9A-F]+))[:/]))).+")));

            // We don't need to add these, but it is important to note that there is no access to the following
            ps.AddPermission(new NetworkInformationPermission(NetworkInformationAccess.None));
            ps.AddPermission(new EnvironmentPermission(PermissionState.None));
            ps.AddPermission(new RegistryPermission(PermissionState.None));
            ps.AddPermission(new EventLogPermission(PermissionState.None));


            SandboxedAppDomain = AppDomain.CreateDomain(DomainSuffix, null, setup, ps, null);

            return Create(constructorArguments);
        }

        private T Create(object[] constructorArguments)
        {
            var type = typeof(T);

            return (T)Activator.CreateInstanceFrom(
                SandboxedAppDomain,
                type.Assembly.ManifestModule.FullyQualifiedName,
                // ReSharper disable once AssignNullToNotNullAttribute
                type.FullName, false, BindingFlags.CreateInstance,
                null, constructorArguments,
                CultureInfo.CurrentCulture, null
            ).Unwrap();
        }

        #region IDisposable Support
        //Implementing IDisposable Pattern: https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/dispose-pattern
        private bool _disposed; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                if (SandboxedAppDomain != null)
                {
                    AppDomain.Unload(SandboxedAppDomain);
                    SandboxedAppDomain = null;
                }
            }

            _disposed = true;
        }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}