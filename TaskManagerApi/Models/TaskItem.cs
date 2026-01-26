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
    /// Current status of the task.
    /// </summary>
    public TaskStatus Status { get; set; } = TaskStatus.NotStarted;
    
    /// <summary>
    /// Indicates whether the task has been completed (computed property).
    /// </summary>
    public bool IsCompleted => Status == TaskStatus.Completed;
    
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
    
    /// <summary>
    /// Collection of comments on this task.
    /// </summary>
    public ICollection<TaskComment> Comments { get; set; } = new List<TaskComment>();
    
    /// <summary>
    /// Collection of file attachments on this task.
    /// </summary>
    public ICollection<TaskAttachment> Attachments { get; set; } = new List<TaskAttachment>();
    
    /// <summary>
    /// Estimated time to complete the task in hours.
    /// </summary>
    public decimal? EstimatedHours { get; set; }
    
    /// <summary>
    /// Actual time spent on the task in hours.
    /// </summary>
    public decimal? ActualHours { get; set; }
    
    /// <summary>
    /// Date and time when the task was completed.
    /// </summary>
    public DateTime? CompletedAt { get; set; }
    
    /// <summary>
    /// ID of the user who completed the task.
    /// </summary>
    public Guid? CompletedByUserId { get; set; }
    
    /// <summary>
    /// Navigation property to the user who completed the task.
    /// </summary>
    public User? CompletedByUser { get; set; }
    
    /// <summary>
    /// Indicates if the task has been soft deleted.
    /// </summary>
    public bool IsDeleted { get; set; }
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
