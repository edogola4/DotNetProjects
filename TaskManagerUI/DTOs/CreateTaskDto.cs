using System.ComponentModel.DataAnnotations;
using TaskManagerUI.Models;

namespace TaskManagerUI.DTOs;

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
}