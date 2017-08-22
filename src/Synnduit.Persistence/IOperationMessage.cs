using System;

namespace Synnduit.Persistence
{
    /// <summary>
    /// Represents a message associated with an operation.
    /// </summary>
    public interface IOperationMessage
    {
        /// <summary>
        /// Gets the current operation.
        /// </summary>
        IOperation Operation { get; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        IMessage Message { get; }
    }
}
