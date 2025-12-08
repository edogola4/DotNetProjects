using System.ComponentModel.DataAnnotations;

namespace TaskManagerApi.DTOs;

public class CategoryDto
{
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string Name { get; set; } = string.Empty;
}
