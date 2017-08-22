namespace Synnduit.Persistence
{
    /// <summary>
    /// Represents a serialized source/destination system entity associated with an
    /// operation.
    /// </summary>
    public interface IOperationSerializedEntity
    {
        /// <summary>
        /// Gets the current operation.
        /// </summary>
        IOperation Operation { get; }

        /// <summary>
        /// Gets the serialized source/destination system entity.
        /// </summary>
        ISerializedEntity Entity { get; }
    }
}
