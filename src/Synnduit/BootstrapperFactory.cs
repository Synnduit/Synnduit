using System;
using Synnduit.Configuration;

namespace Synnduit
{
    /// <summary>
    /// Creates instances of the <see cref="IBootstrapper" /> implementation.
    /// </summary>
    internal class BootstrapperFactory : IBootstrapperFactory
    {
        private static Lazy<BootstrapperFactory> instance =
            new Lazy<BootstrapperFactory>(() => new BootstrapperFactory());

        /// <summary>
        /// Gets the singleton instance of the class.
        /// </summary>
        public static BootstrapperFactory Instance
        {
            get { return instance.Value; }
        }

        private readonly IConfigurationSectionProvider configurationSectionProvider;

        private BootstrapperFactory()
            : this(ConfigurationSectionProvider.Instance)
        { }

        public BootstrapperFactory(
            IConfigurationSectionProvider configurationSectionProvider)
        {
            this.configurationSectionProvider = configurationSectionProvider;
        }

        /// <summary>
        /// Creates a new <see cref="IBootstrapper" /> implementation instance.
        /// </summary>
        /// <returns>
        /// A newly created <see cref="IBootstrapper" /> implementation instance.
        /// </returns>
        public IBootstrapper CreateBootstrapper()
        {
            return new Bootstrapper(this.configurationSectionProvider);
        }
    }
}
