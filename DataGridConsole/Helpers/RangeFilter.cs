using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGridConsole.Helpers
{
    /// <summary>
    /// Classifies filters that are ranges (i.e. not discrete) (e.g. Timestamp filter)
    /// </summary>
    public abstract class RangeFilter : DataGridFilter
    {
        protected Cmp Cmp;
    }
}
