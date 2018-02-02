using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using kCura.AuditUI2.Services.AuditLog;

namespace DataGridAuditAPISamples
{
    public class FindErrors : IAuditLogManager
    {
        public static void findAllErrors()
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
                    IList<AuditLogItem> errorStorage = new List<AuditLogItem>();

                    foreach (var item in results)
                    {
                        var actionError = item.ObjectTypeName;
                        if (actionError == "Error")
                        {
                            errorStorage.Add(item);

                        }

                    }
                    foreach (var errorResults in errorStorage)
                    {
                        Console.WriteLine("Audit Entry: " + errorResults.ID);
                        Console.WriteLine(errorResults);
                        Console.WriteLine("");
                    }
                    Console.WriteLine("The total count of audits with a type of Error are: " + errorStorage.Count + "\r\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        #region Methods for IAuditlogManager
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
