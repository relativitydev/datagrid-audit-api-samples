using System;
using System.IO;

namespace DataGridConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // get credentials for authentication
            if (args.Length == 1)
            {
                // read credentials from a file
                string credsFilePath = args[0];
                if (File.Exists(credsFilePath))
                {
                    string[] creds = File.ReadAllLines(credsFilePath);
                    const int NUM_LINES = 3;
                    if (creds.Length != NUM_LINES)
                    {
                        Console.WriteLine($"Need {NUM_LINES} lines in {credsFilePath} in the format:");
                        Console.WriteLine("https://hostname.com");
                        Console.WriteLine("username@hostname.com");
                        Console.WriteLine("MyPassword!1234");
                        PauseExec();
                        return;
                    }
                    string url = creds[0];
                    string username = creds[1];
                    string password = creds[2];
                    // instantiate DataGridClient and perform actions
                    DataGridClient client = new DataGridClient(url, username, password);
                    Console.WriteLine("Successfully instantiated DataGridClient.");
                    Console.WriteLine("-----------");
                    PauseExec();
                    Console.WriteLine("Querying for the first 100 audit records...");
                    RestSamples.GetAuditRecords(client);
                    PauseExec();
                }
                else
                {
                    Console.WriteLine($"Specified file {credsFilePath} does not exist.");
                    PauseExec();
                }
                
            }
            else
            {
                // TODO: implement reading from user input
                //Console.WriteLine("Please enter your Relativity URL (e.g. https://my-instance.com");
                Console.WriteLine("Please specify a path to a credentials file.");
                PauseExec();
            }          
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
