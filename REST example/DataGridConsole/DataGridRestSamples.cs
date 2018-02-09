using System;
using System.Collections.Generic;
using DataGridConsole.Filters;
using Newtonsoft.Json.Linq;

namespace DataGridConsole
{
    /// <summary>
    /// This class contains static methods for querying DataGrid via the DataGrid REST API.
    /// </summary>
    public static class DataGridRestSamples
    {
        #region Examples
        /// <summary>
        /// Prints the first 100 audit records at the instance level.
        /// Sorts by Timestamp descending.
        /// </summary>
        /// <param name="client"></param>
        public static void PrintAuditRecords(RelativityHttpClient client)
        {
            // declare workspace ID
            // -1 for admin-level audits
            const int workspaceId = -1;

            // insert workspaceId into URL
            string requestUri =
                String.Format(Constants.EndpointUris.QueryAudits, workspaceId);

            // items per page
            const int length = 100;

            // index of first artifact in result set
            const int start = 0;

            // add any fields
            var fields = new List<string>
            {
                "Audit ID",
                "Timestamp",
                "Object Name",
                "Action",
                "Execution Time (ms)",
                "Object ArtifactID",
                "User Name",
                "Field",
                "Old Value",
                "New Value"
            };

            JArray fieldsAsJArray = BuildFieldsArray(fields);

            // create any sorts
            var sortByTimestamp = new JObject
            {
                ["Direction"] = Constants.SortOrder.Descending,
                ["FieldIdentifier"] = new JObject
                {
                    ["Name"] = "Timestamp"
                }
            };

            // add more sorts if needed
            // ...
            // then add sorts to JArray
            JArray sorts = new JArray
            {
                sortByTimestamp
            };

            // construct payload
            JObject payload = new JObject
            {
                ["artifactType"] = new JObject
                {
                    // can hard-code this to 0, since the URL knows where we are
                    ["descriptorArtifactTypeID"] = 0
                },
                ["query"] = new JObject
                {
                    ["fields"] = fieldsAsJArray,
                    ["condition"] = "",
                    ["rowCondition"] = "",
                    ["sorts"] = sorts
                },
                ["start"] = start,
                ["length"] = length
            };  
            
            try
            {
                JObject results = client.Post(requestUri, payload);
                // print out plain JSON
                Console.WriteLine(results.ToString());
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
        /// <param name="workspaceId"></param>
        /// <param name="recordId"></param>
        public static void PrintSingleAuditRecord(RelativityHttpClient client, int workspaceId, int recordId)
        {
            const string requestUri =
                Constants.EndpointUris.GetAuditLogItem;
            JObject payload = new JObject
            {
                ["workspaceId"] = workspaceId, 
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
            // declare workspace ID
            // -1 for admin-level audits
            const int workspaceId = -1;

            // insert workspaceId into URL
            string requestUri =
                String.Format(Constants.EndpointUris.QueryAudits, workspaceId);

            // items per page
            const int length = 100;

            // index of first artifact in result set
            const int start = 0;

            // add any fields
            var fields = new List<string>
            {
                "Audit ID",
                "Timestamp",
                "User Name"
            };

            JArray fieldsAsJArray = BuildFieldsArray(fields);

            // create any sorts
            var sortByTimestamp = new JObject
            {
                ["Direction"] = Constants.SortOrder.Descending,
                ["FieldIdentifier"] = new JObject
                {
                    ["Name"] = "Timestamp"
                }
            };

            // add more sorts if needed
            // ...
            // then add sorts to JArray
            JArray sorts = new JArray
            {
                sortByTimestamp
            };

            
            // filter for last N days
            TimeSpan timeSpan = new TimeSpan(lastNumDays, 0, 0, 0);
            TimestampFilter timeFilter = new TimestampFilter(Cmp.Gte, DateTime.Now.Subtract(timeSpan));

            // filter for Logins only
            int loginActionId = QueryHelper.QueryForChoiceId(client, "Login", workspaceId);
            ActionFilter actionFilter = new ActionFilter(loginActionId);

            // now combine the two filters
            string combinedFilter = new JoinedFilter(timeFilter, actionFilter, BoolOp.And).GetCondition();

            // construct payload
            JObject payload = new JObject
            {
                ["artifactType"] = new JObject
                {
                    // can hard-code this to 0, since the URL knows where we are
                    ["descriptorArtifactTypeID"] = 0
                },
                ["query"] = new JObject
                {
                    ["fields"] = fieldsAsJArray,
                    ["condition"] = combinedFilter,
                    ["rowCondition"] = "",
                    ["sorts"] = sorts
                },
                ["start"] = start,
                ["length"] = length
            };

            try
            {
                JObject results = client.Post(requestUri, payload);
                JArray listOfResults = JArray.FromObject(results["Results"]);

                // use a hash set to store unique users
                HashSet<string> users = new HashSet<string>();

                foreach (JToken result in listOfResults)
                {
                    JObject obj = (JObject)result;
                    JObject artifact = (JObject) obj["Artifact"];
                    // get the name of the user and the timestamp
                    JArray fieldValPairs = (JArray) artifact["FieldValuePairs"];
                    // the order of the fields should correspond to
                    // the order we specified when adding returned fields
                    string timestampVal = fieldValPairs[1]["Value"].ToObject<string>();
                    Console.WriteLine($"Timestamp: {timestampVal}");
                    string username = fieldValPairs[2]["Value"]["Name"].ToObject<string>();
                    Console.WriteLine($"Username: {username}");
                    users.Add(username);
                    Console.WriteLine("--");
                }

                Console.WriteLine($"Total number of unique users: {users.Count}");
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        #endregion


        #region Private helper methods

        /// <summary>
        /// Puts a collection of field names into a field
        /// </summary>
        /// <param name="fieldNames"></param>
        /// <returns></returns>
        private static JArray BuildFieldsArray(IEnumerable<string> fieldNames)
        {
            JArray fields = new JArray();
            foreach (string field in fieldNames)
            {
                fields.Add( new JObject
                {
                    ["Name"] = field,
                    // can leave GUIDs and 
                    // Artifact IDs as bogus values
                    ["Guids"] = new JArray(),
                    ["ArtifactID"] = 0
                });
            }
            return fields;
        }

        #endregion
    }
}
