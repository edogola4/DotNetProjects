using TaskManagerApi.Constants;

namespace TaskManagerApi.Models;

/// <summary>
/// Parameters for filtering and sorting tasks.
/// </summary>
public class TaskFilterParameters : PaginationParameters
{
    /// <summary>
    /// Search term for title and description.
    /// </summary>
    public string? Search { get; set; }
    
    /// <summary>
    /// Filter by completion status.
    /// </summary>
    public bool? IsCompleted { get; set; }
    
    /// <summary>
    /// Filter by priority level.
    /// </summary>
    public Priority? Priority { get; set; }
    
    /// <summary>
    /// Filter by category ID.
    /// </summary>
    public Guid? CategoryId { get; set; }
    
    /// <summary>
    /// Filter by tags (comma-separated).
    /// </summary>
    public string? Tags { get; set; }
    
    /// <summary>
    /// Filter by due date from.
    /// </summary>
    public DateTime? DueDateFrom { get; set; }
    
    /// <summary>
    /// Filter by due date to.
    /// </summary>
    public DateTime? DueDateTo { get; set; }
    
    /// <summary>
    /// Sort field.
    /// </summary>
    public string SortBy { get; set; } = SortingConstants.DefaultSortBy;
    
    /// <summary>
    /// Sort order.
    /// </summary>
    public string SortOrder { get; set; } = SortingConstants.DefaultSortOrder;
}
