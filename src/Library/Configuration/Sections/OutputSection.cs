using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Config.Library.Configuration
{
    #region :: output section ::

    /// <summary>
    /// Section handler that reads the content of the output configuration section. The output section defines the settings
    /// for all notification emails 
    /// </summary>
    public class OutputSection : ConfigurationSection
    {
        /// <summary>
        /// Get and set the base path for the setup files and any attachments
        /// </summary>
        [ConfigurationProperty("path", DefaultValue = "", IsRequired = true)]
        public string Path
        {
            get { return (string)this["path"]; }
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
        /// Return the custom configuration collection containing a list of mailing elements
        /// </summary>
        /// <remarks>
        /// We do not require an element to hold the collection items. Instead, we add the individual mailing elements directly
        /// under the output node
        /// </remarks>
        [ConfigurationProperty("", IsRequired = true, IsKey = false, IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(MailingCollection))]
        public MailingCollection Mailings
        {
            get { return (MailingCollection)(base[""]); }
        }
    }

    /// <summary>
    /// Represent the mailing test mode configuration element in the output custom configuration section
    /// </summary>
    public class ModeElement : ConfigurationElement
    {
        /// <summary>
        /// Return the element configuration configurationElement that indicates whether the emails are sent out in test mode in which case the 
        /// email recipient addresses are replaced with the test email address
        /// </summary>
        /// <remarks>
        /// The test email address attribute is required if the test mode is set to true
        /// </remarks>
        [ConfigurationProperty("test", DefaultValue = true, IsRequired = true)]
        public bool Test
        {
            get
            {
                bool testEmail = (bool)this["test"];

                if (testEmail && String.IsNullOrWhiteSpace(this.Email))
                    throw new ConfigurationErrorsException("The \"email\" attribute is required when sending out test emails");

                return testEmail;
            }
        }

        /// <summary>
        /// Return the email addresses that receive all emails when the test attribute is set to true. The original recipients
        /// ('To', 'Cc', and 'Bcc') will not receive any emails in those cases
        /// </summary>
        [ConfigurationProperty("email", DefaultValue = "", IsRequired = false)]
        public string Email
        {
            get { return (string)this["email"]; }
        }
    }

    #endregion

    #region :: mailing elements ::

    /// <summary>
    /// Collection that contains instances of the mailing configuration element
    /// </summary>
    [ConfigurationCollection(typeof(MailingElement))]
    public class MailingCollection : ConfigurationElementCollection
    {
        #region :: properties ::

        /// <summary>
        /// Return the mailing configuration element that matches the specified key
        /// </summary>
        /// <param name="key">The name that identifies the output collection</param>
        /// <returns>The output configuration element matching the specified name</returns>
        public new MailingElement this[string key]
        {
            get { return base.BaseGet(key) as MailingElement; }
        }

        #endregion

        #region :: overrides ::

        /// <summary>
        /// Return the collection type enumeration to indicate the collection type.
        /// </summary>
        /// <remarks>
        /// The return type is BasicMap which specifies a collection type containing elements that apply to this level only
        /// </remarks>
        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        /// <summary>
        /// Specify the configuration element name to be used for the collection
        /// </summary>
        protected override string ElementName
        {
            get { return "mailing"; }
        }

        /// <summary>
        /// Create a new mailing configuration element when a new collection element is created 
        /// </summary>
        /// <returns>The new mailing configuration element</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new MailingElement();
        }

        /// <summary>
        /// Return the key to be used with the mailing configuration collection 
        /// </summary>
        /// <param name="element">The configuration element for which to return the key</param>
        /// <returns>The key for the configuration element</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((MailingElement)element).Name;
        }

        #endregion
    }

    /// <summary>
    /// Represent the mailing configuration element in the output custom configuration section
    /// </summary>
    public class MailingElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return (string)base["name"]; }
        }

        [ConfigurationProperty("content", IsRequired = true)]
        public ContentElement Content
        {
            get { return (ContentElement)base["content"]; }
        }

        [ConfigurationProperty("recipients", IsRequired = true)]
        public RecipientsElement Recipients
        {
            get { return (RecipientsElement)base["recipients"]; }
        }

        [ConfigurationProperty("schedule", IsRequired = true)]
        public ScheduleElement Schedule
        {
            get { return (ScheduleElement)base["schedule"]; }
        }

        /// <summary>
        /// Return the custom configuration collection containing a list of attachment elements
        /// </summary>
        /// <remarks>
        /// We do not require an element to hold the collection items. Instead, we add the individual attachment elements directly
        /// under the output node
        /// </remarks>
        [ConfigurationProperty("", IsRequired = true, IsKey = false, IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(AttachmentCollection))]
        public AttachmentCollection Attachments
        {
            get { return (AttachmentCollection)(base[""]); }
        }
    }

    /// <summary>
    /// Represent the schedule configuration element in the output custom configuration section
    /// </summary>
    /// <remarks>
    /// The schedule includes the time of day when a service should run and how a service behaves after startup
    /// </remarks>
    public class ScheduleElement : ConfigurationElement
    {
        /// <summary>
        /// Return the initial delay (in milliseconds) if the Windows service is to be run immediatley.
        /// </summary>
        /// <remarks>
        /// The delay value is only needed when the run immediately attribute is set to true. Specifying a delay allows us time 
        /// to attach to the debugger to the Windows service before any processing code is executed.
        /// </remarks>
        [ConfigurationProperty("delay", DefaultValue = 30000, IsRequired = false)]
        public int Delay
        {
            get { return (int)this["delay"]; }
        }

        /// <summary>
        /// Return the time when the service should run. The offsetTime is specified as number of hours and minutes after midnight 
        /// when the service should run (expressed as a double value).
        /// </summary>
        [ConfigurationProperty("runtime", IsRequired = true)]
        public double Runtime
        {
            get { return (double)this["runtime"]; }
        }

        /// <summary>
        /// Indicates whether the Windows service should run immediately or whether it should run based on the schedule defined
        /// by the offsetTime value.
        /// </summary>
        [ConfigurationProperty("immediately", DefaultValue = false, IsRequired = true)]
        public bool Immediately
        {
            get { return (bool)this["immediately"]; }
        }
    }

    /// <summary>
    /// Represent the content configuration element in the output custom configuration section
    /// </summary>
    /// <remarks>
    /// The content section defines the email subject and the name of the Xsl transformation file used to generate the email body
    /// </remarks>
    public class ContentElement : ConfigurationElement
    {
        /// <summary>
        /// Return the name of the name of the content file that contains the email text
        /// </summary>
        [ConfigurationProperty("content", DefaultValue = "", IsRequired = true)]
        public string Content
        {
            get { return (string)this["content"]; }
        }

        /// <summary>
        /// Return the subject line to be used for the email 
        /// </summary>
        [ConfigurationProperty("subject", DefaultValue = "", IsRequired = true)]
        public string Subject
        {
            get { return (string)this["subject"]; }
        }
    }

    /// <summary>
    /// Represent the recipients configuration element in the output custom configuration section
    /// </summary>
    public class RecipientsElement : ConfigurationElement
    {
        /// <summary>
        /// Return the list of mailing recipient email addresses
        /// </summary>
        [ConfigurationProperty("to", DefaultValue = "", IsRequired = false)]
        public string To
        {
            get { return (string)this["to"]; }
        }

        /// <summary>
        /// Return the list of email addresses that should be added as Cc recipients 
        /// </summary>
        [ConfigurationProperty("cc", DefaultValue = "", IsRequired = false)]
        public string Cc
        {
            get { return (string)this["cc"]; }
        }

        /// <summary>
        /// Return the list of email addresses that should be added as Bcc recipients 
        /// </summary>
        [ConfigurationProperty("bcc", DefaultValue = "", IsRequired = false)]
        public string Bcc
        {
            get { return (string)this["bcc"]; }
        }
    }

    #endregion

    #region :: attachment elements ::

    /// <summary>
    /// Collection that contains AttachmentElement instances 
    /// </summary>
    [ConfigurationCollection(typeof(AttachmentElement))]
    public class AttachmentCollection : ConfigurationElementCollection
    {
        #region :: properties ::

        /// <summary>
        /// Return the attachment configuration element that matches the specified key 
        /// </summary>
        /// <param name="key">The file name that identifies the attachment element</param>
        /// <returns>The attachment configuration element matching the specified key</returns>
        public new AttachmentElement this[string key]
        {
            get { return base.BaseGet(key) as AttachmentElement; }
        }

        #endregion

        #region :: overrides ::

        /// <summary>
        /// Return the collection type enumeration to indicate the collection type.
        /// </summary>
        /// <remarks>
        /// The return type is BasicMap which specifies a collection type containing elements that apply to this level only
        /// </remarks>
        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        /// <summary>
        /// Return the configuration element name to be used for the collection
        /// </summary>
        protected override string ElementName
        {
            get { return "attachment"; }
        }

        /// <summary>
        /// Create a new attachment configuration element when a new attachment collection element is created
        /// </summary>
        /// <returns>The new attachment configuration element</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new AttachmentElement();
        }

        /// <summary>
        /// Return the key to be used with the attachment configuration collection 
        /// </summary>
        /// <param name="element">The configuration collection for which to return the key</param>
        /// <returns>The key for the configuration element</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((AttachmentElement)element).Name;
        }

        #endregion
    }

    /// <summary>
    /// Represent the attachment configuration element in the output custom configuration section
    /// </summary>
    public class AttachmentElement : ConfigurationElement
    {
        /// <summary>
        /// Return the name of the attachment
        /// </summary>
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)this["name"]; }
        }

        /// <summary>
        /// Return the file name of the attachment to be attached
        /// </summary>
        [ConfigurationProperty("file", IsRequired = true)]
        public string File
        {
            get { return (string)this["file"]; }
        }
    }

    #endregion
}
