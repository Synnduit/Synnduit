using System;

namespace Synnduit.Persistence
{
    /// <summary>
    /// Represents an entity deletion (i.e., the deletion of a destination system entity).
    /// </summary>
    public interface IEntityDeletion : IOperation
    {
        /// <summary>
        /// Gets the ID of the entity type.
        /// </summary>
        Guid EntityTypeId { get; }

        /// <summary>
        /// Gets the ID that uniquely identifies the entity in the destination system.
        /// </summary>
        string DestinationSystemEntityId { get; }

        /// <summary>
        /// Gets the outcome of the deletion (code).
        /// </summary>
        int Outcome { get; }
    }
}
