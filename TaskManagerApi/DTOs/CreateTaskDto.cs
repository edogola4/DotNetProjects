using System.ComponentModel.DataAnnotations;
using TaskManagerApi.Constants;
using TaskManagerApi.Models;

namespace TaskManagerApi.DTOs;

/// <summary>
/// DTO for creating a new task.
/// </summary>
public class CreateTaskDto
{
    /// <summary>
    /// Task title (required).
    /// </summary>
    [Required]
    [StringLength(ValidationConstants.Task.TitleMaxLength, MinimumLength = ValidationConstants.Task.TitleMinLength)]
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// Detailed task description (optional).
    /// </summary>
    [StringLength(ValidationConstants.Task.DescriptionMaxLength)]
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Optional due date for the task.
    /// </summary>
    public DateTime? DueDate { get; set; }
    
    /// <summary>
    /// Task priority level (defaults to Medium).
    /// </summary>
    public Priority Priority { get; set; } = Priority.Medium;
    
    /// <summary>
    /// Optional category ID for organizing the task.
    /// </summary>
    public Guid? CategoryId { get; set; }

    /// <summary>
    /// Validates the DTO properties.
    /// </summary>
    /// <param name="validationContext">Validation context.</param>
    /// <returns>Validation results.</returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (DueDate.HasValue && DueDate.Value < DateTime.UtcNow.Date)
            yield return new ValidationResult(ErrorMessages.Task.DueDateInPast, new[] { nameof(DueDate) });
    }
}
