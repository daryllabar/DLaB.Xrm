#if NET
using DataverseUnitTest;
using DataverseUnitTest.Builders;
#else
using DLaB.Xrm.Test.Builders;
#endif
using Microsoft.Xrm.Sdk;

namespace DLaB.Xrm.Test.Core.Builders
{
    /// <summary>
    /// Builder class for creating instances of IOrganizationService.
    /// </summary>
    public class OrganizationServiceBuilder : OrganizationServiceBuilderBase<OrganizationServiceBuilder>
    {
        /// <summary>
        /// Gets the current instance of the builder.
        /// </summary>
        protected override OrganizationServiceBuilder This => this;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationServiceBuilder"/> class using the default organization service.
        /// </summary>
        public OrganizationServiceBuilder() : this(TestBase.GetOrganizationService()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationServiceBuilder"/> class using the specified organization service.
        /// </summary>
        /// <param name="service">The organization service to use.</param>
        public OrganizationServiceBuilder(IOrganizationService service) : base(service) { }

        #endregion Constructors
    }
}