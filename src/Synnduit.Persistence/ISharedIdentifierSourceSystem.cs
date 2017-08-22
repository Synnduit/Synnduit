using System;

namespace Synnduit.Persistence
{
    /// <summary>
    /// Represents a record that specifies a source system that a given source system
    /// shares identifiers for a given entity type with.
    /// </summary>
    public interface ISharedIdentifierSourceSystem
    {
        /// <summary>
        /// Gets the ID of the source (external) system.
        /// </summary>
        Guid SourceSystemId { get; }

        /// <summary>
        /// Gets the ID of the entity type.
        /// </summary>
        Guid EntityTypeId { get; }

        /// <summary>
        /// Gets the ID of the source (external) system that the source system represented
        /// by the current instance shares identifiers for the entity type represented by
        /// the current instance with.
        /// </summary>
        Guid SharedIdentifierSourceSystemId { get; }
    }
}
