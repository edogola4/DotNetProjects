using System.ComponentModel.DataAnnotations;
using TaskManagerApi.Models;

namespace TaskManagerApi.DTOs;

public class UpdateTaskDto
{
    [StringLength(200, MinimumLength = 1)]
    public string? Title { get; set; }
    
    [StringLength(1000)]
    public string? Description { get; set; }
    
    public DateTime? DueDate { get; set; }
    
    public bool? IsCompleted { get; set; }
    
    public Priority? Priority { get; set; }
    
    public int? CategoryId { get; set; }
}
