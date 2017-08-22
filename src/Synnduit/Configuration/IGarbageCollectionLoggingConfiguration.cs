using System.Collections.Generic;

namespace Synnduit.Configuration
{
    /// <summary>
    /// Exposes the configuration that controls the application's garbage collection
    /// logging behavior.
    /// </summary>
    internal interface IGarbageCollectionLoggingConfiguration
    {
        /// <summary>
        /// Gets the collection of <see cref="EntityDeletionOutcome"/> values identifying
        /// entity deletions that should be excluded from logging.
        /// </summary>
        IEnumerable<EntityDeletionOutcome> ExcludedOutcomes { get; }

        /// <summary>
        /// Gets a value indicating whether the destination system entity that is
        /// being deleted should be recorded in the log.
        /// </summary>
        bool Entity { get; }

        /// <summary>
        /// Gets or sets a value indicating whether an entity deletion should always be
        /// logged if there is at least one message associated with it.
        /// </summary>
        bool AlwaysLogMessages { get; }
    }
}
