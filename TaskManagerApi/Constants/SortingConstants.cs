namespace TaskManagerApi.Constants;

/// <summary>
/// Constants for sorting and filtering operations.
/// </summary>
public static class SortingConstants
{
    /// <summary>
    /// Default sort field for tasks.
    /// </summary>
    public const string DefaultSortBy = "createdAt";

    /// <summary>
    /// Default sort order.
    /// </summary>
    public const string DefaultSortOrder = "desc";

    /// <summary>
    /// Valid sort fields for tasks.
    /// </summary>
    public static class TaskSortFields
    {
        public const string Title = "title";
        public const string DueDate = "duedate";
        public const string Priority = "priority";
        public const string CreatedAt = "createdAt";
    }

    /// <summary>
    /// Valid sort orders.
    /// </summary>
    public static class SortOrders
    {
        public const string Ascending = "asc";
        public const string Descending = "desc";
    }
}