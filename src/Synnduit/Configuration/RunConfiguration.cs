using Newtonsoft.Json;

namespace Synnduit.Configuration
{
    /// <summary>
    /// Represents the configuration of a single run.
    /// </summary>
    internal class RunConfiguration : ConfigurationBase
    {
        /// <summary>
        /// Gets or sets the name of the run.
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the ordered array of segments that comprise the run.
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public SegmentConfiguration[] Segments { get; set; }
    }
}
