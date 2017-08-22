namespace Synnduit.Persistence
{
    /// <summary>
    /// Represents a message associated with a source system entity identity and
    /// (optionally) an operation.
    /// </summary>
    public interface IIdentityOperationMessage : IIdentityOperationObject
    {
        /// <summary>
        /// Gets the message.
        /// </summary>
        IMessage Message { get; }
    }
}
