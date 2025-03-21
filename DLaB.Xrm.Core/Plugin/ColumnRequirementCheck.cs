﻿#if DLAB_UNROOT_NAMESPACE || DLAB_XRM
namespace DLaB.Xrm.Plugin
#else
namespace Source.DLaB.Xrm.Plugin
#endif
{
    /// <summary>
    /// The type of Column Requirement
    /// </summary>
    public enum ColumnRequirementCheck
    {
        /// <summary>
        /// Determines if the target contains the attribute with a (nullable) value, and the value is different from the value in the pre-image.
        /// </summary>
        Changed,
        /// <summary>
        /// Determines if the target contains the attribute with a null value, and the value is different from the value in the pre-image.
        /// </summary>
        Cleared,
        /// <summary>
        /// Determines if the entity contains all the defined attributes with a (non-null) value, or if the Entity overload is used, the specified values.
        /// </summary>
        Contains,
        /// <summary>
        /// Determines if the entity contains all the defined attributes with a null value.
        /// </summary>
        ContainsNull,
        /// <summary>
        /// Determines if the entity contains all the defined attributes with a (nullable) value.
        /// </summary>
        ContainsNullable,
        /// <summary>
        /// Determines if the entity contains all the defined attributes with a specific value.
        /// </summary>
        ContainsValue,
        /// <summary>
        /// Determines if the entity is missing all the defined attributes from its attribute collection.
        /// </summary>
        Missing,
        /// <summary>
        /// Determines if the entity is either missing all the defined attributes from its attribute collection, or if they are present, they are null.
        /// </summary>
        MissingOrNull,
        /// <summary>
        /// Determines the entity does not contain all the defined attributes with the specific values.
        /// </summary>
        Not,
        /// <summary>
        /// Used for other/custom validators
        /// </summary>
        Other,
        /// <summary>
        /// Determines if the target contains the attribute with a (non-null) value, and the value is different from the value in the pre-image.
        /// </summary>
        Updated,
        /// <summary>
        /// Determines if the target contains the attribute with the defined value, and the value is different from the value in the pre-image.
        /// </summary>
        UpdatedValue
    }
}