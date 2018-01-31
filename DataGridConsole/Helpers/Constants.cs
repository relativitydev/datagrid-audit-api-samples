using System;

namespace DataGridConsole.Helpers
{
    /// <summary>
    /// Contains static classes to add typing around strings
    /// </summary>
    public static class Constants
    {
        public static class BoolOps
        {
            public static string And = "and";
            public static string Or = "or";
        }

        public static class Cmp
        {
            /// <summary>
            /// Greater than or equal to
            /// </summary>
            public static string Gte = "gte";

            /// <summary>
            /// Less than or equal to
            /// </summary>
            public static string Lte = "lte";
        }

        internal static class CmpType
        {
            /// <summary>
            /// If we are filtering based on a discrete set of values, we specify terms
            /// </summary>
            internal static string Terms = "terms";

            /// <summary>
            /// If we are filtering based on a continuous spectrum of values (such as time), then we specify a range
            /// </summary>
            internal static string Range = "range";
        }
    }
}
