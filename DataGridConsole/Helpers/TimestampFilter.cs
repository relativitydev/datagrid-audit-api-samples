using System;
using System.Globalization;
using Newtonsoft.Json.Linq;

namespace DataGridConsole.Helpers
{
    /// <summary>
    /// Builds out filters for timestamps
    /// </summary>
    public class TimestampFilter : RangeFilter
    {
        private DateTime _dt;

        public TimestampFilter(Cmp cmp, DateTime dateTime)
        {
            Cmp = cmp;
            _dt = dateTime;
            FilterType = "TimeStamp";
        }


        /// <summary>
        /// Returns the JObject representation of this Timestamp filter.
        /// </summary>
        /// <returns></returns>
        public override JObject GetFilter()
        {
            // Model for JSON query (the "and" and "or" arrays can
            // be safely ommitted):

            // ["and"] = new JArray
            // {
            //     new JObject
            //     {
            //         ["or"] = new JArray
            //         {
            //             new JObject
            //             {
            //                 ["range"] = new JObject
            //                 {
            //                     ["TimeStamp"] = new JObject
            //                     {
            //                         { "gte", "2018-01-14T00:00:00.000Z" }
            //                     }
            //                 }
            //             }
            //         }
            //     } 
            // }
            const string dateFormat = "yyyy-MM-ddTHH:mm:ss.fffZ";
            JObject result = new JObject
            {
                [Constants.CmpType.Range] = new JObject
                {
                    [FilterType] = new JObject()
                }
            };
            switch (Cmp)
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
