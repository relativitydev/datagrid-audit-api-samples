using System;
using System.Globalization;
using Newtonsoft.Json.Linq;

namespace DataGridConsole.Filters
{
    /// <summary>
    /// Builds out filters for timestamps
    /// </summary>
    public class TimestampFilter : IRecordFilter
    {
        private readonly Cmp _cmp;

        private DateTime _dt;

        public string FilterType => "Timestamp";

        public TimestampFilter(Cmp cmp, DateTime dateTime)
        {
            _cmp = cmp;
            _dt = dateTime;
        }


        /// <summary>
        /// Returns the string representation of this Timestamp condition.
        /// 'Timestamp' <= 2017-11-23T23:59:00.00Z
        /// </summary>
        /// <returns></returns>
        public string GetCondition()
        {
            const string dateFormat = "yyyy-MM-ddTHH:mm:ss.ffZ";
            string result = String.Empty;
            switch (_cmp)
            {
                // insert the proper comparison operator
                case Cmp.EqTo:
                    result = Concatenate(_dt, Constants.Cmp.EqTo, dateFormat);
                    break;
                case Cmp.Gtn:
                    result = Concatenate(_dt, Constants.Cmp.Gtn, dateFormat);
                    break;
                case Cmp.Gte:
                    result = Concatenate(_dt, Constants.Cmp.Gte, dateFormat);
                    break;
                case Cmp.Lte:
                    result = Concatenate(_dt, Constants.Cmp.Lte, dateFormat);
                    break;
                case Cmp.Ltn:
                    result = Concatenate(_dt, Constants.Cmp.Ltn, dateFormat);
                    break;
                // should never reach default
            }
            return result;
        }


        /// <summary>
        /// Formats the condition as a string
        /// (e.g. 'Timestamp' <= 2017-11-23T23:59:00.00Z) 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="cmp"></param>
        /// <param name="dateFormat"></param>
        /// <returns></returns>
        private string Concatenate(DateTime dt, string cmp, string dateFormat)
        {
            return $"'{FilterType}' {cmp} {dt.ToString(dateFormat, CultureInfo.InvariantCulture)}";
        }
    }
}
