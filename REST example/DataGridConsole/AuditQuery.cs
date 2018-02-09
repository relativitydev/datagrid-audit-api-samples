using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataGridConsole.Filters;
using Newtonsoft.Json.Linq;

namespace DataGridConsole
{

    public class Sort
    {
        /// <summary>
        /// Ascending or descending
        /// </summary>
        public SortOrder Direction { get; set; }

        /// <summary>
        /// Name of field on which we are sorting
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// Returns the JObject representation for this sort
        /// </summary>
        /// <returns></returns>
        public JObject GetJObject()
        {
            string direction = String.Empty;;

            switch (Direction)
            {
                case SortOrder.Asc:
                    direction = Constants.SortOrder.Ascending;
                    break;
                case SortOrder.Desc:
                    direction = Constants.SortOrder.Descending;
                    break;
            }

            if (String.IsNullOrEmpty(direction))
            {
                throw new InvalidEnumArgumentException("Direction must be a valid sort order.");
            }

            var sortObj = new JObject
            {
                ["Direction"] = direction,
                ["FieldIdentifier"] = new JObject
                {
                    ["Name"] = FieldName
                }
            };
            return sortObj;
        }
    }

    /// <summary>
    /// Class used to construct JSON payloads
    /// </summary>
    public class AuditQuery
    {
        #region Public Members

        /// <summary>
        /// A collection of fields that we want to return
        /// </summary>
        public IEnumerable<string> Fields { get; set; }

        /// <summary>
        /// Specify any sorts
        /// </summary>
        public IEnumerable<Sort> Sorts { get; set; }

        /// <summary>
        /// Specify any condition we need
        /// </summary>
        public IRecordFilter Condition { get; set; }

        /// <summary>
        /// The number of results we want to return
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// The 0-based index of the "page" of results we want to start on.
        /// </summary>
        public int Start { get; set; }

        #endregion


        /// <summary>
        /// Returns the JObject representation of this query
        /// that RelativityHttpClient can consume
        /// </summary>
        /// <returns></returns>
        public JObject GetJObject()
        {
            // first get the sorts
            IEnumerable<JObject> sorts = Sorts.Select(x => x.GetJObject());
            JArray sortsArr = JArray.FromObject(sorts);

            // convert fields to JArray
            JArray fields = BuildFieldsArray(Fields);

            string condition = String.Empty;
            if (Condition != null)
            {
                condition = Condition.GetCondition();
            }

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
                    ["fields"] = fields,
                    ["condition"] = condition,
                    ["rowCondition"] = "", // have not added this member yet
                    ["sorts"] = sortsArr
                },
                ["start"] = Start,
                ["length"] = Length
            };

            return payload;
        }


        #region Private Methods

        private static JArray BuildFieldsArray(IEnumerable<string> fieldNames)
        {
            JArray fields = new JArray();
            foreach (string field in fieldNames)
            {
                fields.Add(new JObject
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
