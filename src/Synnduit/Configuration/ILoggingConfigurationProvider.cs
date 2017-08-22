namespace Synnduit.Configuration
{
    /// <summary>
    /// Provides access to the configuration that controls the application's logging
    /// behavior.
    /// </summary>
    internal interface ILoggingConfigurationProvider
    {
        /// <summary>
        /// Gets the configuration that controls the application's migration logging
        /// behavior.
        /// </summary>
        IMigrationLoggingConfiguration MigrationConfiguration { get; }

        /// <summary>
        /// Gets the configuration that controls the application's garbage collection
        /// logging behavior.
        /// </summary>
        IGarbageCollectionLoggingConfiguration GarbageCollectionConfiguration { get; }
    }
}
