namespace Synnduit.Configuration
{
    /// <summary>
    /// Provides read-write access to base, application, run, and run segment configuration
    /// objects.
    /// </summary>
    internal interface IWritableConfigurationProvider : IConfigurationProvider
    {
        /// <summary>
        /// Sets the underlying <see cref="IConfigurationProvider" /> implementation
        /// instance; the configuration objects will be retrieved from this instance.
        /// </summary>
        /// <param name="configurationProvider">
        /// The underlying <see cref="IConfigurationProvider" /> implementation instance.
        /// </param>
        void SetConfigurationProvider(
            IConfigurationProvider configurationProvider);
    }
}
