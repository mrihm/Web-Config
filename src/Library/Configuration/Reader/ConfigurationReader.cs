using System;
using System.Configuration;

namespace Config.Library.Configuration
{
    /// <summary>
    /// Generic interface that defines the methods to be implemented by configuration readers
    /// </summary>
    public interface IConfigurationReader<TSettings>
    {
        TSettings GetSettings();
    }


    /// <summary>
    /// Abstract base class for classes that implement the IConfigurationReader interface. The base class is implemented as a 
    /// generic class so that we can specify which (custom) configuration section should be read.
    /// </summary>
    public abstract class ConfigurationReader<TConfiguration> 
        where TConfiguration : ConfigurationSection
    {
        protected readonly TConfiguration _configuration;

        /// <summary>
        /// Inject the configuration section to be read into the reader and try to cast the configuration section as the 
        /// specified configuration class
        /// </summary>
        public ConfigurationReader(object configSection)
        {
            if (configSection == null)
                throw new ArgumentNullException("The configuration section cannot be null");

            _configuration = configSection as TConfiguration;

            if (_configuration == null)
                throw new ArgumentException("The configuration section is missing or invalid");
        }
    }
}
