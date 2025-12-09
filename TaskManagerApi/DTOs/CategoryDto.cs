using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TaskManagerApi.DTOs;

public class CategoryDto
{
    [Required]
    [StringLength(100, MinimumLength = 1)]
    [DefaultValue("Work")]
    public string Name { get; set; } = string.Empty;
}
