namespace DataGridConsole.Filters
{
    /// <summary>
    /// Contains static classes to add typing around strings
    /// </summary>
    public static class Constants
    {
        public static class BoolOps
        {
            public const string And = "and";
            public const string Or = "or";
        }

        public static class Cmp
        {
            /// <summary>
            /// Greater than or equal to
            /// </summary>
            public const string Gte = "gte";

            /// <summary>
            /// Less than or equal to
            /// </summary>
            public const string Lte = "lte";
        }

        public static class EndpointUris
        {
            /// <summary>
            /// 
            /// </summary>
            public const string GetAuditLogItems =
                "/Relativity.REST/api/kCura.AuditUI2.Services.AuditLog.IAuditLogModule/Audit%20Log%20Manager/GetAuditLogItemsAsync";

            public const string GetAuditLogItem =
                "/Relativity.REST/api/kCura.AuditUI2.Services.AuditLog.IAuditLogModule/Audit%20Log%20Manager/GetAuditLogItemAsync";
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
