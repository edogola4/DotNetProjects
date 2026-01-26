using System.ComponentModel.DataAnnotations;

namespace TaskManagerApi.DTOs;

/// <summary>
/// DTO for creating a new tag.
/// </summary>
public class CreateTagDto
{
    /// <summary>
    /// Tag name (required).
    /// </summary>
    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string Name { get; set; } = string.Empty;
}

/// <summary>
/// DTO for tag response data.
/// </summary>
public class TagResponseDto
{
    /// <summary>
    /// Unique tag identifier.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Tag name.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Number of tasks using this tag.
    /// </summary>
    public int TaskCount { get; set; }
}

/// <summary>
/// DTO for updating task tags.
/// </summary>
public class UpdateTaskTagsDto
{
    /// <summary>
    /// List of tag IDs to associate with the task.
    /// </summary>
    public List<Guid> TagIds { get; set; } = new();
}