using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Config.Library.Domain;

namespace Config.Library.Configuration
{
    public class MailingReader : ConfigurationReader<OutputSection>, IMailingConfigurationReader
    {
        /// <summary>
        /// Inject the mailing output configuration section to be read into the reader
        /// </summary>
        public MailingReader(object configSection) : base(configSection) { }

        /// <summary>
        /// Read the mailing output settings into a MailingSettings object
        /// </summary>
        /// <remarks>
        /// The output configuration section can contain multiple mailing entries, each identified by a unique name
        /// </remarks>
        /// <param name="name">The name of the mailing output to be read</param>
        /// <returns></returns>
        public MailingSettings GetSettings(string name)
        {
            var mailing = _configuration.Mailings[name];

            if (mailing == null) {
                return null;
            }

            var settings = new MailingSettings { Path = _configuration.Path };

            settings.Content = ReadContent(mailing.Content);
            settings.Schedule = ReadSchedule(mailing.Schedule); 
            settings.Recipients = ReadRecipients(mailing.Recipients);

            foreach (AttachmentElement attachment in mailing.Attachments) {
                settings.Attachments.Add(new Document(attachment.File));
            }

            return settings;
        }

        /// <summary>
        /// Extract the recipient information for the mailing output
        /// </summary>
        /// <param name="element">The recipient configuration element</param>
        /// <returns>A configuration object containing the recipient information</returns>
        private Recipients ReadRecipients(RecipientsElement element)
        {
            var recipients = new Recipients();

            recipients.ToList.AddRange(SplitAddresses(element.To));
            recipients.CcList.AddRange(SplitAddresses(element.Cc));
            recipients.BccList.AddRange(SplitAddresses(element.Bcc));

            return recipients;
        }

        /// <summary>
        /// Extract the content information for the mailing output
        /// </summary>
        /// <param name="element">The content configuration element</param>
        /// <returns>A configuration object containing the content information</returns>
        private Content ReadContent(ContentElement element)
        {
            return new Content { FileName = element.Content, Subject = element.Subject }; 
        }

        /// <summary>
        /// Extract the schedule information for the mailing output
        /// </summary>
        /// <param name="element">The schedule configuration element</param>
        /// <returns>A configuration object containing the schedule information</returns>
        private Schedule ReadSchedule(ScheduleElement element)
        {
            return new Schedule { StartDelay = element.Delay, Offset = element.Runtime, StartImmediately = element.Immediately };
        }
    }
}
