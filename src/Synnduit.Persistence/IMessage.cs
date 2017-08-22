namespace Synnduit.Persistence
{
    /// <summary>
    /// Represents a log message.
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// Gets the message type (code).
        /// </summary>
        int Type { get; }

        /// <summary>
        /// Gets the hash of the text of the message.
        /// </summary>
        string TextHash { get; }

        /// <summary>
        /// Gets the text of the message.
        /// </summary>
        string Text { get; }
    }
}
