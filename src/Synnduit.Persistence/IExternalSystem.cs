using System;

namespace Synnduit.Persistence
{
    /// <summary>
    /// Represents an external system.
    /// </summary>
    public interface IExternalSystem
    {
        /// <summary>
        /// Gets the ID of the external system.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the name of the external system.
        /// </summary>
        string Name { get; }
    }
}
