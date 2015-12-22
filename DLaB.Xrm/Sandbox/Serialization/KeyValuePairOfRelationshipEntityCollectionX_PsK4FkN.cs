﻿using Microsoft.Xrm.Sdk;
// ReSharper disable InconsistentNaming

namespace DLaB.Xrm.Sandbox.Serialization
{
    /// <summary>
    /// Sandbox Serialization Safe KeyValuePairOfRelationship
    /// </summary>
    public class KeyValuePairOfRelationshipEntityCollectionX_PsK4FkN
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public Relationship key { get; set; }
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public SerializableEntityCollection value { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyValuePairOfRelationshipEntityCollectionX_PsK4FkN"/> class.
        /// </summary>
        public KeyValuePairOfRelationshipEntityCollectionX_PsK4FkN() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyValuePairOfRelationshipEntityCollectionX_PsK4FkN"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public KeyValuePairOfRelationshipEntityCollectionX_PsK4FkN(Relationship key, SerializableEntityCollection value)
        {
            this.key = key;
            this.value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyValuePairOfRelationshipEntityCollectionX_PsK4FkN"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public KeyValuePairOfRelationshipEntityCollectionX_PsK4FkN(Relationship key, EntityCollection value): this(key, new SerializableEntityCollection(value))
        {
        }
    }
}
