using System;
using System.Collections.Generic;
using System.Linq;
using Synnduit.Configuration;
using Synnduit.Events;
using Synnduit.Persistence;
using Synnduit.Properties;

namespace Synnduit
{
    /// <summary>
    /// Performs individual runs.
    /// </summary>
    internal class Runner : IRunner
    {
        private readonly IConfigurationLoader configurationLoader;

        private readonly IBootstrapperFactory bootstrapperFactory;

        private readonly IBridgeFactory bridgeFactory;

        private readonly string configurationName;

        private readonly string runName;

        private readonly IDictionary<string, object> runData;

        private Lazy<ConfigurationWrapper> configuration;

        private ContextFactory contextFactory;

        public Runner(
            IConfigurationLoader configurationLoader,
            IBootstrapperFactory bootstrapperFactory,
            IBridgeFactory bridgeFactory,
            string configurationName,
            string runName)
        {
            this.configurationLoader = configurationLoader;
            this.bootstrapperFactory = bootstrapperFactory;
            this.bridgeFactory = bridgeFactory;
            this.configurationName = configurationName;
            this.runName = runName;
            this.runData = new Dictionary<string, object>();
            this.configuration =
                new Lazy<ConfigurationWrapper>(this.LoadConfiguration);
            this.contextFactory = null;
        }

        /// <summary>
        /// Performs the run that the current instance is configured for.
        /// </summary>
        public void Run()
        {
            try
            {
                SegmentConfiguration[] segments =
                    this.configuration.Value.RunConfiguration.Segments;
                for(int i = 0; i < segments.Length; i++)
                {
                    this.RunSegment(i + 1, segments.Length, segments[i]);
                }
            }
            catch(Exception exception)
            {
                throw new SynnduitException(
                    Resources.RunExceptionMessage, exception);
            }
        }

        private void RunSegment(
            int segmentIndex,
            int segmentCount,
            SegmentConfiguration segmentConfiguration)
        {
            using(IBootstrapper bootstrapper =
                this.bootstrapperFactory.CreateBootstrapper())
            {
                using(ISafeRepository safeRepository =
                    bootstrapper.Get<ISafeRepository>())
                {
                    this.RunSegment(
                        segmentIndex,
                        segmentCount,
                        segmentConfiguration,
                        bootstrapper,
                        safeRepository);
                }
            }
        }

        private void RunSegment(
            int segmentIndex,
            int segmentCount,
            SegmentConfiguration segmentConfiguration,
            IBootstrapper bootstrapper,
            ISafeRepository safeRepository)
        {
            this.SetupConfigurationProvider(segmentConfiguration, bootstrapper);
            IContext context = this.SetupContext(
                segmentConfiguration,
                segmentIndex,
                segmentCount,
                bootstrapper,
                safeRepository);
            IBridge bridge = this.bridgeFactory.CreateBridge(
                segmentConfiguration.Type,
                context.EntityType.Type,
                bootstrapper);
            IInvocableInitializer invocableInitializer =
                bootstrapper.Get<IInvocableInitializer>();
            bridge.ContextValidator.Validate(context);
            ISegmentRunner segmentRunner = bridge.CreateSegmentRunner();
            bridge.EventDispatcher.SegmentExecuting(new SegmentExecutingArgs());
            invocableInitializer.Initialize(bridge.EventDispatcher);
            segmentRunner.Run();
            bridge.EventDispatcher.SegmentExecuted(new SegmentExecutedArgs());
        }

        private void SetupConfigurationProvider(
            SegmentConfiguration segmentConfiguration,
            IBootstrapper bootstrapper)
        {
            IWritableConfigurationProvider writableConfigurationProvider
                = bootstrapper.Get<IWritableConfigurationProvider>();
            writableConfigurationProvider
                .SetConfigurationProvider(new ConfigurationProvider(
                    this.configuration.Value, segmentConfiguration));
        }

        private IContext SetupContext(
            SegmentConfiguration segmentConfiguration,
            int segmentIndex,
            int segmentCount,
            IBootstrapper bootstrapper,
            ISafeRepository safeRepository)
        {
            IContext context =
                this
                .GetContextFactory(safeRepository)
                .CreateContext(
                    bootstrapper,
                    segmentConfiguration,
                    segmentIndex,
                    segmentCount,
                    this.runData);
            IWritableContext writableContext = bootstrapper.Get<IWritableContext>();
            writableContext.SetContext(context);
            return context;
        }

        private ContextFactory GetContextFactory(ISafeRepository safeRepository)
        {
            if(this.contextFactory == null)
            {
                this.contextFactory = new ContextFactory(
                    this,
                    safeRepository.GetExternalSystems(),
                    safeRepository.GetEntityTypes(),
                    safeRepository.GetSharedIdentifierSourceSystems());
            }
            return this.contextFactory;
        }

        private ConfigurationWrapper LoadConfiguration()
        {
            BaseConfiguration baseConfiguration = null;
            ApplicationConfiguration applicationConfiguration =
                this.configurationLoader.LoadApplicationConfiguration(
                    this.configurationName);
            if(applicationConfiguration.BaseConfiguration != null)
            {
                baseConfiguration =
                    this
                    .configurationLoader
                    .LoadBaseConfiguration(
                        applicationConfiguration.BaseConfiguration);
            }
            return new ConfigurationWrapper(
                baseConfiguration,
                applicationConfiguration,
                this.GetRunConfiguration(applicationConfiguration));
        }

        private RunConfiguration GetRunConfiguration(
            ApplicationConfiguration applicationConfiguration)
        {
            return this.Single(
                applicationConfiguration.Runs,
                this.runName,
                run => run.Name,
                Resources.RunNotFound);
        }

        private T Single<T>(
            IEnumerable<T> collection,
            string name,
            Func<T, string> getName,
            string exceptionMessageFormat)
        {
            try
            {
                return
                    collection
                    .Single(t => getName(t) == name);
            }
            catch(Exception exception)
            {
                throw new InvalidOperationException(
                    string.Format(exceptionMessageFormat, name),
                    exception);
            }
        }

        private class SegmentExecutingArgs : ISegmentExecutingArgs
        {
        }

        private class SegmentExecutedArgs : ISegmentExecutedArgs
        {
        }

        private class ConfigurationWrapper
        {
            public ConfigurationWrapper(
                BaseConfiguration baseConfiguration,
                ApplicationConfiguration applicationConfiguration,
                RunConfiguration runConfiguration)
            {
                this.BaseConfiguration = baseConfiguration;
                this.ApplicationConfiguration = applicationConfiguration;
                this.RunConfiguration = runConfiguration;
            }

            public BaseConfiguration BaseConfiguration { get; }

            public ApplicationConfiguration ApplicationConfiguration { get; }

            public RunConfiguration RunConfiguration { get; }
        }

        private class ConfigurationProvider : IConfigurationProvider
        {
            private readonly ConfigurationWrapper configuration;

            private readonly SegmentConfiguration segmentConfiguration;

            public ConfigurationProvider(
                ConfigurationWrapper configuration,
                SegmentConfiguration segmentConfiguration)
            {
                this.configuration = configuration;
                this.segmentConfiguration = segmentConfiguration;
            }

            public BaseConfiguration BaseConfiguration
            {
                get { return this.configuration.BaseConfiguration; }
            }

            public ApplicationConfiguration ApplicationConfiguration
            {
                get { return this.configuration.ApplicationConfiguration; }
            }

            public RunConfiguration RunConfiguration
            {
                get { return this.configuration.RunConfiguration; }
            }

            public SegmentConfiguration SegmentConfiguration
            {
                get { return this.segmentConfiguration; }
            }
        }

        private class ContextFactory
        {
            private readonly Runner parent;

            private readonly IEnumerable<Persistence.IExternalSystem> externalSystems;

            private readonly IEnumerable<Persistence.IEntityType> entityTypes;

            private readonly IEnumerable<
                ISharedIdentifierSourceSystem> sharedIdentifierSourceSystems;

            public ContextFactory(
                Runner parent,
                IEnumerable<Persistence.IExternalSystem> externalSystems,
                IEnumerable<Persistence.IEntityType> entityTypes,
                IEnumerable<ISharedIdentifierSourceSystem> sharedIdentifierSourceSystems)
            {
                this.parent = parent;
                this.externalSystems = externalSystems;
                this.entityTypes = entityTypes;
                this.sharedIdentifierSourceSystems = sharedIdentifierSourceSystems;
            }

            public IContext CreateContext(
                IBootstrapper bootstrapper,
                SegmentConfiguration segmentConfiguration,
                int segmentIndex,
                int segmentCount,
                IDictionary<string, object> runData)
            {
                IContext context;
                IEnumerable<IExternalSystem>
                    externalSystems = this.CreateExternalSystems();
                IExternalSystem sourceSystem =
                    this.GetSourceSystem(segmentConfiguration, externalSystems);
                IEnumerable<IEntityType> entityTypes =
                    this.CreateEntityTypes(sourceSystem, externalSystems);
                IEntityType entityType =
                    this.GetEntityType(segmentConfiguration.EntityType, entityTypes);
                IReadOnlyDictionary<string, string> parameters =
                    this.AssembleParameters(bootstrapper, entityType, sourceSystem);
                context = new Context(
                    segmentConfiguration.Type,
                    segmentIndex,
                    segmentCount,
                    sourceSystem,
                    entityType,
                    externalSystems,
                    entityTypes,
                    parameters,
                    runData);
                return context;
            }

            private IEnumerable<IExternalSystem> CreateExternalSystems()
            {
                return
                    this
                    .externalSystems
                    .Select(externalSystem => new ExternalSystem(externalSystem))
                    .ToArray();
            }

            private IExternalSystem GetSourceSystem(
                SegmentConfiguration segmentConfiguration,
                IEnumerable<IExternalSystem> externalSystems)
            {
                IExternalSystem sourceSystem = null;
                string sourceSystemName = this.GetSourceSystemName(segmentConfiguration);
                if(sourceSystemName != null)
                {
                    sourceSystem = this.parent.Single(
                        externalSystems,
                        this.GetSourceSystemName(segmentConfiguration),
                        externalSystem => externalSystem.Name,
                        Resources.SourceSystemNotFound);
                }
                return sourceSystem;
            }

            private string GetSourceSystemName(SegmentConfiguration segmentConfiguration)
            {
                return this.GetInheritedOverridenValue(
                    segmentConfiguration.SourceSystem,
                    this.parent.configuration.Value.RunConfiguration.SourceSystem,
                    this.parent.configuration.Value.ApplicationConfiguration.SourceSystem);
            }

            private IEnumerable<IEntityType> CreateEntityTypes(
                IExternalSystem sourceSystem,
                IEnumerable<IExternalSystem> externalSystems)
            {
                IEnumerable<IEntityType> entityTypes;
                IDictionary<Guid, IExternalSystem> externalSystemsById =
                    externalSystems
                    .ToDictionary(externalSystem => externalSystem.Id);
                if(sourceSystem != null)
                {
                    IDictionary<Guid, IEnumerable<Guid>> sharedIdentifierSourceSystemIds =
                        this
                        .sharedIdentifierSourceSystems
                        .Where(siss => siss.SourceSystemId == sourceSystem.Id)
                        .GroupBy(siss => siss.EntityTypeId)
                        .ToDictionary(
                            group => group.Key,
                            group => group.Select(
                                siss => siss.SharedIdentifierSourceSystemId));
                    entityTypes =
                        this
                        .entityTypes
                        .Select(entityType => new EntityType(
                            entityType,
                            this.GetExternalSystem(
                                entityType.DestinationSystemId, externalSystemsById),
                            this.GetSharedIdentifierSourceSystems(
                                entityType.Id,
                                sharedIdentifierSourceSystemIds,
                                externalSystemsById)))
                        .ToArray();
                }
                else
                {
                    entityTypes =
                        this
                        .entityTypes
                        .Select(entityType => new EntityType(
                            entityType,
                            this.GetExternalSystem(
                                entityType.DestinationSystemId, externalSystemsById),
                            null))
                        .ToArray();
                }
                return entityTypes;
            }

            private IEnumerable<IExternalSystem> GetSharedIdentifierSourceSystems(
                Guid entityTypeId,
                IDictionary<Guid, IEnumerable<Guid>> sharedIdentifierSourceSystemIds,
                IDictionary<Guid, IExternalSystem> externalSystemsById)
            {
                var sharedIdentifierSourceSystems = new IExternalSystem[0];
                if(sharedIdentifierSourceSystemIds
                    .TryGetValue(entityTypeId, out IEnumerable<Guid> sourceSystemIds))
                {
                    sharedIdentifierSourceSystems =
                        sourceSystemIds
                        .Select(id => this.GetExternalSystem(id, externalSystemsById))
                        .ToArray();
                }
                return sharedIdentifierSourceSystems;
            }

            private IExternalSystem GetExternalSystem(
                Guid id, IDictionary<Guid, IExternalSystem> externalSystemsById)
            {
                if(externalSystemsById.TryGetValue(
                    id, out IExternalSystem externalSystem) == false)
                {
                    throw new InvalidOperationException(
                        string.Format(Resources.ExternalSystemIdNotFound, id));
                }
                return externalSystem;
            }

            private IEntityType GetEntityType(
                string entityTypeName, IEnumerable<IEntityType> entityTypes)
            {
                return this.parent.Single(
                    entityTypes,
                    entityTypeName,
                    entityType => entityType.Name,
                    Resources.EntityTypeNotFound);
            }

            private IReadOnlyDictionary<string, string> AssembleParameters(
                IBootstrapper bootstrapper,
                IEntityType entityType,
                IExternalSystem sourceSystem)
            {
                IParametersAssembler parametersAssembler
                    = bootstrapper.Get<IParametersAssembler>();
                return parametersAssembler.AssembleParameters(
                    entityType.DestinationSystem.Id,
                    entityType.Id,
                    sourceSystem != null ? (Guid?) sourceSystem.Id : null);
            }

            private string GetInheritedOverridenValue(params string[] values)
            {
                return values.FirstOrDefault(value => value != null);
            }
        }

        private class Context : IContext
        {
            private readonly SegmentType segmentType;

            private readonly int segmentIndex;

            private readonly int segmentCount;

            private readonly IExternalSystem sourceSystem;

            private readonly IEntityType entityType;

            private readonly IEnumerable<IExternalSystem> externalSystems;

            private readonly IEnumerable<IEntityType> entityTypes;

            private readonly IReadOnlyDictionary<string, string> parameters;

            private readonly IDictionary<string, object> runData;

            public Context(
                SegmentType segmentType,
                int segmentIndex,
                int segmentCount,
                IExternalSystem sourceSystem,
                IEntityType entityType,
                IEnumerable<IExternalSystem> externalSystems,
                IEnumerable<IEntityType> entityTypes,
                IReadOnlyDictionary<string, string> parameters,
                IDictionary<string, object> runData)
            {
                this.segmentType = segmentType;
                this.segmentIndex = segmentIndex;
                this.segmentCount = segmentCount;
                this.sourceSystem = sourceSystem;
                this.entityType = entityType;
                this.externalSystems = externalSystems;
                this.entityTypes = entityTypes;
                this.parameters = parameters;
                this.runData = runData;
            }

            public SegmentType SegmentType
            {
                get { return this.segmentType; }
            }

            public int SegmentIndex
            {
                get { return this.segmentIndex; }
            }

            public int SegmentCount
            {
                get { return this.segmentCount; }
            }

            public IExternalSystem SourceSystem
            {
                get { return this.sourceSystem; }
            }

            public IExternalSystem DestinationSystem
            {
                get { return this.entityType.DestinationSystem; }
            }

            public IEntityType EntityType
            {
                get { return this.entityType; }
            }

            public IEnumerable<IExternalSystem> ExternalSystems
            {
                get { return this.externalSystems; }
            }

            public IEnumerable<IEntityType> EntityTypes
            {
                get { return this.entityTypes; }
            }

            public IReadOnlyDictionary<string, string> Parameters
            {
                get { return this.parameters; }
            }

            public IDictionary<string, object> RunData
            {
                get { return this.runData; }
            }
        }

        private class ExternalSystem : IExternalSystem
        {
            private readonly Persistence.IExternalSystem externalSystem;

            public ExternalSystem(Persistence.IExternalSystem externalSystem)
            {
                this.externalSystem = externalSystem;
            }

            public Guid Id
            {
                get { return this.externalSystem.Id; }
            }

            public string Name
            {
                get { return this.externalSystem.Name; }
            }
        }

        private class EntityType : IEntityType
        {
            private static readonly IDictionary<Guid, Type> cachedEntityTypes;

            static EntityType()
            {
                cachedEntityTypes = new Dictionary<Guid, Type>();
            }

            private readonly Persistence.IEntityType entityType;

            private readonly IExternalSystem destinationSystem;

            private readonly Type type;

            private readonly IEnumerable<IExternalSystem> sharedIdentifierSourceSystems;

            public EntityType(
                Persistence.IEntityType entityType,
                IExternalSystem destinationSystem,
                IEnumerable<IExternalSystem> sharedIdentifierSourceSystems)
            {
                this.entityType = entityType;
                this.destinationSystem = destinationSystem;
                this.type = this.GetEntityType(entityType);
                this.sharedIdentifierSourceSystems = sharedIdentifierSourceSystems;
            }

            public Guid Id
            {
                get { return this.entityType.Id; }
            }

            public string Name
            {
                get { return this.entityType.Name; }
            }

            public IExternalSystem DestinationSystem
            {
                get { return this.destinationSystem; }
            }

            public Type Type
            {
                get { return this.type; }
            }

            public bool IsMutable
            {
                get { return this.entityType.IsMutable; }
            }

            public bool IsDuplicable
            {
                get { return this.entityType.IsDuplicable; }
            }

            public IEnumerable<IExternalSystem> SharedIdentifierSourceSystems
            {
                get { return this.sharedIdentifierSourceSystems; }
            }

            private Type GetEntityType(Persistence.IEntityType entityType)
            {
                if(cachedEntityTypes.TryGetValue(entityType.Id, out Type type) == false)
                {
                    type = this.ExtractEntityType(entityType);
                    cachedEntityTypes.Add(entityType.Id, type);
                }
                return type;
            }

            private Type ExtractEntityType(Persistence.IEntityType entityType)
            {
                try
                {
                    return
                        AppDomain
                        .CurrentDomain
                        .GetAssemblies()
                        .Where(assembly => assembly.IsDynamic == false)
                        .SelectMany(assembly => assembly.ExportedTypes)
                        .Single(type => type.AssemblyQualifiedName == entityType.TypeName);
                }
                catch
                {
                    throw new InvalidOperationException(string.Format(
                        Resources.InvalidEntityType,
                        entityType.Name,
                        entityType.TypeName));
                }
            }
        }
    }
}
