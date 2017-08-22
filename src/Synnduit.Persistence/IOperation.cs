using System;

namespace Synnduit.Persistence
{
    /// <summary>
    /// Represents an operation; any information regarding an operation will always be
    /// associated with one and the same instance (i.e., ID).
    /// </summary>
    public interface IOperation
    {
        /// <summary>
        /// Gets the ID of the operation.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the operation's time stamp.
        /// </summary>
        DateTimeOffset TimeStamp { get; }
    }
}
