using System.Configuration;

namespace Synnduit.Configuration
{
    /// <summary>
    /// The configuration element that controls the application's garbage collection
    /// logging behavior.
    /// </summary>
    public class GarbageCollectionLoggingConfigurationElement : ConfigurationElement
    {
        /// <summary>
        /// The default value of the <see cref="Entity" /> property.
        /// </summary>
        public const bool DefaultEntityValue = true;

        /// <summary>
        /// The default value of the <see cref="AlwaysLogMessages" /> property.
        /// </summary>
        public const bool DefaultAlwaysLogMessagesValue = false;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public GarbageCollectionLoggingConfigurationElement()
        { }

        /// <summary>
        /// Gets or sets a comma-separated list of <see cref="EntityDeletionOutcome" />
        /// values identifying entity deletions that should be excluded from logging; the
        /// default is null (i.e., all entity deletions are logged by default).
        /// </summary>
        [ConfigurationProperty("excludedOutcomes")]
        public string ExcludedOutcomes
        {
            get { return (string) this["excludedOutcomes"]; }
            set { this["excludedOutcomes"] = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the destination system entity that is
        /// being deleted should be recorded in the log; the default is true.
        /// </summary>
        [ConfigurationProperty("entity", DefaultValue = DefaultEntityValue)]
        public bool Entity
        {
            get { return (bool) this["entity"]; }
            set { this["entity"] = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether an entity deletion should always be
        /// logged if there is at least one message associated with it, (possibly)
        /// effectively overriding the <see cref="ExcludedOutcomes" /> values; the default
        /// is false.
        /// </summary>
        [ConfigurationProperty(
            "alwaysLogMessages",
            DefaultValue = DefaultAlwaysLogMessagesValue)]
        public bool AlwaysLogMessages
        {
            get { return (bool) this["alwaysLogMessages"]; }
            set { this["alwaysLogMessages"] = value; }
        }
    }
}
