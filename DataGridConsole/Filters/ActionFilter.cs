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

        public ActionFilter(IEnumerable<int> actionIds)
        {;
            _actionIds = actionIds.ToList();
        }

        public ActionFilter(IEnumerable<string> actionNames)
        {
            _actionNames = actionNames.ToList();
        }

        #endregion

        public JObject GetFilter()
        {
            throw new NotImplementedException();
        }


    }
}
