using Newtonsoft.Json.Linq;

namespace DataGridConsole.Filters
{

    /// <summary>
    /// Interface for DataGrid filters
    /// </summary>
    public interface IDataGridFilter
    {
        /// <summary>
        /// The type of filter we are using i.e. the field we are filtering on.
        /// This is needed for one of the JSON parameters.
        /// </summary>
        string FilterType { get; }


        /// <summary>
        /// Returns the JObject representation of this filter that can be consumed in a REST call
        /// </summary>
        /// <returns></returns>
        JObject GetFilter();
    }
}
