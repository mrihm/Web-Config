using System;
using System.Configuration;
using System.Net.Mail;

using Config.Library.Settings;
using System.Net;

namespace Config.ConsoleApp.Configuration
{
    /// <summary>
    /// Internal configuration class that implements the section handler code which reads the content of the email settings custom 
    /// configuration section 
    /// </summary>
    public class SmtpConfiguration : ConfigurationSection, ISmtpSettings
    {
        #region :: attributes ::

        private MailAddress _sender;
        private MailAddress _replyTo;
        private SmtpClient _client;

        public MailAddress Sender 
        {
            get
            {
                if (_sender == null && !String.IsNullOrEmpty(this.From)) {
                    _sender = new MailAddress(this.From, this.Display);
                }

                return _sender;
            }
        }

        public MailAddress ReplyTo
        {
            get
            {
                if (_replyTo == null && !String.IsNullOrEmpty(this.Reply)) {
                    _replyTo = new MailAddress(this.Reply);
                }

                return _replyTo;
            }
        }

        public SmtpClient Client
        {
            get 
            {
                if (_client == null) 
                {
                    _client = new SmtpClient { Host = this.Server.Smtp, EnableSsl = this.Server.UseSsl };

                    if (this.Server.Port > 0) 
                        _client.Port = this.Server.Port;

                    if (this.Server.Authenticate) 
                        _client.Credentials = new NetworkCredential(this.User.Name, this.User.Password);
                }

                return _client;
            }
        }

        private string From
        {
            get { return (string)this["from"]; }
        }

        private string Display
        {
            get { return (string)this["display"]; }
        }

        private string Reply
        {
            get { return (string)this["reply"]; }
        }

        private ServerConfiguration Server
        {
            get { return (ServerConfiguration)this["server"]; }
        }

        private UserConfiguration User
        {
            get { return (UserConfiguration)this["user"]; }
        }

        #endregion

        public SmtpConfiguration()
        {
            Properties.Add(new ConfigurationProperty("from", typeof(string), "", ConfigurationPropertyOptions.IsRequired));
            Properties.Add(new ConfigurationProperty("display", typeof(string), "", ConfigurationPropertyOptions.IsRequired));
            Properties.Add(new ConfigurationProperty("reply", typeof(string), ""));

            Properties.Add(new ConfigurationProperty("user", typeof(UserConfiguration), null, ConfigurationPropertyOptions.IsRequired));
            Properties.Add(new ConfigurationProperty("server", typeof(ServerConfiguration), null, ConfigurationPropertyOptions.IsRequired));
        }

        /// <summary>
        /// Class that defines the attributes for the server configuration configurationElement of the custom email configuration section
        /// </summary>
        private class ServerConfiguration : ConfigurationElement
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
        /// Configuration class that defines the attributes for the user configuration configurationElement in the mail configuration section
        /// </summary>
        private class UserConfiguration : ConfigurationElement
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
}
