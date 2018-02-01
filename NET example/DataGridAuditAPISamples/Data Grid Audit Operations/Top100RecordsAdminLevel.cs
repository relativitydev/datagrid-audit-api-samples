using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using kCura.AuditUI2.Services.AuditLog;

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

                    //Generate a new request using helper method
                    var newRequest = ConnectionHelper.AuditLogDataRequest;
                    //Submit request & return results.
                    newRequest.ItemsPerPage = 100;
                    IList<AuditLogItem> items = auditLogManager.GetAuditLogItemsAsync(newRequest).Result.Data;

                    foreach (var item in items)
                    {

                        Console.WriteLine("");
                        Console.WriteLine(item);

                    }
                    Console.WriteLine("");
                    Console.WriteLine("The total count of records returned: " + items.Count + "\r\n");
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
