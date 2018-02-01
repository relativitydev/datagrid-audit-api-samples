using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using kCura.AuditUI2.Services.AuditLog;
using Newtonsoft.Json.Linq;

namespace DataGridAuditAPISamples
{
    public class Top100RecordsAdminLevel : IAuditLogManager
    {
        public static void ReturnTop100Audits()
        {
            ConnectionHelper cmr = new ConnectionHelper();
           var factory = cmr.GetServiceFactory();
          
            try
            {

                using (IAuditLogManager auditLogManager = factory.CreateProxy<IAuditLogManager>())
                {

                    List<int> workspaceIds = new List<int>{-1}; /*This is the target workspace to pull audits from*/
                    var itemsPerPage = 100;
                    var pageNumber = 1;
                    const string sortBy = "TimeStamp";
                    const string sortOrder = "desc";
                    const bool includeDetails = false;
                    const bool includeOldNewValues = false;
                    string query = "*";
                    var fields = new List<string>();
                    string filterQuery = BuildFilterQuery(query, fields);
              


                    AuditLogDataRequest newRequest = new AuditLogDataRequest();
               
                    newRequest.SortBy = sortBy;
                    newRequest.SortOrder = sortOrder;
                    newRequest.Workspaces = workspaceIds;
                    newRequest.PageNumber = pageNumber;
                    newRequest.ItemsPerPage = itemsPerPage;
                    newRequest.IncludeDetails = includeDetails;
                    newRequest.FilterQuery = filterQuery;
                    newRequest.IncludeOldNewValues = includeOldNewValues;
                    newRequest.IncludeDetails = includeDetails;
                   
                   
                    IList<AuditLogItem> items = auditLogManager.GetAuditLogItemsAsync(newRequest).Result.Data;
                   
                    foreach(var item in items)
                    {
                        
                        Console.WriteLine("");
                        Console.WriteLine(item);
                        
                    }
                    Console.WriteLine("");
                    Console.WriteLine("The total count of records returned: " + items.Count + "\r\n");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static string BuildFilterQuery(string query, IEnumerable<string> fields)
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

        #region methods for IAuditlogManager
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<DateTime> GetArtifactLastUpdatedAsync(int workspaceId, int artifactId)
        {
            throw new NotImplementedException();
        }

        public Task<AuditLogChartResponse> GetAuditLogForChartAsync(int workspaceId, AuditLogChartRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AuditLogChartResponse> GetAuditLogForChartAsync(AuditLogChartRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AuditLogHistogramResponse> GetAuditLogForHistogramAsync(int workspaceId, AuditLogHistogramRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AuditLogHistogramResponse> GetAuditLogForHistogramAsync(AuditLogHistogramRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AuditLogItem> GetAuditLogItemAsync(int workspaceId, AuditLogItemRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AuditLogItemsResponse> GetAuditLogItemsAsync(int workspaceId, AuditLogDataRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AuditLogItemsResponse> GetAuditLogItemsAsync(AuditLogDataRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<IList<MultiselectFilterOption>> GetMultiselectFilterOptionsAsync(int workspaceId, MultiselectFilterOptionsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<IList<AuditLogChartItem>> GetRelativityObjectsByNamesAsync(int workspaceId, IList<string> request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UseSharedElasticSearchSettingAsync(int workspaceId)
        {
            throw new NotImplementedException();
        }
        #endregion  


        
    }
}
