using System;
using System.Configuration;
using System.IO;
using System.Text;

namespace DataGridConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string url = String.Empty;
            string username = String.Empty;
            string password = String.Empty;
            RelativityHttpClient client;
            // get credentials for authentication
            if (args.Length == 1)
            {
                // read credentials from a file
                string credsFilePath = args[0];
                if (File.Exists(credsFilePath))
                {
                    string[] creds = File.ReadAllLines(credsFilePath);
                    const int numLines = 3;
                    if (creds.Length != numLines)
                    {
                        Console.WriteLine($"Need {numLines} lines in {credsFilePath} in the format:");
                        Console.WriteLine("https://hostname.com");
                        Console.WriteLine("username@hostname.com");
                        Console.WriteLine("MyPassword!1234");
                        PauseExec();
                        return;
                    }
                    url = creds[0];
                    username = creds[1];
                    password = creds[2];
                    
                }
                else
                {
                    Console.WriteLine($"Specified file {credsFilePath} does not exist.");
                    PauseExec();
                }               
            }
            else
            {
                // try to read from app.config file
                // read credentials from app.config
                var configReader = new AppSettingsReader();
                url = configReader.GetValue("RelativityBaseUrl", typeof(string)).ToString();
                username = configReader.GetValue("RelativityUserName", typeof(string)).ToString();
                password = configReader.GetValue("RelativityPassword", typeof(string)).ToString();

            }

            // if anything is empty, have the end-user input the credentials
            if (String.IsNullOrEmpty(url) ||
                String.IsNullOrEmpty(username) ||
                String.IsNullOrEmpty(password))
            {
                ReadUserInput(out url, out username, out password);
            }
            PauseExec();
            // instantiate DataGridClient and perform actions
            client = new RelativityHttpClient(url, username, password);
            Console.WriteLine("Successfully instantiated RelativityHttpClient.");
            Console.WriteLine("-----------");

            // finally, run the samples
            RunTests(client);
        }


        private static void RunTests(RelativityHttpClient client)
        {
            Console.WriteLine("Querying for the first 100 audit records...");
            DataGridRestSamples.PrintAuditRecords(client);
            PauseExec();
            Console.WriteLine("-----------");
            const int NUM_DAYS = 30;
            Console.WriteLine($"Querying for users who logged in within the last {NUM_DAYS} days...");
            DataGridRestSamples.PrintUsersLoggedInRecently(client, NUM_DAYS);
            PauseExec();
        }


        /// <summary>
        /// Reads in and validates the user credentials by attempting to log in
        /// </summary>
        /// <param name="url"></param>
        /// <param name="user"></param>
        /// <param name="pw"></param>
        /// <returns></returns>
        private static void ReadUserInput(out string url, out string user, out string pw)
        {
            Console.WriteLine("Please enter your Relativity instance URL (e.g. https://my-instance.com).");
            url = Console.ReadLine();
            Console.WriteLine("Please enter your Relativity username (e.g. albert.einstein@relativity.com).");
            user = Console.ReadLine();
            Console.WriteLine("Please enter your Relativity password. The cursor will not move.");
            StringBuilder pwBuilder = new StringBuilder();
            // hide password
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }
                if (key.Key == ConsoleKey.Backspace && pwBuilder.Length > 0)
                {
                    // remove last element
                    pwBuilder.Remove(pwBuilder.Length - 1, 1);
                }
                else
                {
                    pwBuilder.Append(key.KeyChar);
                }
            }
            pw = pwBuilder.ToString();
        }



        /// <summary>
        /// Pauses terminal at end of execution.
        /// </summary>
        private static void PauseExec()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }
    }
}
