using System;
using System.Collections.Generic;
using System.Configuration;
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

            // read credentials from app.config
            var configReader = new AppSettingsReader();
            string url = configReader.GetValue("RelativityBaseUrl", typeof(string)).ToString();
            string username = configReader.GetValue("RelativityUserName", typeof(string)).ToString();
            string password = configReader.GetValue("RelativityPassword", typeof(string)).ToString();
            string workspaceIdAsStr = configReader.GetValue("WorkspaceId", typeof(string)).ToString();
            int workspaceId = -1;
            if (!String.IsNullOrEmpty(workspaceIdAsStr))
            {
                workspaceId = Int32.Parse(workspaceIdAsStr);
            }

            var connMgr = new ConnectionHelper(url, username, password);
            Console.WriteLine("Successfully read credentials from file.");
            
            Pause();

            // if successful, instantiate services
            ServiceFactory serviceFactory = connMgr.GetServiceFactory();
            using (var logMgr = serviceFactory.CreateProxy<IExternalAuditLogObjectManager>())
            using (var objMgr = serviceFactory.CreateProxy<IObjectManager>())
            {
                Task.Run(async () =>
                {
                    await AuditExamples.QueryAdminAudits(logMgr, objMgr, workspaceId);
                }).Wait();
            }
            Pause();

        }

        public static void Pause()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }
    }
}
