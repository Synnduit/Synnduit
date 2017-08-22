using System;

namespace Synnduit.Persistence
{
    /// <summary>
    /// Represents the identity that uniquely identifies a source system entity.
    /// </summary>
    public interface ISourceSystemEntityIdentity
    {
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
    }
}
