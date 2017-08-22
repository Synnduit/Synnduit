using System.Collections.Generic;

namespace Synnduit.Configuration
{
    /// <summary>
    /// The base class for classes representing the run/segment configuration.
    /// </summary>
    internal abstract class ConfigurationBase
    {
        private readonly IDictionary<string, string> parameters;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        protected ConfigurationBase()
        {
            this.parameters = new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets or sets the metadata provider that should be used.
        /// </summary>
        public string MetadataProvider { get; set; }

        /// <summary>
        /// Gets or sets the entity serializer that should be used.
        /// </summary>
        public string Serializer { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the inherited collection of
        /// preprocessor operations applied to source system entities should be cleared.
        /// </summary>
        public bool ClearSourceSystemPreprocessorOperations { get; set; }

        /// <summary>
        /// Gets or sets the collection of preprocessor operations to be excluded from the
        /// inherited collection of operations applied to source system entities.
        /// </summary>
        public string[] ExcludeSourceSystemPreprocessorOperations { get; set; }

        /// <summary>
        /// Gets or sets the collection of preprocessor operations applied to source system
        /// entities; will be added to the inherited collection.
        /// </summary>
        public string[] SourceSystemPreprocessorOperations { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the inherited collection of
        /// preprocessor operations applied to destination system entities should be
        /// cleared.
        /// </summary>
        public bool ClearDestinationSystemPreprocessorOperations { get; set; }

        /// <summary>
        /// Gets or sets the collection of preprocessor operations to be excluded from the
        /// inherited collection of operations applied to destination system entities.
        /// </summary>
        public string[] ExcludeDestinationSystemPreprocessorOperations { get; set; }

        /// <summary>
        /// Gets or sets the collection of preprocessor operations applied to destination
        /// system entities; will be added to the inherited collection.
        /// </summary>
        public string[] DestinationSystemPreprocessorOperations { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the inherited collection of
        /// homogenizers (aplied by the deduplication cache indexers) should be cleared.
        /// </summary>
        public bool ClearHomogenizers { get; set; }

        /// <summary>
        /// Gets or sets the collection of homogenizers (aplied by the deduplication cache
        /// indexers) to be excluded from the inherited collection of homogenizers.
        /// </summary>
        public string[] ExcludeHomogenizers { get; set; }

        /// <summary>
        /// Gets or sets the collection of homogenizers to be applied by the
        /// (deduplication) cache indexers; will be added to the inherited collection.
        /// </summary>
        public string[] Homogenizers { get; set; }

        /// <summary>
        /// Gets or sets the name of the source system.
        /// </summary>
        public string SourceSystem { get; set; }

        /// <summary>
        /// Gets the dictionary of parameters (i.e., all additional values not represented
        /// by class properties).
        /// </summary>
        public IDictionary<string, string> Parameters
        {
            get { return this.parameters; }
        }
    }
}
