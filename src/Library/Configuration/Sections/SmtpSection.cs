using System;
using System.Configuration;

namespace Config.Library.Configuration
{
    /// <summary>
    /// Represent the smtp custom configuration section
    /// </summary>
    public class SmtpSection : ConfigurationSection
    {
        /// <summary>
        /// Return the sender email address to be used when sending out emails
        /// </summary>
        [ConfigurationProperty("from", DefaultValue = "", IsRequired = false)]
        public string From
        {
            get { return (string)this["from"]; }
        }

        /// <summary>
        /// Return the 'Reply To' email address
        /// </summary>
        [ConfigurationProperty("reply", DefaultValue = "", IsRequired = false)]
        public string ReplyTo
        {
            get { return (string)this["reply"]; }
        }

        /// <summary>
        /// Return the configuration element that defines the notification mode for all emails
        /// </summary>
        [ConfigurationProperty("mode", IsRequired = true)]
        public ModeElement Mode
        {
            get { return this["mode"] as ModeElement; }
        }

        /// <summary>
        /// Return the configuration configurationElement that holds the user authentication information if authentication is required
        /// </summary>
        [ConfigurationProperty("user", IsRequired = true)]
        public UserElement User
        {
            get { return (UserElement)this["user"]; }
        }

        /// <summary>
        /// Return the configuration configurationElement that holds the server information for the custom email configuration section
        /// </summary>
        [ConfigurationProperty("server", IsRequired = true)]
        public ServerElement Server
        {
            get { return (ServerElement)this["server"]; }
        }
    }

    /// <summary>
    /// Represent the server configuration element in the smtp custom configuration section
    /// </summary>
    public class ServerElement : ConfigurationElement
    {
        /// <summary>
        /// Return the outgoing server name to be used for sending out emails
        /// </summary>
        [ConfigurationProperty("smtp", IsRequired = true)]
        public string Smtp
        {
            get { return (string)this["smtp"]; }
        }

        /// <summary>
        /// Return the port to be used for sending out emails
        /// </summary>
        [ConfigurationProperty("port", DefaultValue = "0", IsRequired = false)]
        public int Port
        {
            get { return (int)this["port"]; }
        }

        /// <summary>
        /// Return the flag which indicates whether user authentication is required when sending out emails
        /// </summary>
        [ConfigurationProperty("authenticate", DefaultValue = false, IsRequired = false)]
        public bool Authenticate
        {
            get { return (this.Properties["authenticate"] == null) ? false : (bool)this["authenticate"]; }
        }

        /// <summary>
        /// Return the flag which indicates whether to use Ssl when sending emails
        /// </summary>
        [ConfigurationProperty("ssl", DefaultValue = false, IsRequired = false)]
        public bool UseSsl
        {
            get { return (this.Properties["ssl"] == null) ? false : (bool)this["ssl"]; }
        }
    }

    /// <summary>
    /// Represent the user configuration element in the smtp custom configuration section
    /// </summary>
    public class UserElement : ConfigurationElement
    {
        /// <summary>
        /// Return the user name to be used if authentication is required when sending out emails
        /// </summary>
        [ConfigurationProperty("name", DefaultValue = "", IsRequired = false)]
        public string Name
        {
            get { return (string)this["name"]; }
        }

        /// <summary>
        /// Return the password to be used if authentication is required when sending out emails
        /// </summary>
        [ConfigurationProperty("password", DefaultValue = "", IsRequired = false)]
        public string Password
        {
            get
            {
                string password = string.Empty;

                if (this.Properties["password"] != null)
                    password = this["password"] as string;

                return password;
            }
        }
    }
}
