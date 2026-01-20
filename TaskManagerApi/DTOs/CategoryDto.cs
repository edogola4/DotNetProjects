using System.ComponentModel.DataAnnotations;
using TaskManagerApi.Constants;

namespace TaskManagerApi.DTOs;

/// <summary>
/// DTO for category operations.
/// </summary>
public class CategoryDto
{
    /// <summary>
    /// Category name (required).
    /// </summary>
    [Required]
    [StringLength(ValidationConstants.Category.NameMaxLength, MinimumLength = ValidationConstants.Category.NameMinLength)]
    public string Name { get; set; } = string.Empty;
}
