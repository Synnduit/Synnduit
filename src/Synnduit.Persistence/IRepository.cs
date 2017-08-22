using System;
using System.Collections.Generic;

namespace Synnduit.Persistence
{
    /// <summary>
    /// Exposes methods that allow the application access to the underlying persistence
    /// mechanism.
    /// </summary>
    public interface IRepository : IDisposable
    {
        /// <summary>
        /// Clears all shared source system identifier (group) records for the specified
        /// entity type.
        /// </summary>
        /// <param name="entityTypeId">The ID of the entity type.</param>
        void ClearSharedSourceSystemIdentifiers(Guid entityTypeId);

        /// <summary>
        /// Creates a new entity deletion.
        /// </summary>
        /// <param name="transaction">The transaction to create.</param>
        void CreateEntityDeletion(IEntityDeletion transaction);

        /// <summary>
        /// Creates a new identity entity transaction.
        /// </summary>
        /// <param name="transaction">The transaction to create.</param>
        void CreateIdentityEntityTransaction(IIdentityEntityTransaction transaction);

        /// <summary>
        /// Creates a new message associated with the specified source system entity
        /// identity (always) and operation (if it exists).
        /// </summary>
        /// <param name="message">The message to create.</param>
        void CreateIdentityOperationMessage(IIdentityOperationMessage message);

        /// <summary>
        /// Creates a new serialized source system entity associated with the specified
        /// source system entity identity (always) and operation (if it exists).
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        void CreateIdentityOperationSourceSystemEntity(
            IIdentityOperationSourceSystemEntity entity);

        /// <summary>
        /// Creates a new entity source/destination system identifier mapping.
        /// </summary>
        /// <param name="mapping">The mapping to create.</param>
        void CreateMapping(IMapping mapping);

        /// <summary>
        /// Creates a new mapping entity transaction.
        /// </summary>
        /// <param name="transaction">The transaction to create.</param>
        void CreateMappingEntityTransaction(IMappingEntityTransaction transaction);

        /// <summary>
        /// Creates a new serialized destination system entity associated with an
        /// operation.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        void CreateOperationDestinationSystemEntity(IOperationSerializedEntity entity);

        /// <summary>
        /// Creates a new operation message.
        /// </summary>
        /// <param name="message">The message to create.</param>
        void CreateOperationMessage(IOperationMessage message);

        /// <summary>
        /// Creates a new serialized source system entity associated with an operation.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        void CreateOperationSourceSystemEntity(IOperationSerializedEntity entity);

        /// <summary>
        /// Creates or updates the specified entity type.
        /// </summary>
        /// <param name="entityType">The entity type to create or update.</param>
        void CreateOrUpdateEntityType(IEntityType entityType);

        /// <summary>
        /// Creates or updates the specified external system.
        /// </summary>
        /// <param name="externalSystem">The external system to create or update.</param>
        void CreateOrUpdateExternalSystem(IExternalSystem externalSystem);

        /// <summary>
        /// Creates or updates the specified feed.
        /// </summary>
        /// <param name="feed">The feed to create or update.</param>
        void CreateOrUpdateFeed(IFeed feed);

        /// <summary>
        /// Creates the specified application parameter.
        /// </summary>
        /// <param name="parameter">The parameter to create.</param>
        void CreateParameter(IParameter parameter);

        /// <summary>
        /// Creates a new shared source system identifier record.
        /// </summary>
        /// <param name="sharedSourceSystemIdentifier">
        /// The shared source system identifier record to create.
        /// </param>
        void CreateSharedSourceSystemIdentifier(
            ISharedSourceSystemIdentifier sharedSourceSystemIdentifier);

        /// <summary>
        /// Creates a new value change record.
        /// </summary>
        /// <param name="valueChange">The value change record to create.</param>
        void CreateValueChange(IValueChange valueChange);

        /// <summary>
        /// Deletes the specified application parameter.
        /// </summary>
        /// <param name="id">The ID of the parameter.</param>
        void DeleteParameter(Guid id);

        /// <summary>
        /// Gets the dictionary of application parameters that apply to the specified
        /// destination system.
        /// </summary>
        /// <param name="destinationSystemId">
        /// The ID of the destination (external) system.
        /// </param>
        /// <returns>
        /// The dictionary (name/value) of application parameters that apply to the
        /// specified destination system.
        /// </returns>
        IDictionary<string, string>
            GetDestinationSystemParameters(Guid destinationSystemId);

        /// <summary>
        /// Gets all entity mappings for the specified destination system that are in one
        /// of the specified states.
        /// </summary>
        /// <param name="destinationSystemId">
        /// The ID of the destination (external) system.
        /// </param>
        /// <param name="states">
        /// The collection of requested mapping states (codes).
        /// </param>
        /// <returns>The collection of entity mappings.</returns>
        IEnumerable<IEntityMapping> GetEntityMappings(
            Guid destinationSystemId, params int[] states);

        /// <summary>
        /// Gets the specified entity type.
        /// </summary>
        /// <param name="id">The ID of the requested entity type.</param>
        /// <returns>The requested entity type.</returns>
        IEntityType GetEntityType(Guid id);

        /// <summary>
        /// Gets the dictionary of application parameters that apply to the specified
        /// entity type.
        /// </summary>
        /// <param name="entityTypeId">The ID of the entity type.</param>
        /// <returns>
        /// The dictionary of application parameters that apply to the specified entity
        /// type.
        /// </returns>
        IDictionary<string, string> GetEntityTypeParameters(Guid entityTypeId);

        /// <summary>
        /// Gets the collection of all entity types.
        /// </summary>
        /// <returns>The collection of all entity types.</returns>
        IEnumerable<IEntityType> GetEntityTypes();

        /// <summary>
        /// Gets the dictionary of application parameters that apply to the specified
        /// entity type and source system.
        /// </summary>
        /// <param name="entityTypeId">The ID of the entity type.</param>
        /// <param name="sourceSystemId">The ID of the source (external) system.</param>
        /// <returns>
        /// The dictionary of application parameters that apply to the specified entity
        /// type and source system.
        /// </returns>
        IDictionary<string, string> GetEntityTypeSourceSystemParameters(
            Guid entityTypeId, Guid sourceSystemId);

        /// <summary>
        /// Gets the collection of all external systems.
        /// </summary>
        /// <returns>The collection of all external systems.</returns>
        IEnumerable<IExternalSystem> GetExternalSystems();

        /// <summary>
        /// Gets the full name of the type that represents the feed for the specified
        /// entity type and source system.
        /// </summary>
        /// <param name="entityTypeId">The ID of the entity type.</param>
        /// <param name="sourceSystemId">The ID of the source (external) system.</param>
        /// <returns>
        /// The full name of the type that represents the feed for the specified entity
        /// type and source system; if no feed is registered for the specified combination,
        /// a null reference will be returned.
        /// </returns>
        string GetFeedTypeFullName(Guid entityTypeId, Guid sourceSystemId);

        /// <summary>
        /// Gets the collection of identifiers that uniquely identify entities of the
        /// specified type in the destination system along with associated mapping
        /// information.
        /// </summary>
        /// <param name="entityTypeId">The ID of the entity type.</param>
        /// <returns>The collection of destination system identifiers.</returns>
        IEnumerable<IMappedEntityIdentifier> GetMappedEntityIdentifiers(Guid entityTypeId);

        /// <summary>
        /// Gets the serialized entity saved for the specified mapping.
        /// </summary>
        /// <param name="mappingId">The ID of the mapping.</param>
        /// <returns>The serialized entity saved for the specified mapping.</returns>
        byte[] GetMappingEntity(Guid mappingId);

        /// <summary>
        /// Gets the collection of all application parameters.
        /// </summary>
        /// <returns>The collection of all application parameters.</returns>
        IEnumerable<IParameter> GetParameters();

        /// <summary>
        /// Gets the collection of records specifying which source (external) systems share
        /// identifiers for individual entity type.
        /// </summary>
        /// <returns>
        /// The collection of records specifying which source (external) systems share
        /// identifiers for individual entity type.
        /// </returns>
        IEnumerable<ISharedIdentifierSourceSystem> GetSharedIdentifierSourceSystems();

        /// <summary>
        /// Gets the dictionary of application parameters that apply to the specified
        /// source system.
        /// </summary>
        /// <param name="sourceSystemId">The ID of the source (external) system.</param>
        /// <returns>
        /// The dictionary of application parameters that apply to the specified source
        /// system.
        /// </returns>
        IDictionary<string, string> GetSourceSystemParameters(Guid sourceSystemId);

        /// <summary>
        /// Sets the state of the specified mapping.
        /// </summary>
        /// <param name="mappingId">The ID of the mapping.</param>
        /// <param name="operation">The current operation.</param>
        /// <param name="state">The new state (code) of the mapping.</param>
        void SetMappingState(Guid mappingId, IOperation operation, int state);

        /// <summary>
        /// Sets the value of the specified application parameter.
        /// </summary>
        /// <param name="id">The ID of the parameter.</param>
        /// <param name="value">The new parameter value.</param>
        void SetParameterValue(Guid id, string value);

        /// <summary>
        /// Updates the last access correlation ID of the specified source system entity
        /// identity.
        /// </summary>
        /// <param name="operationIdIdentity">
        /// The current operation ID/source system entity identity combination.
        /// </param>
        void UpdateIdentityCorrelationId(IOperationIdIdentity operationIdIdentity);

        /// <summary>
        /// Updates the serialized source system entity for the specified mapping.
        /// </summary>
        /// <param name="mappingId">The ID of the mapping.</param>
        /// <param name="operation">The current operation.</param>       
        /// <param name="sourceSystemEntity">
        /// The new serialized source system entity.
        /// </param>
        void UpdateMappingEntity(
            Guid mappingId,
            IOperation operation,
            ISerializedEntity sourceSystemEntity);
    }
}
