using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace DataGridConsole.Filters
{
    public class ActionFilter : DataGridFilter
    {
        private List<int> _actionIds;

        private List<string> _actionNames;


        #region Constructors

        public ActionFilter(IEnumerable<int> actionIds)
        {
            SetType();
            _actionIds = actionIds.ToList();
        }

        public ActionFilter(IEnumerable<string> actionNames)
        {
            SetType();
            _actionNames = actionNames.ToList();
        }

        #endregion

        public override JObject GetFilter()
        {
            throw new NotImplementedException();
        }

        private void SetType()
        {
            FilterType = "Action";
        }
    }
}
