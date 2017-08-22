using System;
using System.Configuration;
using Synnduit.Properties;

namespace Synnduit.Configuration
{
    /// <summary>
    /// Provides access to the application's config file configuration section.
    /// </summary>
    internal class ConfigurationSectionProvider : IConfigurationSectionProvider
    {
        private static Lazy<ConfigurationSectionProvider> instance =
            new Lazy<ConfigurationSectionProvider>(
                () => new ConfigurationSectionProvider());

        /// <summary>
        /// Gets the singleton instance of the class.
        /// </summary>
        public static ConfigurationSectionProvider Instance
        {
            get { return instance.Value; }
        }

        private readonly Lazy<SynnduitConfigurationSection> configuration;

        private ConfigurationSectionProvider()
        {
            this.configuration =
                new Lazy<SynnduitConfigurationSection>(this.LoadConfiguration);
        }

        /// <summary>
        /// Gets the application's config file configuration section.
        /// </summary>
        /// <returns>The application's config file configuration section.</returns>
        public SynnduitConfigurationSection GetConfiguration()
        {
            return configuration.Value;
        }

        private SynnduitConfigurationSection LoadConfiguration()
        {
            var configuration =
                ConfigurationManager.GetSection("synnduit")
                as SynnduitConfigurationSection;
            if(configuration == null)
            {
                throw new InvalidOperationException(
                    Resources.ConfigurationSectionNotFound);
            }
            return configuration;
        }
    }
}
