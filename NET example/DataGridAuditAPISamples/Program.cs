using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGridAuditAPISamples
{
    class Program
    {
        static void Main(string[] args)
        {
           
            Console.WriteLine("##########################################");
            Console.WriteLine("#       DATAGRID AUDIT API SAMPLES       #");
            Console.WriteLine("#           ORDER OF SAMPLES:            #");
            Console.WriteLine("#  1. Find Last Login Date of All Users  #");
            Console.WriteLine("#  2. Find All Errors in Audit           #");
            Console.WriteLine("#  3. Find All Actions by Specific User  #");
            Console.WriteLine("#  4. Find the 100 Most Recent Audits    #");
            Console.WriteLine("##########################################\r\n");

           Pause();
         FindInactiveUsers.findInactives();
           Pause();
         FindErrors.findAllErrors();
           Pause();
         FindAllActionsByUserName.findAllActions(args[0]); /*This has the Relativity Admin account entered in the build options */
           Pause();
         Top100RecordsAdminLevel.ReturnTop100Audits();
        }

        public static void Pause()
        {
            Console.Write("Press any key to continue . . . \r\n \r\n");
            Console.Write("");
            Console.ReadKey(true);
        }
    }
}
