namespace DataGridConsole.Filters
{
    public enum BoolOp
    {
        And,
        Or
    }

    public enum Cmp
    {
        /// <summary>
        /// Equal to
        /// </summary>
        EqTo,

        /// <summary>
        /// Greater than or equal to
        /// </summary>
        Gte,

        /// <summary>
        /// Less than or equal to
        /// </summary>
        Lte,

        /// <summary>
        /// Greater than
        /// </summary>
        Gtn,

        /// <summary>
        /// Less than
        /// </summary>
        Ltn
    }


    /// <summary>
    /// Determines sort order
    /// </summary>
    public enum SortOrder
    {
        Asc,
        Desc
    }

}
