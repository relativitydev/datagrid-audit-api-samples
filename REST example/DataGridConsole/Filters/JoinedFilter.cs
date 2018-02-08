using System;
using System.Collections.Generic;
using System.Linq;

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
        public string GetCondition()
        {
            string result = String.Empty;
            // extract conditions from the collection
            IEnumerable<string> filters = _filters.Select(x => x.GetCondition());
            switch (_op)
            {
                case BoolOp.And:
                    result = JoinConditions(filters, Constants.BoolOps.And);
                    break;
                case BoolOp.Or:
                    result = JoinConditions(filters, Constants.BoolOps.Or);
                    break;
                // should never hit default case
            }
            return result;
        }


        /// <summary>
        /// Concatenates the conditions together with "AND" or "OR"
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="boolOp"></param>
        /// <returns></returns>
        private string JoinConditions(IEnumerable<string> conditions, string boolOp)
        {
            List<string> sb = new List<string>();
            foreach (string condition in conditions)
            {
                sb.Add($"({condition})");
            }

            string sep = " " + boolOp + " ";
            return String.Join(sep, sb);
        }
    }
}
