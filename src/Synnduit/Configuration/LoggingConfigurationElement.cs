using System.Configuration;

namespace Synnduit.Configuration
{
    /// <summary>
    /// The configuration element that controls the application's logging behavior.
    /// </summary>
    public class LoggingConfigurationElement : ConfigurationElement
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public LoggingConfigurationElement()
        { }

        /// <summary>
        /// Gets or sets the configuration element that controls the application's
        /// migration logging behavior.
        /// </summary>
        [ConfigurationProperty("migration")]
        public MigrationLoggingConfigurationElement Migration
        {
            get { return (MigrationLoggingConfigurationElement) this["migration"]; }
            set { this["migration"] = value; }
        }

        /// <summary>
        /// Gets or sets the configuration element that controls the application's garbage
        /// collection logging behavior.
        /// </summary>
        [ConfigurationProperty("garbageCollection")]
        public GarbageCollectionLoggingConfigurationElement GarbageCollection
        {
            get
            {
                return
                    (GarbageCollectionLoggingConfigurationElement)
                    this["garbageCollection"];
            }
            set { this["garbageCollection"] = value; }
        }
    }
}
