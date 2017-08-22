using System;

namespace Synnduit.Persistence
{
    /// <summary>
    /// Represents an application parameter.
    /// </summary>
    public interface IParameter
    {
        /// <summary>
        /// Gets the ID of the parameter.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the ID of the destination (external) system that the parameter is
        /// associated with.
        /// </summary>
        Guid? DestinationSystemId { get; }

        /// <summary>
        /// Gets the ID of the entity type that the parameter is associated with.
        /// </summary>
        Guid? EntityTypeId { get; }

        /// <summary>
        /// Gets the ID of the source (external) system that the parameter is associated
        /// with.
        /// </summary>
        Guid? SourceSystemId { get; }

        /// <summary>
        /// Gets the name of the parameter.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the parameter value.
        /// </summary>
        string Value { get; }
    }
}
