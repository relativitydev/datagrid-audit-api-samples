using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace DataGridConsole.Filters
{
    public class ActionFilter : IDataGridFilter
    {
        /// <summary>
        /// Specifies the type as "Action". Needed as a JSON parameter.
        /// </summary>
        public string FilterType => "Action";

        private List<int> _actionIds;

        private List<string> _actionNames;

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
        }

        public ActionFilter(IEnumerable<string> actionNames)
        {
            _actionNames = actionNames.ToList();
        }

        #endregion


        public JObject GetFilter()
        {
            var result = new JObject();

            return result;
        }


    }
}
