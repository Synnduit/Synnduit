namespace Synnduit.Configuration
{
    /// <summary>
    /// Provides access to base, application, run, and run segment configuration objects.
    /// </summary>
    internal interface IConfigurationProvider
    {
        /// <summary>
        /// Gets the current base configuration; a null reference will be returned if the
        /// current application configuration has no base configuration.
        /// </summary>
        BaseConfiguration BaseConfiguration { get; }

        /// <summary>
        /// Gets the current application configuration.
        /// </summary>
        ApplicationConfiguration ApplicationConfiguration { get; }

        /// <summary>
        /// Gets the current run configuration.
        /// </summary>
        RunConfiguration RunConfiguration { get; }

        /// <summary>
        /// Gets the current run segment configuration.
        /// </summary>
        SegmentConfiguration SegmentConfiguration { get; }
    }
}
