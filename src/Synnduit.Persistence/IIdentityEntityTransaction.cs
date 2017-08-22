namespace Synnduit.Persistence
{
    /// <summary>
    /// Represents an identity entity transaction (i.e., a transaction not associated with
    /// a specific source/destination system identifier mapping, only a source system
    /// entity identity).
    /// </summary>
    public interface IIdentityEntityTransaction : IEntityTransaction
    {
        /// <summary>
        /// Gets the source system entity identity.
        /// </summary>
        ISourceSystemEntityIdentity Identity { get; }
    }
}
