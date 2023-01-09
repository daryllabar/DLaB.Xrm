using System;
using Microsoft.Xrm.Sdk;

#if DLAB_UNROOT_NAMESPACE || DLAB_XRM
namespace DLaB.Xrm.Plugin
#else
namespace Source.DLaB.Xrm.Plugin
#endif
{
    /// <summary>
    /// Contains the Validator and the Exception to throw
    /// </summary>
    public struct AssertValidator
    {
        /// <summary>
        /// The Validator
        /// </summary>
        public IRequirementValidator Validator { get; set; }
        /// <summary>
        /// The Exception
        /// </summary>
        public Exception ExceptionToThrow { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="validator">The Validator</param>
        /// <param name="exceptionToThrow">The Exception to Throw</param>
        public AssertValidator(IRequirementValidator validator, Exception exceptionToThrow)
        {
            Validator = validator;
            ExceptionToThrow = exceptionToThrow;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="validator">The Validator</param>
        /// <param name="errorMessage">The error message to Throw as an InvalidPluginExecutionException</param>
        public AssertValidator(IRequirementValidator validator, string errorMessage)
        {
            Validator = validator;
            ExceptionToThrow = new InvalidPluginExecutionException(errorMessage);
        }
    }
}
