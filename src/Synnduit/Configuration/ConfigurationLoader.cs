using System;
using System.IO;
using Newtonsoft.Json;

namespace Synnduit.Configuration
{
    /// <summary>
    /// Loads individual application and base configuration files.
    /// </summary>
    internal class ConfigurationLoader : IConfigurationLoader
    {
        private static Lazy<ConfigurationLoader> instance =
            new Lazy<ConfigurationLoader>(() => new ConfigurationLoader());

        /// <summary>
        /// Gets the singleton instance of the class.
        /// </summary>
        public static ConfigurationLoader Instance
        {
            get { return instance.Value; }
        }

        private readonly IConfigurationSectionProvider configurationSectionProvider;

        private ConfigurationLoader()
            : this(ConfigurationSectionProvider.Instance)
        { }

        public ConfigurationLoader(
            IConfigurationSectionProvider configurationSectionProvider)
        {
            this.configurationSectionProvider = configurationSectionProvider;
        }

        /// <summary>
        /// Loads the specified application configuration.
        /// </summary>
        /// <param name="name">The name of the application configuration to load.</param>
        /// <returns>The specified application configuration.</returns>
        public ApplicationConfiguration LoadApplicationConfiguration(string name)
        {
            return this.Load<ApplicationConfiguration>(name);
        }

        /// <summary>
        /// Load the specified base configuration.
        /// </summary>
        /// <param name="name">The name of the base configuration to load.</param>
        /// <returns>The specified base configuration.</returns>
        public BaseConfiguration LoadBaseConfiguration(string name)
        {
            return this.Load<BaseConfiguration>(name);
        }

        private T Load<T>(string name)
        {
            T value;
            var serializer = new JsonSerializer();
            using(var reader = new StreamReader(this.GetFilePath(name)))
            {
                value = (T) serializer.Deserialize(reader, typeof(T));
            }
            return value;
        }

        private string GetFilePath(string name)
        {
            return Path.Combine(
                this
                .configurationSectionProvider
                .GetConfiguration()
                .RunConfigurationFilesDirectoryPath,
                name);
        }
    }
}
