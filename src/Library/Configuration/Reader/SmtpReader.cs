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
    public class SmtpReader : ConfigurationReader<SmtpSection>, ISmtpConfigurationReader
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
            var settings = new SmtpSettings { Sender = new MailAddress(_configuration.From) };

            settings.Mode = ReadMode();
            settings.ReplyTo = ExtractReply();
            settings.Client = ExtractServer();

            return settings;
        }

        /// <summary>
        /// Extract the mailing mode information that indicates whether to send out test emails
        /// </summary>
        /// <returns>A configuration object containing the mailing mode settings</returns>
        private MailingMode ReadMode()
        {
            var mailingMode = new MailingMode { UseTest = _configuration.Mode.Test };

            if (mailingMode.UseTest) {
                mailingMode.RedirectTo.AddRange(SplitAddresses(_configuration.Mode.Email));
            }

            return mailingMode;
        }

        /// <summary>
        /// Create a MailAddress object for the replyTo email address if one was specified and is not the same as the sender
        /// </summary>
        /// <returns>A MailMessage object containing the reply to recipient information</returns>
        private MailAddress ExtractReply()
        {
            if (String.IsNullOrEmpty(_configuration.ReplyTo))
                return null;

            MailAddress replyTo = new MailAddress(_configuration.ReplyTo);
            MailAddress sender = new MailAddress(_configuration.From);

            if (replyTo.Address.Equals(sender.Address, StringComparison.InvariantCultureIgnoreCase)) {
                replyTo = null;
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
