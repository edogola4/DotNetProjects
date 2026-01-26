using TaskManagerApi.Models;

namespace TaskManagerApi.DTOs;

/// <summary>
/// DTO for task response data.
/// </summary>
public class TaskResponseDto
{
    /// <summary>
    /// Unique task identifier.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Task title.
    /// </summary>
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// Task description.
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Task due date (optional).
    /// </summary>
    public DateTime? DueDate { get; set; }
    
    /// <summary>
    /// Whether the task is completed.
    /// </summary>
    public bool IsCompleted { get; set; }
    
    /// <summary>
    /// Task priority level.
    /// </summary>
    public Priority Priority { get; set; }
    
    /// <summary>
    /// Current task status.
    /// </summary>
    public Models.TaskStatus Status { get; set; }
    
    /// <summary>
    /// Associated category ID (optional).
    /// </summary>
    public Guid? CategoryId { get; set; }
    
    /// <summary>
    /// Category name (if assigned).
    /// </summary>
    public string? CategoryName { get; set; }
    
    /// <summary>
    /// List of associated tags.
    /// </summary>
    public List<TagResponseDto> Tags { get; set; } = new();
    
    /// <summary>
    /// Estimated time in hours.
    /// </summary>
    public decimal? EstimatedHours { get; set; }
    
    /// <summary>
    /// Actual time spent in hours.
    /// </summary>
    public decimal? ActualHours { get; set; }
    
    /// <summary>
    /// Date and time when completed.
    /// </summary>
    public DateTime? CompletedAt { get; set; }
    
    /// <summary>
    /// Username of who completed the task.
    /// </summary>
    public string? CompletedByUsername { get; set; }
    
    /// <summary>
    /// Number of comments on this task.
    /// </summary>
    public int CommentCount { get; set; }
    
    /// <summary>
    /// Number of attachments on this task.
    /// </summary>
    public int AttachmentCount { get; set; }
    
    /// <summary>
    /// Task creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Last update timestamp.
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}
