namespace Synnduit.Persistence
{
    /// <summary>
    /// Represents the identifier that uniquely identifies a known entity in the
    /// destination system along with associated mapping information.
    /// </summary>
    public interface IMappedEntityIdentifier
    {
        /// <summary>
        /// Gets the ID that uniquely identifies the entity in the destination system.
        /// </summary>
        string DestinationSystemEntityId { get; }

        /// <summary>
        /// Gets the mapping's origin (code).
        /// </summary>
        int State { get; }

        /// <summary>
        /// Gets the mapping's state (code).
        /// </summary>
        int Origin { get; }
    }
}
