using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGridConsole.Helpers
{
    public enum BoolOp
    {
        And,
        Or
    }

    public enum Cmp
    {
        /// <summary>
        /// Greater than or equal to
        /// </summary>
        Gte,

        /// <summary>
        /// Less than or equal to
        /// </summary>
        Lte
    }

    public enum CmpType
    {
        Terms,
        Range
    }
}
