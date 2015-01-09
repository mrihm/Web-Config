using System;
using System.Net.Mail;

namespace Config.Library.Domain
{
    public class SmtpSettings 
    {
        /// <summary>
        /// Set and return the SmtpClient to be used to send out emails
        /// </summary>
        public SmtpClient Client { get; set; }

        /// <summary>
        /// Set and return the mail address object for the default email sender
        /// </summary>
        public MailAddress Sender { get; set; }

        /// <summary>
        /// Set and return the Replyto mail address object for the default email sender
        /// </summary>
        public MailAddress ReplyTo { get; set; } 
    }
}
