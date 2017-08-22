using System.Configuration;

namespace Synnduit.Configuration
{
    /// <summary>
    /// Represents the application's configuration file section.
    /// </summary>
    public class SynnduitConfigurationSection : ConfigurationSection
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public SynnduitConfigurationSection()
        { }

        /// <summary>
        /// Gets or sets the binary files directory path; the (MEF) exports from the DLL
        /// files in this directory shall be loaded.
        /// </summary>
        [ConfigurationProperty("binaryFilesDirectoryPath", IsRequired = true)]
        public string BinaryFilesDirectoryPath
        {
            get { return (string) this["binaryFilesDirectoryPath"]; }
            set { this["binaryFilesDirectoryPath"] = value; }
        }

        /// <summary>
        /// Gets or sets the configuration directory path; the configuration files for
        /// individual runs shall be loaded from this directory.
        /// </summary>
        [ConfigurationProperty("runConfigurationFilesDirectoryPath", IsRequired = true)]
        public string RunConfigurationFilesDirectoryPath
        {
            get { return (string) this["runConfigurationFilesDirectoryPath"]; }
            set { this["runConfigurationFilesDirectoryPath"] = value; }
        }

        /// <summary>
        /// Gets or sets the configuration element that controls the application's logging
        /// behavior.
        /// </summary>
        [ConfigurationProperty("logging")]
        public LoggingConfigurationElement Logging
        {
            get { return (LoggingConfigurationElement) this["logging"]; }
            set { this["logging"] = value; }
        }
    }
}
