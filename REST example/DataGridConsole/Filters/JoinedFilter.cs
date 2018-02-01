using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace DataGridConsole.Filters
{
    /// <summary>
    /// A class for combining filters with ANDs and ORs
    /// </summary>
    public class JoinedFilter : IRecordFilter
    {
        /// <summary>
        /// Returns the type of filter. Actually not needed here.
        /// </summary>
        public string FilterType => "JoinedFilter";

        /// <summary>
        /// List of filters we want to combine
        /// </summary>
        private readonly List<IRecordFilter> _filters = new List<IRecordFilter>();

        /// <summary>
        /// Boolean operation with which we want to combine the filters
        /// </summary>
        private BoolOp _op;

        #region Constructors

        /// <summary>
        /// Joins together two filters
        /// </summary>
        /// <param name="filter1"></param>
        /// <param name="filter2"></param>
        /// <param name="op"></param>
        public JoinedFilter(IRecordFilter filter1, IRecordFilter filter2, BoolOp op)
        {
            _filters.Add(filter1);
            _filters.Add(filter2);
            _op = op;
        }


        /// <summary>
        /// Joins together a collection of filters
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="op"></param>
        public JoinedFilter(IEnumerable<IRecordFilter> filters, BoolOp op)
        {
            _filters = filters.ToList();
            _op = op;
        }


        #endregion


        

        /// <summary>
        /// Returns the filters joined as a JSON object
        /// </summary>
        /// <returns></returns>
        public JObject GetFilter()
        {
            var result = new JObject();
            // extract JObjects from the DataGridFilter objects
            IEnumerable<JObject> filters = _filters.Select(x => x.GetFilter());
            JArray filtersJArray = JArray.FromObject(filters);
            switch (_op)
            {
                case BoolOp.And:
                    result[Constants.BoolOps.And] = filtersJArray;
                    break;
                case BoolOp.Or:
                    result[Constants.BoolOps.Or] = filtersJArray;
                    break;
                // should never hit default case
            }
            return result;
        }
    }
}
