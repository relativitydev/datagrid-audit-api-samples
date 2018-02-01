using System;
using System.Collections.Generic;
using kCura.Relativity.Client;
using Relativity.Services.ServiceProxy;
using kCura.AuditUI2.Services.AuditLog;
using Newtonsoft.Json.Linq;
using UsernamePasswordCredentials = kCura.Relativity.Client.UsernamePasswordCredentials;

namespace DataGridAuditAPISamples
{
    class ConnectionHelper
    {
        //Constants for building the Audit Data Request
        public const int itemsPerPage = 100000;
        public const int pageNumber = 1;
        public const string sortBy = "TimeStamp";
        public const string sortOrder = "desc";
        public const bool includeDetails = false;
        public const bool includeOldNewValues = false;
        public const string _userName = "YOURRELATIVITYUSERNAME"; /*YOURUSERNAMEHERE*/
        public const string _password = "YOURRELATIVITYPASSWORD";/*YOURPASSWORDHERE*/
        public const string BaseRelativityURL = "YOURRELATIVITYURL"; /*EX: https://MySite.relativity.com/Relativity */
        public const int WorkspaceID = -1;/*TARGET WORKSPACE HERE*/

        public static Uri WebApiUri => new Uri(BaseRelativityURL + "webapi/");
        public static Uri ServicesUri => new Uri(BaseRelativityURL + ".Services");
        public static Uri RestUri => new Uri(BaseRelativityURL + ".REST/api");

        //Method for setting the properties of the Audit Data Request
        public static void GetValue(AuditLogDataRequest newRequest)
        {
            newRequest.SortBy = ConnectionHelper.sortBy;
            newRequest.SortOrder = ConnectionHelper.sortOrder;
            newRequest.PageNumber = ConnectionHelper.pageNumber;
            newRequest.ItemsPerPage = ConnectionHelper.itemsPerPage;
            newRequest.IncludeDetails = ConnectionHelper.includeDetails;
            newRequest.IncludeOldNewValues = ConnectionHelper.includeOldNewValues;
            newRequest.IncludeDetails = ConnectionHelper.includeDetails;
        }

        //Method for creating the Audit Data Request
        public static AuditLogDataRequest AuditLogDataRequest
        {
            get
            {
                List<int> workspaceIds = new List<int> { -1 };
                string query = "*";
                var fields = new List<string>();
                string filterQuery = ConnectionHelper.BuildFilterQuery(query, fields);
                AuditLogDataRequest newRequest = new AuditLogDataRequest();
                newRequest.Workspaces = workspaceIds;
                newRequest.FilterQuery = filterQuery;
                GetValue(newRequest);
                return newRequest;
            }
        }

        //Method for generating Audit Data Request Query
        public static string BuildFilterQuery(string query, IEnumerable<string> fields)
        {
            // JSON structure:
            // { 
            //   "filtered": { 
            //     "filter": {},
            //     "query": {
            //       "query_string": {
            //         "query": "<some query>",
            //         "fields": ["Field1", "Field2"]
            //       }
            //     }       
            //   }
            // }
            JObject result = new JObject
            {
                ["filtered"] = new JObject
                {
                    ["filter"] = new JObject(),
                    ["query"] = new JObject
                    {
                        ["query_string"] = new JObject
                        {
                            {"query", query},
                            {"fields", JArray.FromObject(fields)}
                        }
                    }
                }
            };

            return result.ToString();
        }

        //Instantiates RSAPI proxy
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

        //Instantiates Services
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
