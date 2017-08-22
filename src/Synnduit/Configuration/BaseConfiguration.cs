namespace Synnduit.Configuration
{
    /// <summary>
    /// Represents the main configuration of the application.
    /// </summary>
    internal class BaseConfiguration
    {
        /// <summary>
        /// Gets or sets the collection of namespaces the configuration uses.
        /// </summary>
        public string[] Usings { get; set; }

        /// <summary>
        /// Gets or sets the metadata provider that should be used.
        /// </summary>
        public string MetadataProvider { get; set; }

        /// <summary>
        /// Gets or sets the entity serializer that should be used.
        /// </summary>
        public string Serializer { get; set; }

        /// <summary>
        /// Gets or sets the collection of preprocessor operations applied to source system
        /// entities.
        /// </summary>
        public string[] SourceSystemPreprocessorOperations { get; set; }

        /// <summary>
        /// Gets or sets the collection of preprocessor operations applied to destination
        /// system entities.
        /// </summary>
        public string[] DestinationSystemPreprocessorOperations { get; set; }

        /// <summary>
        /// Gets or sets the collection of homogenizers to be applied by the
        /// (deduplication) cache indexers.
        /// </summary>
        public string[] Homogenizers { get; set; }

        /// <summary>
        /// Gets or sets the entity merger that should be used.
        /// </summary>
        public string Merger { get; set; }
    }
}
