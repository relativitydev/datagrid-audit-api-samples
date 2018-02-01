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
