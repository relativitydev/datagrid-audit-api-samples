using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace DataGridConsole
{
    public static class QueryHelper
    {
        /// <summary>
        /// Queries for the Artifact ID of the choice with the give name
        /// </summary>
        /// <param name="client"></param>
        /// <param name="choiceName"></param>
        /// <returns></returns>
        public static int QueryForChoiceId(RelativityHttpClient client, 
            string choiceName, 
            int workspaceId)
        {
            int choiceArtifactId = -1;
            JObject payload = new JObject
            {
                ["condition"] = $"'Name' == '{choiceName}'",
                ["fields"] = new JArray()
            };

            try
            {
                JObject response = client.Post(Filters.Constants.EndpointUris.QueryChoices, payload);
                // see if we get a result
                int count = response["ResultCount"].ToObject<int>();
                if (count > 0)
                {
                    JArray choices = (JArray) response["Results"];
                    JToken firstOrDef = choices.FirstOrDefault();
                    // cast to JObject
                    JObject choice = (JObject) firstOrDef;
                    if (choice != null)
                    {
                        choiceArtifactId = choice["Artifact ID"].ToObject<int>();
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return choiceArtifactId;
        }
    }
}
