using Newtonsoft.Json;

namespace Synnduit.Configuration
{
    /// <summary>
    /// Represents the main configuration of the application.
    /// </summary>
    internal class ApplicationConfiguration : ConfigurationBase
    {
        /// <summary>
        /// Gets or sets the name of the base configuration.
        /// </summary>
        public string BaseConfiguration { get; set; }

        /// <summary>
        /// Gets or sets the collection of namespaces the configuration uses.
        /// </summary>
        public string[] Usings { get; set; }

        /// <summary>
        /// Gets or sets the array of supported runs.
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public RunConfiguration[] Runs { get; set; }
    }
}
