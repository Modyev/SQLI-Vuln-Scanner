using System.Web;
using System;
using System.Diagnostics;
using System.Drawing;
//using Console = Colorful.Console;
using System.Linq;

namespace SQLI_Vuln_Tester
{
    internal class Program
    {
        public static string[] VulnPayloads =
        {
            "'",
            "' OR 1=1",
        };
        public static string[] VulnResponses =
        {
            "You have an error in your SQL".ToLower(),
            "SQL syntax".ToLower(),
            "MySQL".ToLower(),
            "syntax error",

        };
        public static bool VulnFlag = false;
        static void Main(string[] args)
        {
            Console.Title = "SQLI Automated Penetration Testing Tool";

            System.Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Input URL: ");
            System.Console.ForegroundColor = ConsoleColor.Green;
            string url = Console.ReadLine();
            Website site = new Website(url);

            if (!url.Contains("="))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("URL is potientially SAFE to SQL Injection");
                Console.ResetColor();
                return;
            }

            Tester(site);

            Console.WriteLine();
            if (VulnFlag)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("URL is potientially VULNERABLE to SQL Injection");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("URL is potientially SAFE to SQL Injection");
            }
            Console.ResetColor();

            Console.Read();
        }

        public static void Tester(Website site)
        {
            bool VulnFlag = false;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Starting The penetration test...\n", Color.DarkGreen);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            foreach (string payload in VulnPayloads)
            {
                string payloadedURL = site.URL;
                foreach (string param in site.Params)
                {
                    string paramValue = param.Split("=")[1];
                    payloadedURL = payloadedURL.Replace(paramValue, payload);
                }
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"Making Request with Payload ({payload}): {payloadedURL}");


                string[] vulnResponses = IdentifyIfSiteIsVuln(payloadedURL);
                if (vulnResponses[0] != null)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"\n[VULN] Website is Vuln to the payload ( {payload} )");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("Found Responses: {\"");

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    foreach(string vuln in vulnResponses)
                    {
                        Console.Write(vuln + " | ");
                    }
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("\"}");
                    Console.WriteLine();
                }
            }
        }
        private static string[] IdentifyIfSiteIsVuln(string url)
        {
            string[] Responses = new string[6];
            int index = 0;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var req = client.GetAsync(url);
                    if (req.Result.IsSuccessStatusCode)
                    {
                        string response = req.Result.Content.ReadAsStringAsync().Result;
                        foreach(string vulnResp in VulnResponses)
                        {
                            if (response.ToLower().Contains(vulnResp))
                            {
                                Responses[index] = vulnResp;
                                index++;
                                VulnFlag = true;

                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"[ERROR] Couldn't make a request to {url}  Status code: {req.Result.StatusCode}", Color.Red);
                        return Responses;
                    }
                }
            } catch (Exception e)
            {
                Console.WriteLine("[ERROR] " + e.Message, Color.Red);
            }
            return Responses;
        }
    }
}
