using System;

namespace Config.Library.Domain
{
    /// <summary>
    /// Interface that defines the methods to be implemented by the Smtp configuration section reader
    /// </summary>
    public interface ISmtpConfigurationReader
    {
        SmtpSettings GetSettings();
    }

    /// <summary>
    /// Interface that defines the methods to be implemented by the mailing output configuration section reader
    /// </summary>
    public interface IMailingConfigurationReader
    {
        MailingSettings GetSettings(string name);
    }
}
