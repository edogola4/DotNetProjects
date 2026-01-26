namespace TaskManagerApi.Models;

/// <summary>
/// Represents a file attachment on a task.
/// </summary>
public class TaskAttachment
{
    /// <summary>
    /// Unique identifier for the attachment.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Original filename of the attachment.
    /// </summary>
    public string FileName { get; set; } = string.Empty;
    
    /// <summary>
    /// File path or URL where the attachment is stored.
    /// </summary>
    public string FilePath { get; set; } = string.Empty;
    
    /// <summary>
    /// MIME type of the file.
    /// </summary>
    public string ContentType { get; set; } = string.Empty;
    
    /// <summary>
    /// Size of the file in bytes.
    /// </summary>
    public long FileSize { get; set; }
    
    /// <summary>
    /// Date and time when the attachment was uploaded.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// ID of the task this attachment belongs to.
    /// </summary>
    public Guid TaskId { get; set; }
    
    /// <summary>
    /// Navigation property to the task.
    /// </summary>
    public TaskItem Task { get; set; } = null!;
    
    /// <summary>
    /// ID of the user who uploaded this attachment.
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Navigation property to the user who uploaded the attachment.
    /// </summary>
    public User User { get; set; } = null!;
}