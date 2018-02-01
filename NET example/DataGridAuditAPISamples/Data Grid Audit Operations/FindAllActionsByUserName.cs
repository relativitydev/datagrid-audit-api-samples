using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using kCura.AuditUI2.Services.AuditLog;

namespace DataGridAuditAPISamples
{
    public class FindAllActionsByUserName : IAuditLogManager
    {
        public static void findAllActions(string userAuditName)
        {
            ConnectionHelper cmr = new ConnectionHelper();
            var factory = cmr.GetServiceFactory();

            try
            {

                using (IAuditLogManager auditLogManager = factory.CreateProxy<IAuditLogManager>())
                {
                    //Generate a new request using helper method
                    var newRequest = ConnectionHelper.AuditLogDataRequest;
                    //Submit request & return results.
                    IList<AuditLogItem> results = auditLogManager.GetAuditLogItemsAsync(newRequest).Result.Data;
                    IList<AuditLogItem> userStorage = new List<AuditLogItem>();

                    foreach (var item in results)
                    {
                        var test = item.UserName;
                        if (test == userAuditName)
                        {
                            userStorage.Add(item);
                        }
                    }
                    foreach (var userResults in userStorage)
                    {
                        Console.WriteLine("Audit Entry: " + userResults.ID);
                        Console.WriteLine(userResults);
                        Console.WriteLine("");
                    }
                    Console.WriteLine("The total count of audits by user: " + userAuditName + " is: " + userStorage.Count + "\r\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
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
