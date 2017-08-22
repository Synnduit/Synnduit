namespace Synnduit.Configuration
{
    /// <summary>
    /// Loads individual application and base configuration files.
    /// </summary>
    internal interface IConfigurationLoader
    {
        /// <summary>
        /// Loads the specified application configuration.
        /// </summary>
        /// <param name="name">The name of the application configuration to load.</param>
        /// <returns>The specified application configuration.</returns>
        ApplicationConfiguration LoadApplicationConfiguration(string name);

        /// <summary>
        /// Load the specified base configuration.
        /// </summary>
        /// <param name="name">The name of the base configuration to load.</param>
        /// <returns>The specified base configuration.</returns>
        BaseConfiguration LoadBaseConfiguration(string name);
    }
}
