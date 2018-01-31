using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DataGridConsole.Helpers
{
    /// <summary>
    /// A class for combining filters with ANDs and ORs
    /// </summary>
    public class JoinedFilter : DataGridFilter
    {
        private List<DataGridFilter> _filters = new List<DataGridFilter>();

        private BoolOp _op;


        #region Constructors

        /// <summary>
        /// Joins together two filters
        /// </summary>
        /// <param name="filter1"></param>
        /// <param name="filter2"></param>
        /// <param name="op"></param>
        public JoinedFilter(DataGridFilter filter1, DataGridFilter filter2, BoolOp op)
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
        public JoinedFilter(IEnumerable<DataGridFilter> filters, BoolOp op)
        {
            foreach (DataGridFilter filter in filters)
            {
                _filters.Add(filter);
            }

            _op = op;
        }


        #endregion




        public override JObject GetFilter()
        {
            throw new NotImplementedException();
        }
    }
}
