using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using TaskManagerApi.Models;

namespace TaskManagerApi.DTOs;

public class CreateTaskDto
{
    [Required]
    [StringLength(200, MinimumLength = 1)]
    [DefaultValue("Complete project documentation")]
    public string Title { get; set; } = string.Empty;
    
    [StringLength(1000)]
    [DefaultValue("Write comprehensive documentation for the API endpoints")]
    public string Description { get; set; } = string.Empty;
    
    [DefaultValue("2025-12-31T23:59:59Z")]
    public DateTime? DueDate { get; set; }
    
    [DefaultValue(2)]
    public Priority Priority { get; set; } = Priority.Medium;
    
    [DefaultValue(null)]
    public Guid? CategoryId { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (DueDate.HasValue && DueDate.Value < DateTime.UtcNow.Date)
            yield return new ValidationResult("Due date cannot be in the past", new[] { nameof(DueDate) });
    }
}
