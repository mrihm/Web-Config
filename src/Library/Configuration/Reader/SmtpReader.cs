using System;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Config.Library.Domain;

namespace Config.Library.Configuration
{
    public class SmtpReader : ConfigurationReader<SmtpSection>, IConfigurationReader<SmtpSettings>
    {
        /// <summary>
        /// Inject the smtp configuration section to be read into the reader
        /// </summary>
        public SmtpReader(object configSection) : base(configSection) { }

        /// <summary>
        /// Read the smtp configuration section settings and convert the values into a
        /// </summary>
        /// <returns></returns>
        public SmtpSettings GetSettings()
        {
            var settings = new SmtpSettings();

            settings.Sender = ExtractSender();
            settings.ReplyTo = ExtractReply();
            settings.Client = ExtractServer();

            return settings;
        }

        /// <summary>
        /// Read the default email sender information and return a MailAddress object containing the email sender information
        /// </summary>
        /// <returns>A MailMessage object containing the sender information</returns>
        private MailAddress ExtractSender()
        {
            MailAddress sender = null;

            if (!String.IsNullOrWhiteSpace(_configuration.From)) 
                sender = new MailAddress(_configuration.From);

            return sender;
        }

        /// <summary>
        /// Create a MailAddress object for the replyTo email address if one was specified and is not the same as the sender
        /// </summary>
        /// <returns>A MailMessage object containing the reply to recipient information</returns>
        private MailAddress ExtractReply()
        {
            MailAddress replyTo = null;

            var sender = _configuration.From;
            var reply = _configuration.ReplyTo;

            if (!String.IsNullOrEmpty(reply) && (reply.CompareTo(sender) == 0)) {
                replyTo = new MailAddress(reply);
            }

            return replyTo;
        }

        /// <summary>
        /// Create the SmtpClient object to be used to send out emails
        /// </summary>
        /// <returns>The instantiated SmtpClient object used to send out the emails</returns>
        private SmtpClient ExtractServer()
        {
            var server = _configuration.Server;
            var user = _configuration.User;

            var client = new SmtpClient { Host = server.Smtp, EnableSsl = server.UseSsl };

            if (server.Port > 0)
                client.Port = server.Port;
            if (server.Authenticate)
                client.Credentials = new NetworkCredential(user.Name, user.Password);

            return client;
        }
    }
}
