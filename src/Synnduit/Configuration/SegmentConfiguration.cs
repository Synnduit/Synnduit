using Newtonsoft.Json;

namespace Synnduit.Configuration
{
    /// <summary>
    /// Represents the configuration of a single run segment.
    /// </summary>
    internal class SegmentConfiguration : ConfigurationBase
    {
        /// <summary>
        /// Gets or sets the segment type.
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public SegmentType Type { get; set; }

        /// <summary>
        /// Gets or sets the entity type.
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string EntityType { get; set; }
    }
}
