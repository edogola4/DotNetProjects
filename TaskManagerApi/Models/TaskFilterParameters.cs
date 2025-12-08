namespace TaskManagerApi.Models;

public class TaskFilterParameters : PaginationParameters
{
    public string? Search { get; set; }
    public bool? IsCompleted { get; set; }
    public Priority? Priority { get; set; }
    public int? CategoryId { get; set; }
    public string? Tags { get; set; }
    public DateTime? DueDateFrom { get; set; }
    public DateTime? DueDateTo { get; set; }
    public string SortBy { get; set; } = "createdAt";
    public string SortOrder { get; set; } = "desc";
}
