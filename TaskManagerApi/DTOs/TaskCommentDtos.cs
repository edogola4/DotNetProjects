using System.ComponentModel.DataAnnotations;

namespace TaskManagerApi.DTOs;

/// <summary>
/// DTO for creating a new task comment.
/// </summary>
public class CreateTaskCommentDto
{
    /// <summary>
    /// Comment content (required).
    /// </summary>
    [Required]
    [StringLength(1000, MinimumLength = 1)]
    public string Content { get; set; } = string.Empty;
}

/// <summary>
/// DTO for updating a task comment.
/// </summary>
public class UpdateTaskCommentDto
{
    /// <summary>
    /// Updated comment content (required).
    /// </summary>
    [Required]
    [StringLength(1000, MinimumLength = 1)]
    public string Content { get; set; } = string.Empty;
}

/// <summary>
/// DTO for task comment response data.
/// </summary>
public class TaskCommentResponseDto
{
    /// <summary>
    /// Unique comment identifier.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Comment content.
    /// </summary>
    public string Content { get; set; } = string.Empty;
    
    /// <summary>
    /// Comment creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Last update timestamp.
    /// </summary>
    public DateTime UpdatedAt { get; set; }
    
    /// <summary>
    /// ID of the user who created the comment.
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Username of the comment author.
    /// </summary>
    public string Username { get; set; } = string.Empty;
}