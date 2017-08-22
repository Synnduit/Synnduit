using System;

namespace Synnduit.Persistence
{
    /// <summary>
    /// Represents an entity source/destination system identifier mapping.
    /// </summary>
    public interface IMapping
    {
        /// <summary>
        /// Gets the ID of the mapping.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the current operation.
        /// </summary>
        IOperation Operation { get; }

        /// <summary>
        /// Gets the serialized source system entity.
        /// </summary>
        ISerializedEntity SourceSystemEntity { get; }

        /// <summary>
        /// Gets the ID of the entity type.
        /// </summary>
        Guid EntityTypeId { get; }

        /// <summary>
        /// Gets the ID of the source (external) system.
        /// </summary>
        Guid SourceSystemId { get; }

        /// <summary>
        /// Gets the ID that uniquely identifies the entity in the source system.
        /// </summary>
        string SourceSystemEntityId { get; }

        /// <summary>
        /// Gets the ID that uniquely identifies the entity in the destination system.
        /// </summary>
        string DestinationSystemEntityId { get; }

        /// <summary>
        /// Gets the mapping's origin (code).
        /// </summary>
        int Origin { get; }

        /// <summary>
        /// Gets the mapping's state (code).
        /// </summary>
        int State { get; }
    }
}
