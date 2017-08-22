using System;

namespace Synnduit.Persistence
{
    /// <summary>
    /// Represents an entity feed.
    /// </summary>
    public interface IFeed
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
        /// Gets the full name of the type that represents the feed.
        /// </summary>
        string FeedTypeFullName { get; }
    }
}
