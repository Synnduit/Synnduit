using System;

namespace Synnduit.Persistence
{
    /// <summary>
    /// Represents an operation ID/source system entity identity combination, the
    /// information required to update the identity last access correlation ID.
    /// </summary>
    public interface IOperationIdIdentity
    {
        /// <summary>
        /// Gets the ID of the current operation.
        /// </summary>
        Guid OperationId { get; }

        /// <summary>
        /// Gets the source system entity identity.
        /// </summary>
        ISourceSystemEntityIdentity Identity { get; }
    }
}
