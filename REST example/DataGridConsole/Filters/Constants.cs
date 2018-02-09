namespace DataGridConsole.Filters
{
    /// <summary>
    /// Contains static classes to add typing around strings
    /// </summary>
    public static class Constants
    {
        public static class BoolOps
        {
            public const string And = "AND";
            public const string Or = "OR";
        }

        public static class Cmp
        {
            /// <summary>
            /// Equal to
            /// </summary>
            public const string EqTo = "==";

            /// <summary>
            /// Greater than or equal to
            /// </summary>
            public const string Gte = ">=";

            /// <summary>
            /// Less than or equal to
            /// </summary>
            public const string Lte = "<=";

            /// <summary>
            /// Strictly greater than
            /// </summary>
            public const string Gtn = ">";

            /// <summary>
            /// Strictly less than
            /// </summary>
            public const string Ltn = "<";
        }

        public static class EndpointUris
        {
            /// <summary>
            /// Endpont for querying audits. Need to format with workspace artifact ID.
            /// </summary>
            public const string QueryAudits =
                "/Relativity.Rest/API/Relativity.Objects.Audits/workspaces/{0}/audits/query/";

            public static string QueryWithDetails = QueryAudits + "withdetails";

            public const string GetAuditLogItem =
                "/Relativity.REST/api/kCura.AuditUI2.Services.AuditLog.IAuditLogModule/Audit%20Log%20Manager/GetAuditLogItemAsync";

            public const string QueryChoices =
                "/Relativity.REST/Workspace/{0}/Choice/QueryResult";
        }

        public static class SortOrder
        {
            public const string Ascending = "Ascending";
            public const string Descending = "Descending";
        }


        internal static class CmpType
        {
            /// <summary>
            /// If we are filtering based on a discrete set of values, we specify terms
            /// </summary>
            internal const string Terms = "terms";

            /// <summary>
            /// If we are filtering based on a continuous spectrum of values (such as time), then we specify a range
            /// </summary>
            internal const string Range = "range";
        }
    }
}
