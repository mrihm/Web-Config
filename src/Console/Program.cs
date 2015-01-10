using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

using StructureMap;

using Config.Library.Configuration;
using Config.Library.Domain;

namespace Config.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var output = ConfigurationManager.GetSection("events.settings/output") as OutputSection;
            var ebola = output.Mailings["ebola"];

            Console.WriteLine("Output\n------\nPath: {0}\n", output.Path);

            Console.WriteLine("Mode\n----\nTest: {0}", output.Mode.Test);
            Console.WriteLine("Email: {0}\n", output.Mode.Email);

            Console.WriteLine("Mailing\n-------\nName: {0}", ebola.Name);
            Console.WriteLine("Content: {0}", ebola.Content.Content);
            Console.WriteLine("Subject: {0}\n", ebola.Content.Subject);
            Console.WriteLine("To: {0}", ebola.Recipients.To);
            Console.WriteLine("Cc: {0}", ebola.Recipients.Cc);
            Console.WriteLine("Bcc: {0}\n", ebola.Recipients.Bcc);
            Console.WriteLine("Delay: {0}", ebola.Schedule.Delay);
            Console.WriteLine("Immediately: {0}", ebola.Schedule.Immediately);
            Console.WriteLine("Runtime: {0}\n", ebola.Schedule.Runtime);

            foreach (AttachmentElement attachment in ebola.Attachments) {
                Console.WriteLine("  Attachment\n  -------\n  Name: {0}", attachment.Name);
                Console.WriteLine("  File: {0}\n", attachment.File);
            }

            return;

            using (var container = new Container())
            {
                InitContainer(container);

                var settings = (container.GetInstance<IConfigurationReader<SmtpSettings>>()).GetSettings();

                Console.WriteLine("Sender\n------\nAddress: {0}", settings.Sender.Address);
                Console.WriteLine("DisplayName: {0}\n", settings.Sender.DisplayName);

                if (settings.ReplyTo != null) {
                    Console.WriteLine("ReplyTo\n-------\nAddress: {0}", settings.ReplyTo.Address);
                    Console.WriteLine("DisplayName: {0}\n", settings.ReplyTo.DisplayName);
                }

                Console.WriteLine("Client\n------\nHost: {0}", settings.Client.Host);
                Console.WriteLine("EnableSsl: {0}", settings.Client.EnableSsl);
                Console.WriteLine("UserName: {0}", (settings.Client.Credentials as NetworkCredential).UserName);
                Console.WriteLine("{0}\n", (settings.Client.Credentials as NetworkCredential).Password);
            }
        }

        private static void InitContainer(Container container)
        {
            container.Configure(r => 
            {
                r.For<IConfigurationReader<SmtpSettings>>().Use<SmtpReader>().Ctor<object>().Is(ConfigurationManager.GetSection("events.settings/smtp"));
            });
        }
    }
}
