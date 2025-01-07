using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Drawing;

using Console = Colorful.Console;

namespace SQLI_Vuln_Tester
{
    internal class Website
    {
        public string URL;
        public string Schema;
        public string[] Params;


        public string BaseURL;

        public string Domain { get; set; }
        public string CurrentPage { get; set; }

        public Website(string _url)
        {

            URL = _url;
            Uri uri = new Uri(URL);

            Schema = uri.Scheme;
            Domain = uri.Host;
            CurrentPage = uri.AbsolutePath;

            BaseURL = $"{uri.Scheme}://{uri.Host}{uri.AbsolutePath}";

            var queryParameters = HttpUtility.ParseQueryString(uri.Query);
            string[] Parameters = new string[queryParameters.Count];

            for (int i = 0; i < queryParameters.Count; i++)
            {
                Parameters[i] = $"{queryParameters.AllKeys[i]}={queryParameters[queryParameters.AllKeys[i]]}";
            }


            System.Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nParsed URL Details:\n");

            System.Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("Domain: ");
            System.Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Domain);

            System.Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("Current Page: ");
            System.Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(CurrentPage);

            System.Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("Base URL: ");
            System.Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(BaseURL);

            System.Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nParsed Parameters:\n");

            for (int i = 0; i < Parameters.Length; i++)
            {
                System.Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(Parameters[i]);
            }

            Console.ResetColor();
            Params = Parameters;
            Console.WriteLine("\n\n");
        }

    }
}
