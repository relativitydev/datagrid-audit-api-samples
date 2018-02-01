using System;
using kCura.Relativity.Client;
using Relativity.Services.ServiceProxy;
using UsernamePasswordCredentials = kCura.Relativity.Client.UsernamePasswordCredentials;

namespace DataGridAuditAPISamples
{
    class ConnectionHelper
    {
        
        public static readonly string _userName = "YOURRELATIVITYUSERNAME"; /*YOURUSERNAMEHERE*/
        public static readonly string _password = "YOURRELATIVITYPASSWORD";/*YOURPASSWORDHERE*/
        public static readonly string BaseRelativityURL = "YOURRELATIVITYURL"; /*EX: https://MySite.relativity.com/Relativity */
        public static int WorkspaceID = -1;/*TARGET WORKSPACE HERE*/

        public static Uri WebApiUri => new Uri(BaseRelativityURL + "webapi/");
        public static Uri ServicesUri => new Uri(BaseRelativityURL + ".Services");
        public static Uri RestUri => new Uri(BaseRelativityURL + ".REST/api");




        public static IRSAPIClient GetRsapiClient()
        {
            try
            {
                IRSAPIClient proxy = new RSAPIClient
                    (ServicesUri, new UsernamePasswordCredentials(_userName, _password));
                proxy.APIOptions.WorkspaceID = WorkspaceID;
                return proxy;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to connect to RSAPI. " + ex);
                throw;
            }
        }



        public ServiceFactory GetServiceFactory()
        {
            try
            {
                Relativity.Services.ServiceProxy.UsernamePasswordCredentials credsService =
                    new Relativity.Services.ServiceProxy.UsernamePasswordCredentials(_userName, _password);
                ServiceFactory factory
                    = new ServiceFactory(new ServiceFactorySettings(ServicesUri, RestUri, credsService));
                return factory;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

        }

      
    }
}
