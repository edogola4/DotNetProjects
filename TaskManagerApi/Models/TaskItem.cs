using System.ComponentModel;

namespace TaskManagerApi.Models;

/// <summary>
/// Represents a task item in the task management system.
/// </summary>
public class TaskItem
{
    /// <summary>
    /// Unique identifier for the task.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Task title or name.
    /// </summary>
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// Detailed description of the task.
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Optional due date for the task.
    /// </summary>
    public DateTime? DueDate { get; set; }
    
    /// <summary>
    /// Indicates whether the task has been completed.
    /// </summary>
    public bool IsCompleted { get; set; }
    
    /// <summary>
    /// Priority level of the task.
    /// </summary>
    public Priority Priority { get; set; } = Priority.Medium;
    
    /// <summary>
    /// Date and time when the task was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Date and time when the task was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; }
    
    /// <summary>
    /// ID of the user who owns this task.
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Navigation property to the user who owns this task.
    /// </summary>
    public User User { get; set; } = null!;
    
    /// <summary>
    /// Optional category ID for organizing tasks.
    /// </summary>
    public Guid? CategoryId { get; set; }
    
    /// <summary>
    /// Navigation property to the task's category.
    /// </summary>
    public Category? Category { get; set; }
    
    /// <summary>
    /// Collection of tags associated with this task.
    /// </summary>
    public ICollection<Tag> Tags { get; set; } = new List<Tag>();
}

/// <summary>
/// Enumeration of task priority levels.
/// </summary>
public enum Priority
{
    /// <summary>
    /// Low priority task.
    /// </summary>
    [Description("Low priority - can be done when time permits")]
    Low = 0,
    
    /// <summary>
    /// Medium priority task (default).
    /// </summary>
    [Description("Medium priority - normal importance level")]
    Medium = 1,
    
    /// <summary>
    /// High priority task.
    /// </summary>
    [Description("High priority - important and should be done soon")]
    High = 2,
    
    /// <summary>
    /// Urgent priority task.
    /// </summary>
    [Description("Urgent priority - critical and needs immediate attention")]
    Urgent = 3
}
