using System;
using System.Globalization;
using Newtonsoft.Json.Linq;

namespace DataGridConsole.Filters
{
    /// <summary>
    /// Builds out filters for timestamps
    /// </summary>
    public class TimestampFilter : IDataGridFilter
    {
        private readonly Cmp _cmp;

        private DateTime _dt;

        public string FilterType => "TimeStamp";

        public TimestampFilter(Cmp cmp, DateTime dateTime)
        {
            _cmp = cmp;
            _dt = dateTime;
        }


        /// <summary>
        /// Returns the JObject representation of this Timestamp filter.
        /// </summary>
        /// <returns></returns>
        public JObject GetFilter()
        {
            // Model for JSON query

            // new JObject
            // {
            //      ["range"] = new JObject
            //      {
            //            ["TimeStamp"] = new JObject
            //            {
            //                 { "gte", "2018-01-14T00:00:00.000Z" }
            //            }
            //      }
            // }

            const string dateFormat = "yyyy-MM-ddTHH:mm:ss.fffZ";
            JObject result = new JObject
            {
                [Constants.CmpType.Range] = new JObject
                {
                    [FilterType] = new JObject()
                }
            };
            switch (_cmp)
            {
                case Cmp.Gte:
                    result[Constants.CmpType.Range][FilterType][Constants.Cmp.Gte] = 
                        _dt.ToString(dateFormat, CultureInfo.InvariantCulture);
                    break;
                case Cmp.Lte:
                    result[Constants.CmpType.Range][FilterType][Constants.Cmp.Lte] = 
                        _dt.ToString(dateFormat, CultureInfo.InvariantCulture);
                    break;
                // should never reach default
            }
            return result;
        }
    }
}
