#if NET
using DataverseUnitTest;
using DataverseUnitTest.Builders;
#else
using DLaB.Xrm.Test.Builders;
#endif

namespace DLaB.Xrm.Test.Core.Builders
{
    /// <summary>
    /// Class to simplify the simplest cases of creating entities without changing the defaults.
    /// </summary>
    public class CrmEnvironmentBuilder : CrmEnvironmentBuilderBase<CrmEnvironmentBuilder>
    {
        protected override CrmEnvironmentBuilder This => this;

        #region Fluent Methods

        #endregion Fluent Methods
    }
}
