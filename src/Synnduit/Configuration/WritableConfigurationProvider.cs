using System.ComponentModel.Composition;

namespace Synnduit.Configuration
{
    /// <summary>
    /// Provides read-write access to base, application, run, and run segment configuration
    /// objects.
    /// </summary>
    [Export(typeof(IWritableConfigurationProvider))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    internal class WritableConfigurationProvider : IWritableConfigurationProvider
    {
        private IConfigurationProvider configurationProvider;

        /// <summary>
        /// Gets the current base configuration; a null reference will be returned if the
        /// current application configuration has no base configuration.
        /// </summary>
        public BaseConfiguration BaseConfiguration
        {
            get { return this.configurationProvider.BaseConfiguration; }
        }

        /// <summary>
        /// Gets the current application configuration.
        /// </summary>
        public ApplicationConfiguration ApplicationConfiguration
        {
            get { return this.configurationProvider.ApplicationConfiguration; }
        }

        /// <summary>
        /// Gets the current run configuration.
        /// </summary>
        public RunConfiguration RunConfiguration
        {
            get { return this.configurationProvider.RunConfiguration; }
        }

        /// <summary>
        /// Gets the current run segment configuration.
        /// </summary>
        public SegmentConfiguration SegmentConfiguration
        {
            get { return this.configurationProvider.SegmentConfiguration; }
        }

        /// <summary>
        /// Sets the underlying <see cref="IConfigurationProvider" /> implementation
        /// instance; the configuration objects will be retrieved from this instance.
        /// </summary>
        /// <param name="configurationProvider">
        /// The underlying <see cref="IConfigurationProvider" /> implementation instance.
        /// </param>
        public void SetConfigurationProvider(
            IConfigurationProvider configurationProvider)
        {
            this.configurationProvider = configurationProvider;
        }
    }
}
