using System.ComponentModel.DataAnnotations;
using TaskManagerApi.Constants;
using TaskManagerApi.Models;

namespace TaskManagerApi.DTOs;

/// <summary>
/// DTO for updating an existing task.
/// </summary>
public class UpdateTaskDto
{
    /// <summary>
    /// Task title (optional).
    /// </summary>
    [StringLength(ValidationConstants.Task.TitleMaxLength, MinimumLength = ValidationConstants.Task.TitleMinLength)]
    public string? Title { get; set; }
    
    /// <summary>
    /// Task description (optional).
    /// </summary>
    [StringLength(ValidationConstants.Task.DescriptionMaxLength)]
    public string? Description { get; set; }
    
    /// <summary>
    /// Due date (optional).
    /// </summary>
    public DateTime? DueDate { get; set; }
    
    /// <summary>
    /// Completion status (optional).
    /// </summary>
    public bool? IsCompleted { get; set; }
    
    /// <summary>
    /// Priority level (optional).
    /// </summary>
    public Priority? Priority { get; set; }
    
    /// <summary>
    /// Category ID (optional).
    /// </summary>
    public Guid? CategoryId { get; set; }
}
