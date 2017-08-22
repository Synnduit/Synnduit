using System.Collections.Generic;

namespace Synnduit.Configuration
{
    /// <summary>
    /// Exposes the configuration that controls the application's migration logging
    /// behavior.
    /// </summary>
    internal interface IMigrationLoggingConfiguration
    {
        /// <summary>
        /// Gets the collection of <see cref="EntityTransactionOutcome"/> values
        /// identifying entity transactions that should be excluded from logging.
        /// </summary>
        IEnumerable<EntityTransactionOutcome> ExcludedOutcomes { get; }

        /// <summary>
        /// Gets a value indicating whether the source system entity should be recorded in
        /// the log.
        /// </summary>
        bool SourceSystemEntity { get; }

        /// <summary>
        /// Gets a value indicating whether the destination system entity should be
        /// recorded in the log.
        /// </summary>
        bool DestinationSystemEntity { get; }

        /// <summary>
        /// Gets a value indicating whether individual value changes should be recorded in
        /// the log.
        /// </summary>
        bool ValueChanges { get; }

        /// <summary>
        /// Gets or sets a value indicating whether an entity transaction should always be
        /// logged if there is at least one message associated with it.
        /// </summary>
        bool AlwaysLogMessages { get; }
    }
}
