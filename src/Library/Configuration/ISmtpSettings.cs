using System;
using System.Net.Mail;

namespace Config.Library.Settings
{
    public interface ISmtpSettings
    {
        SmtpClient Client { get; }
        MailAddress Sender { get; }
        MailAddress ReplyTo { get; }
    }
}
