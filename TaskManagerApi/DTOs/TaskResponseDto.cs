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
    /// Associated category ID (optional).
    /// </summary>
    public Guid? CategoryId { get; set; }
    
    /// <summary>
    /// Task creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Last update timestamp.
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}
