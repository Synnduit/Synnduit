namespace Synnduit.Configuration
{
    /// <summary>
    /// Provides access to the application's config file configuration section.
    /// </summary>
    internal interface IConfigurationSectionProvider
    {
        /// <summary>
        /// Gets the application's config file configuration section.
        /// </summary>
        /// <returns>The application's config file configuration section.</returns>
        SynnduitConfigurationSection GetConfiguration();
    }
}
