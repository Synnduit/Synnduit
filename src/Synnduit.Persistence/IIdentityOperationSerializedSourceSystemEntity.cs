namespace Synnduit.Persistence
{
    /// <summary>
    /// Represents a serialized source system entity associated with a source system entity
    /// identity and (optionally) an operation.
    /// </summary>
    public interface IIdentityOperationSourceSystemEntity :
        IIdentityOperationObject
    {
        /// <summary>
        /// Gets the (serialized) source system entity.
        /// </summary>
        ISerializedEntity Entity { get; }
    }
}
