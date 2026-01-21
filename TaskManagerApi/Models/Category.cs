namespace TaskManagerApi.Models;

/// <summary>
/// Represents a category for organizing tasks.
/// </summary>
public class Category
{
    /// <summary>
    /// Unique identifier for the category.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Name of the category.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Date and time when the category was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// ID of the user who owns this category.
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Navigation property to the user who owns this category.
    /// </summary>
    public User User { get; set; } = null!;
    
    /// <summary>
    /// Collection of tasks in this category.
    /// </summary>
    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}
