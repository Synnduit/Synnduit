using System;

namespace Synnduit.Persistence
{
    /// <summary>
    /// Represents a shared source system identifier record.
    /// </summary>
    public interface ISharedSourceSystemIdentifier
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
        /// Gets the group number; source systems belonging to the same group (number)
        /// share identifiers for the entity type.
        /// </summary>
        int GroupNumber { get; }
    }
}
