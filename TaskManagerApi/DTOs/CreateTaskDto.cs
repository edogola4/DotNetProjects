using System.ComponentModel.DataAnnotations;
using TaskManagerApi.Models;

namespace TaskManagerApi.DTOs;

public class CreateTaskDto
{
    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string Title { get; set; } = string.Empty;
    
    [StringLength(1000)]
    public string Description { get; set; } = string.Empty;
    
    public DateTime? DueDate { get; set; }
    
    public Priority Priority { get; set; } = Priority.Medium;
    
    public Guid? CategoryId { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (DueDate.HasValue && DueDate.Value < DateTime.UtcNow.Date)
            yield return new ValidationResult("Due date cannot be in the past", new[] { nameof(DueDate) });
    }
}
