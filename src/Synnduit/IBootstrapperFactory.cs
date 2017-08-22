namespace Synnduit
{
    /// <summary>
    /// Creates instances of the <see cref="IBootstrapper" /> implementation.
    /// </summary>
    internal interface IBootstrapperFactory
    {
        /// <summary>
        /// Creates a new <see cref="IBootstrapper" /> implementation instance.
        /// </summary>
        /// <returns>
        /// A newly created <see cref="IBootstrapper" /> implementation instance.
        /// </returns>
        IBootstrapper CreateBootstrapper();
    }
}
