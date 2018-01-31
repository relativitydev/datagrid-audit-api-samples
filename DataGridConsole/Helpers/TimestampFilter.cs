using System;
using Newtonsoft.Json.Linq;

namespace DataGridConsole.Helpers
{
    /// <summary>
    /// Builds out filters for timestamps
    /// </summary>
    public class TimestampFilter : DataGridFilter
    {
        private readonly Cmp _cmp;

        private DateTime _dt;

        public TimestampFilter(Cmp cmp, DateTime dateTime)
        {
            _cmp = cmp;
            _dt = dateTime;
            FilterType = "TimeStamp";
        }

        public override JObject GetFilter()
        {
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
                        result[Constants.CmpType.Range][FilterType][Constants.Cmp.Gte] = _dt.ToLongTimeString();
                        break;
                    case Cmp.Lte:
                        result[Constants.CmpType.Range][FilterType][Constants.Cmp.Lte] = _dt.ToLongTimeString();
                        break;
            }
            return result;
        }
    }
}
