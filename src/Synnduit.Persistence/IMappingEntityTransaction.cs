using System;

namespace Synnduit.Persistence
{
    /// <summary>
    /// Represents a mapping entity transaction (i.e., a transaction with a known entity
    /// source/destination system identifier mapping).
    /// </summary>
    public interface IMappingEntityTransaction : IEntityTransaction
    {
        /// <summary>
        /// Gets the ID of the entity source/destination system identifier mapping that the
        /// transaction is associated with.
        /// </summary>
        Guid MappingId { get; }
    }
}
