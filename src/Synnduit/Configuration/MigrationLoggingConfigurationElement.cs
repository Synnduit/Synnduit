using System.Configuration;

namespace Synnduit.Configuration
{
    /// <summary>
    /// The configuration element that controls the application's migration logging
    /// behavior.
    /// </summary>
    public class MigrationLoggingConfigurationElement : ConfigurationElement
    {
        /// <summary>
        /// The default value of the <see cref="ExcludedOutcomes" /> property.
        /// </summary>
        public const string DefaultExcludedOutcomesValue = "Skipped, NoChangesDetected";

        /// <summary>
        /// The default value of the <see cref="SourceSystemEntity" /> property.
        /// </summary>
        public const bool DefaultSourceSystemEntityValue = true;

        /// <summary>
        /// The default value of the <see cref="DestinationSystemEntity" /> property.
        /// </summary>
        public const bool DefaultDestinationSystemEntityValue = false;

        /// <summary>
        /// The default value of the <see cref="ValueChanges" /> property.
        /// </summary>
        public const bool DefaultValueChangesValue = true;

        /// <summary>
        /// The default value of the <see cref="AlwaysLogMessages" /> property.
        /// </summary>
        public const bool DefaultAlwaysLogMessagesValue = false;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public MigrationLoggingConfigurationElement()
        { }

        /// <summary>
        /// Gets or sets a comma-separated list of
        /// <see cref="Logging.EntityTransactionOutcome"/> values identifying entity
        /// transactions that should be excluded from logging; the default is
        /// <see cref="Logging.EntityTransactionOutcome.Skipped" /> and
        /// <see cref="Logging.EntityTransactionOutcome.NoChangesDetected"/>.
        /// </summary>
        [ConfigurationProperty(
            "excludedOutcomes",
            DefaultValue = DefaultExcludedOutcomesValue)]
        public string ExcludedOutcomes
        {
            get { return (string) this["excludedOutcomes"]; }
            set { this["excludedOutcomes"] = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the source system entity should be
        /// recorded in the log; the default is true.
        /// </summary>
        [ConfigurationProperty(
            "sourceSystemEntity",
            DefaultValue = DefaultSourceSystemEntityValue)]
        public bool SourceSystemEntity
        {
            get { return (bool) this["sourceSystemEntity"]; }
            set { this["sourceSystemEntity"] = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the destination system entity should be
        /// recorded in the log; the default is false.
        /// </summary>
        [ConfigurationProperty(
            "destinationSystemEntity",
            DefaultValue = DefaultDestinationSystemEntityValue)]
        public bool DestinationSystemEntity
        {
            get { return (bool) this["destinationSystemEntity"]; }
            set { this["destinationSystemEntity"] = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether individual value changes should be
        /// recorded in the log; the default is true.
        /// </summary>
        [ConfigurationProperty("valueChanges", DefaultValue = DefaultValueChangesValue)]
        public bool ValueChanges
        {
            get { return (bool) this["valueChanges"]; }
            set { this["valueChanges"] = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether an entity transaction should always be
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
