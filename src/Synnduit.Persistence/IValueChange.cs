using System;

namespace Synnduit.Persistence
{
    /// <summary>
    /// Represents a value change record.
    /// </summary>
    public interface IValueChange
    {
        /// <summary>
        /// Gets the ID of the value change record.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the ID of the mapping entity transaction that the value change is
        /// associated with.
        /// </summary>
        Guid MappingEntityTransactionId { get; }

        /// <summary>
        /// Gets the name of the value that has changed.
        /// </summary>
        string ValueName { get; }

        /// <summary>
        /// Gets the previous value.
        /// </summary>
        string PreviousValue { get; }

        /// <summary>
        /// Gets the new value.
        /// </summary>
        string NewValue { get; }
    }
}
