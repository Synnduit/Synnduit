using Synnduit.Configuration;

namespace Synnduit
{
    /// <summary>
    /// Creates <see cref="IRunner" /> instances.
    /// </summary>
    public static class RunnerFactory
    {
        /// <summary>
        /// Creates a new <see cref="IRunner" /> instance for the specified configuration
        /// and run.
        /// </summary>
        /// <param name="configurationName">
        /// The name of the configuration (file) to use.
        /// </param>
        /// <param name="runName">
        /// The name of the run to configure the <see cref="IRunner" /> instance for; must
        /// exist within the specified configuration (file).
        /// </param>
        /// <returns>
        /// The <see cref="IRunner" /> instance created for the specified configuration
        /// and run.
        /// </returns>
        public static IRunner CreateRunner(
            string configurationName, string runName)
        {
            return new Runner(
                ConfigurationLoader.Instance,
                BootstrapperFactory.Instance,
                BridgeFactory.Instance,
                configurationName,
                runName);
        }
    }
}
