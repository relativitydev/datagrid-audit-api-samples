using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace DataGridConsole.Helpers
{

    /// <summary>
    /// Base abstract class for DataGrid filteres. Can nest filters inside if it is composite.
    /// </summary>
    public abstract class DataGridFilter
    {
        /// <summary>
        /// The type of filter we are using i.e. the field we are filtering on
        /// </summary>
        protected static string FilterType;


        /// <summary>
        /// Returns the JObject representation of this filter that can be consumed in a REST call
        /// </summary>
        /// <returns></returns>
        public abstract JObject GetFilter();
    }
}
