using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kCura.AuditUI2.Services.ExternalAuditLog;
using Relativity.Services.Objects;
using Relativity.Services.ServiceProxy;

namespace DataGridAuditAPISamples
{
    public class Program
    {
        public static void Main(string[] args)
        {

            //Console.WriteLine("##########################################");
            //Console.WriteLine("#       DATAGRID AUDIT API SAMPLES       #");
            //Console.WriteLine("#           ORDER OF SAMPLES:            #");
            //Console.WriteLine("#  1. Find Last Login Date of All Users  #");
            //Console.WriteLine("#  2. Find All Inactive Users (30 days)  #");
            //Console.WriteLine("#  3. Find All Errors in Audit Logs      #");
            //Console.WriteLine("#  4. Find All Actions by Specific User  #");
            //Console.WriteLine("#  5. Find the 100 Most Recent Audits    #");
            //Console.WriteLine("##########################################\r\n");
            if (args.Length != 1)
            {
                Console.WriteLine("Must specify path to credentials file in the following format:");
                Console.WriteLine("http://instance.com");
                Console.WriteLine("Username");
                Console.WriteLine("Password");
                return;
            }

            // read credentials from file
            var connMgr = new ConnectionHelper(args[0]);
            Console.WriteLine("Successfully read credentials from file.");
            
            Pause();

            // if successful, instantiate services
            ServiceFactory serviceFactory = connMgr.GetServiceFactory();
            using (var logMgr = serviceFactory.CreateProxy<IExternalAuditLogObjectManager>())
            using (var objMgr = serviceFactory.CreateProxy<IObjectManager>())
            {
                Task.Run(async () =>
                {
                    await AuditExamples.QueryAdminAudits(logMgr, objMgr);
                }).Wait();
            }
            Pause();


            //FindLastLoginAllUsers.findLastLogin();
            //Pause();
            //FindInactiveUsers30Days.findInactives();
            //Pause();
            //FindErrors.findAllErrors();
            //Pause();
            //FindAllActionsByUserName.findAllActions(args[0]);
            //Pause();
            //Top100RecordsAdminLevel.ReturnTop100Audits();
        }

        public static void Pause()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }
    }
}
