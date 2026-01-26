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
    /// Task status (optional).
    /// </summary>
    public Models.TaskStatus? Status { get; set; }
    
    /// <summary>
    /// Category ID (optional).
    /// </summary>
    public Guid? CategoryId { get; set; }
    
    /// <summary>
    /// List of tag IDs to associate with the task (optional).
    /// </summary>
    public List<Guid>? TagIds { get; set; }
    
    /// <summary>
    /// Estimated time to complete in hours (optional).
    /// </summary>
    [Range(0.1, 1000)]
    public decimal? EstimatedHours { get; set; }
    
    /// <summary>
    /// Actual time spent in hours (optional).
    /// </summary>
    [Range(0.1, 1000)]
    public decimal? ActualHours { get; set; }
}
