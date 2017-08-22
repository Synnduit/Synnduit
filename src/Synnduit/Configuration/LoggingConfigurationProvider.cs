using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Synnduit.Properties;

namespace Synnduit.Configuration
{
    /// <summary>
    /// Provides access to the configuration that controls the application's logging
    /// behavior.
    /// </summary>
    [Export(typeof(ILoggingConfigurationProvider))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    internal class LoggingConfigurationProvider : ILoggingConfigurationProvider
    {
        private readonly MigrationLoggingConfiguration migrationLoggingConfiguration;

        private readonly
            GarbageCollectionLoggingConfiguration garbageCollectionLoggingConfiguration;

        [ImportingConstructor]
        public LoggingConfigurationProvider()
            : this(ConfigurationSectionProvider.Instance)
        { }

        public LoggingConfigurationProvider(
            IConfigurationSectionProvider configurationSectionProvider)
        {
            SynnduitConfigurationSection configuration =
                configurationSectionProvider.GetConfiguration();
            this.migrationLoggingConfiguration =
                this.GetMigrationLoggingConfiguration(configuration);
            this.garbageCollectionLoggingConfiguration =
                this.GetGarbageCollectionLoggingConfiguration(configuration);
        }

        /// <summary>
        /// Gets the configuration that controls the application's migration logging
        /// behavior.
        /// </summary>
        public IMigrationLoggingConfiguration MigrationConfiguration
        {
            get { return this.migrationLoggingConfiguration; }
        }

        /// <summary>
        /// Gets the configuration that controls the application's garbage collection
        /// logging behavior.
        /// </summary>
        public IGarbageCollectionLoggingConfiguration GarbageCollectionConfiguration
        {
            get { return this.garbageCollectionLoggingConfiguration; }
        }

        private MigrationLoggingConfiguration
            GetMigrationLoggingConfiguration(SynnduitConfigurationSection configuration)
        {
            return new MigrationLoggingConfiguration(
                this.ParseEnumValues<EntityTransactionOutcome>(
                    configuration.Logging?.Migration?.ExcludedOutcomes,
                    MigrationLoggingConfigurationElement.DefaultExcludedOutcomesValue),
                configuration.Logging?.Migration?.SourceSystemEntity,
                configuration.Logging?.Migration?.DestinationSystemEntity,
                configuration.Logging?.Migration?.ValueChanges,
                configuration.Logging?.Migration?.AlwaysLogMessages);
        }

        private GarbageCollectionLoggingConfiguration
            GetGarbageCollectionLoggingConfiguration(
                SynnduitConfigurationSection configuration)
        {
            return new GarbageCollectionLoggingConfiguration(
                this.ParseEnumValues<EntityDeletionOutcome>(
                    configuration.Logging?.GarbageCollection.ExcludedOutcomes),
                configuration.Logging?.GarbageCollection?.Entity,
                configuration.Logging?.GarbageCollection?.AlwaysLogMessages);
        }

        private IEnumerable<TEnum> ParseEnumValues<TEnum>(
            string value, string defaultValue = null)
        {
            IEnumerable<TEnum> enumValues = new TEnum[] { };
            string valueToParse = value ?? defaultValue;
            if(!string.IsNullOrWhiteSpace(valueToParse))
            {
                enumValues =
                    value
                    .Split(',')
                    .Select(this.ParseEnumValue<TEnum>)
                    .ToArray();
            }
            return enumValues;
        }

        private TEnum ParseEnumValue<TEnum>(string value)
        {
            try
            {
                return (TEnum) Enum.Parse(typeof(TEnum), value.Trim());
            }
            catch(Exception exception)
            {
                throw new InvalidOperationException(
                    string.Format(Resources.InvalidEnumValue, value, typeof(TEnum).Name),
                    exception);
            }
        }

        private class MigrationLoggingConfiguration : IMigrationLoggingConfiguration
        {
            public MigrationLoggingConfiguration(
                IEnumerable<EntityTransactionOutcome> excludedOutcomes,
                bool? sourceSystemEntity,
                bool? destinationSystemEntity,
                bool? valueChanges,
                bool? alwaysLogMessages)
            {
                this.ExcludedOutcomes = excludedOutcomes;
                this.SourceSystemEntity = sourceSystemEntity.GetValueOrDefault(
                    MigrationLoggingConfigurationElement.DefaultSourceSystemEntityValue);
                this.DestinationSystemEntity = destinationSystemEntity.GetValueOrDefault(
                    MigrationLoggingConfigurationElement
                    .DefaultDestinationSystemEntityValue);
                this.ValueChanges = valueChanges.GetValueOrDefault(
                    MigrationLoggingConfigurationElement.DefaultValueChangesValue);
                this.AlwaysLogMessages = alwaysLogMessages.GetValueOrDefault(
                    MigrationLoggingConfigurationElement.DefaultAlwaysLogMessagesValue);
            }

            public IEnumerable<EntityTransactionOutcome> ExcludedOutcomes
            { get; private set; }

            public bool SourceSystemEntity { get; private set; }

            public bool DestinationSystemEntity { get; private set; }

            public bool ValueChanges { get; private set; }

            public bool AlwaysLogMessages { get; private set; }
        }

        private class GarbageCollectionLoggingConfiguration :
            IGarbageCollectionLoggingConfiguration
        {
            public GarbageCollectionLoggingConfiguration(
                IEnumerable<EntityDeletionOutcome> excludedOutcomes,
                bool? entity,
                bool? alwaysLogMessages)
            {
                this.ExcludedOutcomes = excludedOutcomes;
                this.Entity = entity.GetValueOrDefault(
                    GarbageCollectionLoggingConfigurationElement.DefaultEntityValue);
                this.AlwaysLogMessages = alwaysLogMessages.GetValueOrDefault(
                    GarbageCollectionLoggingConfigurationElement
                    .DefaultAlwaysLogMessagesValue);
            }

            public IEnumerable<EntityDeletionOutcome> ExcludedOutcomes
            { get; private set;}

            public bool Entity { get; private set; }

            public bool AlwaysLogMessages { get; private set; }
        }
    }
}
