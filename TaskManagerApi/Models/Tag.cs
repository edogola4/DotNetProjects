namespace TaskManagerApi.Models;

/// <summary>
/// Represents a tag that can be associated with tasks for organization and filtering.
/// </summary>
public class Tag
{
    /// <summary>
    /// Unique identifier for the tag.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Name of the tag.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Collection of tasks associated with this tag.
    /// </summary>
    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}
