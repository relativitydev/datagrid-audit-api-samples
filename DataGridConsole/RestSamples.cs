using System;
using Newtonsoft.Json.Linq;

namespace DataGridConsole
{
    /// <summary>
    /// This class contains static methods for querying DataGrid via the DataGrid REST API.
    /// </summary>
    public static class RestSamples
    {
        
        public static void PrintAuditRecords(DataGridClient client)
        {
            // declare query parameters
            const int workspaceId = -1;
            const string requestUri =
                "/Relativity.REST/api/kCura.AuditUI2.Services.AuditLog.IAuditLogModule/Audit%20Log%20Manager/GetAuditLogItemsAsync";
            const int itemsPerPage = 100;
            const int pageNumber = 1;
            const string sortBy = "TimeStamp";
            const string sortOrder = "desc";
            const bool includeDetails = false;
            const bool includeOldNewValues = false;
            string filterQuery =
                // { 
                //   "filtered": { 
                //     "filter": {},
                //     "query": {
                //       "query_string": {
                //         "query": "\*\",
                //         "fields": ["Details.*"]
                //       }
                //     }       
                //   }
                // }
                "{\"filtered\":{\"filter\":{},\"query\":{\"query_string\":{\"query\":\"\\\"*\\\"\",\"fields\":[\"Details.*\"]}}}}";



            // build out JSON Object as payload
            JObject payload = new JObject
            {
                ["workspaceId"] = workspaceId,
                ["request"] = new JObject
                {
                    {"itemsPerPage", itemsPerPage},
                    {"pageNumber", pageNumber},
                    {"sortBy", sortBy},
                    {"sortOrder", sortOrder},
                    {"includeDetails", includeDetails},
                    {"includeOldNewValues", includeOldNewValues},
                    {"filterQuery", filterQuery}
                }
            };

            try
            {
                JObject results = client.Post(requestUri, payload);
                Console.WriteLine(results.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }

        public static void PrintSingleAuditRecord(DataGridClient client, int recordId)
        {            
            const string requestUri =
                "/Relativity.REST/api/kCura.AuditUI2.Services.AuditLog.IAuditLogModule/Audit%20Log%20Manager/GetAuditLogItemAsync";
            JObject payload = new JObject
            { 
                ["workspaceId"] = -1, // use admin workspace
                ["request"] = new JObject
                {
                    {"Id", recordId}
                }
            };
            
            try
            {
                JObject results = client.Post(requestUri, payload);
                Console.WriteLine(results.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
