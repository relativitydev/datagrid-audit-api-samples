﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DataGridConsole
{
    /// <summary>
    /// This class contains static methods for querying DataGrid via the DataGrid REST API.
    /// </summary>
    public static class RestSamples
    {
        
        public static void GetAuditRecords(DataGridClient client)
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
            const string filterQuery =
                "{\"filtered\":{\"filter\":{},\"query\":{\"query_string\":{\"query\":\"\\\"*\\\"\",\"fields\":[\"Details.*\"]}}}}";

            // build out JSON Object as payload
            JObject payload = new JObject();
            payload["workspaceId"] = workspaceId;
            payload["request"] = new JObject
            {
                { "itemsPerPage", itemsPerPage },
                { "pageNumber", pageNumber },
                { "sortBy", sortBy },
                { "sortOrder", sortOrder },
                { "includeDetails", includeDetails },
                { "includeOldNewValues", includeOldNewValues },
                { "filterQuery", filterQuery }
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
