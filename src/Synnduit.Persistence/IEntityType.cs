using System;

namespace Synnduit.Persistence
{
    /// <summary>
    /// Represents an entity type.
    /// </summary>
    public interface IEntityType
    {
        /// <summary>
        /// Gets the ID of the entity type.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the ID of the entity type's parent destination (external) system.
        /// </summary>
        Guid DestinationSystemId { get; }

        /// <summary>
        /// Gets the name of the entity type.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the assembly qualified name of the type that represents the entity.
        /// </summary>
        string TypeName { get; }

        /// <summary>
        /// Gets the full name of the type that represents the entity type's sink.
        /// </summary>
        string SinkTypeFullName { get; }

        /// <summary>
        /// Gets the full name of the type that represents the entity type's cache feed;
        /// optional, may be null.
        /// </summary>
        string CacheFeedTypeFullName { get; }

        /// <summary>
        /// Gets a value indicating whether instances of the entity type are mutable; i.e.,
        /// whether or not they may change between runs.
        /// </summary>
        bool IsMutable { get; }

        /// <summary>
        /// Gets a value indicating whether source system instances of the entity type may
        /// be duplicates (i.e., represent the same destination system entity); in other
        /// words, this value indicates whether or not source system entity instances
        /// should be deduplicated.
        /// </summary>
        bool IsDuplicable { get; }
    }
}
