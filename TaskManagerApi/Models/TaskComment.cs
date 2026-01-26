namespace TaskManagerApi.Models;

/// <summary>
/// Represents a comment or note on a task.
/// </summary>
public class TaskComment
{
    /// <summary>
    /// Unique identifier for the comment.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// The comment text content.
    /// </summary>
    public string Content { get; set; } = string.Empty;
    
    /// <summary>
    /// Date and time when the comment was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Date and time when the comment was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; }
    
    /// <summary>
    /// ID of the task this comment belongs to.
    /// </summary>
    public Guid TaskId { get; set; }
    
    /// <summary>
    /// Navigation property to the task.
    /// </summary>
    public TaskItem Task { get; set; } = null!;
    
    /// <summary>
    /// ID of the user who created this comment.
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Navigation property to the user who created the comment.
    /// </summary>
    public User User { get; set; } = null!;
    
    /// <summary>
    /// Indicates if the comment has been soft deleted.
    /// </summary>
    public bool IsDeleted { get; set; }
}