using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Config.Library.Domain
{
    /// <summary>
    /// Configuration model class that encapsulates the output mailing settings
    /// </summary>
    public class MailingSettings
    {
        public string Path { get; set; }

        public Content Content { get; set; }
        public Recipients Recipients { get; set; }
        public Schedule Schedule { get; set; }

        public IList<Document> Attachments { get; private set; }

        /// <summary>
        /// Instantiate the attachment list when the object is created
        /// </summary>
        public MailingSettings()
        {
            this.Attachments = new List<Document>();
        }
    }

    /// <summary>
    /// Configuration model class that encapsulates the email mode settings which control whether emails are redirected for test purposes
    /// </summary>
    public class MailingMode
    {
        /// <summary>
        /// Flag indicating whether emails should be sent out in test mode which means emails get redirected to a test address
        /// </summary>
        public bool UseTest { get; set; }

        /// <summary>
        /// Collection that holds the list of emails to which test emails should be redirected
        /// </summary>
        public List<string> RedirectTo { get; private set; }

        public MailingMode()
        {
            this.RedirectTo = new List<string>();
        }
    }

    /// <summary>
    /// Configuration model class that encapsulates the email recipient information
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

        /// <summary>
        /// Parameterless constructor
        /// </summary>
        public Document() { }

        /// <summary>
        /// Constructor that takes the document file name as an argument
        /// </summary>
        public Document(string fileName)
        {
            this.FileName = fileName;
        }
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

    /// <summary>
    /// Schedule class that encapsulates the notification schedule information
    /// </summary>
    public class Schedule
    {
        private int _startDelay;

        /// <summary>
        /// The number of hours and minutes after midnight when the service should run
        /// </summary>
        public double Offset { get; set; }

        /// <summary>
        /// Set and return the flag that indicates whether to run the process immediately or whether to schedule the execution
        /// </summary>
        public bool StartImmediately { get; set; }

        /// <summary>
        /// Set and return the delay, in milliseconds, if the process is to be run immediately
        /// </summary>
        public int StartDelay
        {
            get { return (this.StartImmediately) ? _startDelay : 0; }
            set
            {
                if (value < 0) {
                    throw new ArgumentException("Invalid start delay value");
                }

                _startDelay = value;
            }
        }
    }

    /// <summary>
    /// Domain model class that encapsulates the scheduling information
    /// </summary>
    public class Timetable
    {
        /// <summary>
        /// Get and set the interval until a service should run next
        /// </summary>
        public int Interval { get; set; }

        /// <summary>
        /// Get and set the optional start date for the service 
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Get and set the optional end date for the service 
        /// </summary>
        public DateTime? EndDate { get; set; }
    }
}
