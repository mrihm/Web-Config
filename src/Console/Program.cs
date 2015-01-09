using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

using Config.ConsoleApp.Configuration;

namespace Config.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = ConfigurationManager.GetSection("smtp") as SmtpConfiguration;

            Console.WriteLine(settings.Client.Host);
            Console.WriteLine((settings.Client.Credentials as System.Net.NetworkCredential).UserName);
            Console.WriteLine("{0}\n", (settings.Client.Credentials as System.Net.NetworkCredential).Password);

            Console.WriteLine(settings.Sender.Address);
            Console.WriteLine("{0}\n", settings.Sender.DisplayName);

        }
    }
}
