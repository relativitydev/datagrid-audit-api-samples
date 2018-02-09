using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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


        #endregion

        /// <summary>
        /// Returns the condition as such:
        /// 'Action' IN [12345, 12356]
        /// </summary>
        /// <returns></returns>
        public string GetCondition()
        {
            return $"'{FilterType}' IN CHOICE {ConcatenateIds()}";
        }

        #region Private methods

        private string ConcatenateIds()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            for (int i = 0; i < _actionIds.Count; i++)
            {
                int id = _actionIds[i];
                if (i == _actionIds.Count - 1)
                {
                    // if we are the last one, we do not want to add a comma
                    sb.AppendFormat("{0}", id);
                }
                else
                {
                    sb.AppendFormat("{0},", id);
                }               
            }
            sb.Append("]");
            return sb.ToString();
        }

        #endregion
    }
}
