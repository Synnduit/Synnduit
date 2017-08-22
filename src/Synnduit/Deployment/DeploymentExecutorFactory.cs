namespace Synnduit.Deployment
{
    /// <summary>
    /// Creates <see cref="IDeploymentExecutor" /> instances.
    /// </summary>
    public static class DeploymentExecutorFactory
    {
        /// <summary>
        /// Creates a new <see cref="IDeploymentExecutor" /> instance.
        /// </summary>
        /// <returns>The <see cref="IDeploymentExecutor" /> instance created.</returns>
        public static IDeploymentExecutor CreateDeploymentExecutor()
        {
            return new DeploymentExecutor(BootstrapperFactory.Instance);
        }
    }
}
