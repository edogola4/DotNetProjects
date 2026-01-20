namespace TaskManagerApi.Models;

/// <summary>
/// Represents a user in the task management system.
/// </summary>
public class User
{
    /// <summary>
    /// Unique identifier for the user.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// User's display name.
    /// </summary>
    public string Username { get; set; } = string.Empty;
    
    /// <summary>
    /// User's email address used for authentication.
    /// </summary>
    public string Email { get; set; } = string.Empty;
    
    /// <summary>
    /// Hashed password for authentication.
    /// </summary>
    public string PasswordHash { get; set; } = string.Empty;
    
    /// <summary>
    /// Date and time when the user account was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Collection of tasks belonging to this user.
    /// </summary>
    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    
    /// <summary>
    /// Collection of categories created by this user.
    /// </summary>
    public ICollection<Category> Categories { get; set; } = new List<Category>();
}
