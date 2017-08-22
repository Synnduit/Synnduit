using System;

namespace Synnduit.Persistence
{
    /// <summary>
    /// The base interface for interfaces representing an object associated with a source
    /// system entity identity and (optionally) an operation.
    /// </summary>
    public interface IIdentityOperationObject
    {
        /// <summary>
        /// Gets the source system entity identity.
        /// </summary>
        ISourceSystemEntityIdentity Identity { get; }

        /// <summary>
        /// Gets the current operation.
        /// </summary>
        IOperation Operation { get; }
    }
}
