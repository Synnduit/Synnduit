namespace Synnduit.Persistence
{
    /// <summary>
    /// Represents an entity transaction.
    /// </summary>
    public interface IEntityTransaction : IOperation
    {
        /// <summary>
        /// Gets the outcome of the transaction (code).
        /// </summary>
        int Outcome { get; }
    }
}
