using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Config.Library.Domain
{
    public class MailingSettings
    {
        /// <summary>
        /// Set and return the base path for the notification setup files and the attachments to be included
        /// </summary>
        public string Path { get; set; }

        public Content Content { get; set; }
        public Recipients Recipients { get; set; }
        public Schedule Schedule { get; set; }

        public IList<Document> Attachments { get; private set; }
    }

    /// <summary>
    /// Configuration wrapper class that encapsulates the email recipient information
    /// </summary>
    public class Recipients
    {
        public List<string> ToList { get; private set; }
        public List<string> CcList { get; private set; }
        public List<string> BccList { get; private set; }

        /// <summary>
        /// Instantiate the string lists that hold the recipient email addresses when the class is created
        /// </summary>
        public Recipients()
        {
            this.ToList = new List<string>();
            this.CcList = new List<string>();
            this.BccList = new List<string>();
        }
    }

    /// <summary>
    /// Configuration model class that encapsulates the file settings for the email content and the email attachments
    /// </summary>
    public class Document
    {
        public string FileName { get; set; }
    }

    /// <summary>
    /// Configuration model class that encapsulates the email content settings
    /// </summary>
    public class Content : Document
    {
        private string _subject;

        /// <summary>
        /// Set and return the email subject which can include one or more placeholders
        /// </summary>
        public string Subject
        {
            get { return _subject; }
            set
            {
                if (String.IsNullOrEmpty(value)) {
                    throw new ArgumentNullException("Notification subject cannot be null");
                }

                _subject = value;
            }
        }
    }
}
