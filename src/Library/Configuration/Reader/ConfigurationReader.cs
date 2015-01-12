using System;
using System.Linq;
using System.Configuration;

namespace Config.Library.Configuration
{
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

        /// <summary>
        /// Helper function that splits email addresses that are separated by a comma or semi-colon
        /// </summary>
        /// <param name="addresses">The email address list to be split</param>
        /// <returns>A string array containing one element for each separated email address</returns>
        protected string[] SplitAddresses(string addresses)
        {
            return addresses
                .Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim()).ToArray();
        }
    }
}
