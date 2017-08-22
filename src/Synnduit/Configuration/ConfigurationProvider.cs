using System.ComponentModel.Composition;

namespace Synnduit.Configuration
{
    /// <summary>
    /// Provides access to base, application, run, and run segment configuration objects.
    /// </summary>
    [Export(typeof(IConfigurationProvider))]
    internal class ConfigurationProvider : IConfigurationProvider
    {
        private readonly IWritableConfigurationProvider writableConfigurationProvider;

        [ImportingConstructor]
        public ConfigurationProvider(
            IWritableConfigurationProvider writableConfigurationProvider)
        {
            this.writableConfigurationProvider = writableConfigurationProvider;
        }

        /// <summary>
        /// Gets the current base configuration; a null reference will be returned if the
        /// current application configuration has no base configuration.
        /// </summary>
        public BaseConfiguration BaseConfiguration
        {
            get { return this.writableConfigurationProvider.BaseConfiguration; }
        }

        /// <summary>
        /// Gets the current application configuration.
        /// </summary>
        public ApplicationConfiguration ApplicationConfiguration
        {
            get { return this.writableConfigurationProvider.ApplicationConfiguration; }
        }

        /// <summary>
        /// Gets the current run configuration.
        /// </summary>
        public RunConfiguration RunConfiguration
        {
            get { return this.writableConfigurationProvider.RunConfiguration; }
        }

        /// <summary>
        /// Gets the current run segment configuration.
        /// </summary>
        public SegmentConfiguration SegmentConfiguration
        {
            get { return this.writableConfigurationProvider.SegmentConfiguration; }
        }
    }
}
