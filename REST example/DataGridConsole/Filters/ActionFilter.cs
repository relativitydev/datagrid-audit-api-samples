using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace DataGridConsole.Filters
{
    public class ActionFilter : IRecordFilter
    {
        /// <summary>
        /// Specifies the type as "Action". Needed as a JSON parameter.
        /// </summary>
        public string FilterType => "Action";

        /// <summary>
        /// Stores the IDs of the actions we want to filter on
        /// </summary>
        private readonly List<int> _actionIds;

        /// <summary>
        /// Stores the names of the actions we want to filter on
        /// </summary>
        private readonly List<string> _actionNames;

        /// <summary>
        /// Indicates if we are filtering on names (strings) rather than IDs
        /// </summary>
        private readonly bool _useNames = false;

        /// <summary>
        /// Points to a static dictionary to look up action IDs by name. This assumes
        /// that the IDs of the actions do not change frequently. Otherwise, it would
        /// be better to query for the IDs dynamically and cache them.
        /// </summary>
        private Dictionary<string, int> _actionIdLookup;

        #region Constructors

        public ActionFilter(int actionId)
        {
            _actionIds = new List<int>
            {
                actionId
            };
        }

        public ActionFilter(IEnumerable<int> actionIds)
        {
            _actionIds = actionIds.ToList();
        }


        public ActionFilter(string actionName)
        {
            _actionNames = new List<string>
            {
                actionName
            };
            _useNames = true;
            PopulateLookup();
        }

        public ActionFilter(IEnumerable<string> actionNames)
        {
            _actionNames = actionNames.ToList();
            _useNames = true;
            PopulateLookup();
        }

        #endregion


        public JObject GetCondition()
        {
            // initialize a JSON like:
            // { "terms": { "Action": [] } } 
            var result = new JObject
            {
                [Constants.CmpType.Terms] = new JObject
                {
                    [FilterType] = new JArray()
                }
            };

            switch (_useNames)
            {
                // insert IDs into Action JArray
                case true:
                    // need to lookup IDs
                    IEnumerable<int> actionIds = _actionNames.Select(x => _actionIdLookup[x]);
                    result[Constants.CmpType.Terms][FilterType] = JArray.FromObject(actionIds);
                    break;
                case false:
                    result[Constants.CmpType.Terms][FilterType] = JArray.FromObject(_actionIds);
                    break;
            }

            return result;
        }

        #region Private methods

        /// <summary>
        /// Populates the dictionary for looking up action IDs if we are using names.
        /// These were found by looking at the "Details" column of each audit record.
        /// </summary>
        private void PopulateLookup()
        {
            _actionIdLookup = new Dictionary<string, int>
            {
                ["Create"] = 2,
                ["Update"] = 3,
                ["Delete"] = 9,
                ["Login"] = 25,
                ["Logout"] = 26,
                ["Query"] = 29
                // etc. 
            };           
        }

        #endregion
    }
}
