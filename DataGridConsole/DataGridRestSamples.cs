﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace DataGridConsole
{
    /// <summary>
    /// This class contains static methods for querying DataGrid via the DataGrid REST API.
    /// </summary>
    public static class DataGridRestSamples
    {
        /// <summary>
        /// Prints the first 100 audit records at the instance level.
        /// </summary>
        /// <param name="client"></param>
        public static void PrintAuditRecords(RelativityHttpClient client)
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

            string query = "*";
            List<string> fields = new List<string> {};
            string filterQuery = BuildFilterQuery(query, fields);


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
                //JArray listOfResults = JArray.FromObject(results["Data"]);
                //Console.WriteLine($"Total: {listOfResults.Count}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }


        /// <summary>
        /// Prints out a JSON representation of the specified audit record ID.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="recordId"></param>
        public static void PrintSingleAuditRecord(RelativityHttpClient client, int recordId)
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


        /// <summary>
        /// Prints users who logged in in the last N days
        /// </summary>
        /// <param name="client"></param>
        /// <param name="lastNumDays"></param>
        public static void PrintUsersLoggedInRecently(RelativityHttpClient client, int lastNumDays)
        {
            // declare query parameters
            const int workspaceId = -1;
            const string requestUri =
                "/Relativity.REST/api/kCura.AuditUI2.Services.AuditLog.IAuditLogModule/Audit%20Log%20Manager/GetAuditLogItemsAsync";
            const int itemsPerPage = 50;
            const int pageNumber = 1;
            const string sortBy = "TimeStamp";
            const string sortOrder = "desc";
            const bool includeDetails = false;
            const bool includeOldNewValues = false;

            string query = "*";
            List<string> fields = new List<string> {  };

            // filter for last N days
            JObject filter = new JObject
            {

                ["range"] = new JObject
                {
                    ["TimeStamp"] = new JObject
                    {
                        { "gte", "2018-01-14T00:00:00.000Z" }
                    }
                }

                //["and"] = new JArray
                //{
                //    new JObject
                //    {
                //        ["or"] = new JArray
                //        {
                //            new JObject
                //            {
                //                //["range"] = new JObject
                //                //{
                //                //    ["TimeStamp"] = new JObject
                //                //    {
                //                //        { "gte", "2018-01-14T00:00:00.000Z" }
                //                //    }
                //                //}
                //            }
                //        }
                //    } 
                //}
            };

            string filterQuery = BuildFilterQuery(query, fields, filter);

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
                //JArray listOfResults = JArray.FromObject(results["Data"]);
                //Console.WriteLine($"Total: {listOfResults.Count}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        /// <summary>
        /// Builds out a JSON object representing the filters/queries converted to string
        /// </summary>
        /// <param name="query"></param>
        /// <param name="fields"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        private static string BuildFilterQuery(string query, IEnumerable<string> fields, JObject filter = null)
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

            if (filter == null)
            {
                filter = new JObject();
            }

            JObject result = new JObject
            {
                ["filtered"] = new JObject
                {
                    ["filter"] = filter,
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
    }
}
