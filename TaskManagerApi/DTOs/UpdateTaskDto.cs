using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using TaskManagerApi.Models;

namespace TaskManagerApi.DTOs;

public class UpdateTaskDto
{
    [StringLength(200, MinimumLength = 1)]
    [DefaultValue("Updated task title")]
    public string? Title { get; set; }
    
    [StringLength(1000)]
    [DefaultValue("Updated task description")]
    public string? Description { get; set; }
    
    [DefaultValue("2025-12-31T23:59:59Z")]
    public DateTime? DueDate { get; set; }
    
    [DefaultValue(true)]
    public bool? IsCompleted { get; set; }
    
    [DefaultValue(2)]
    public Priority? Priority { get; set; }
    
    [DefaultValue(null)]
    public Guid? CategoryId { get; set; }
}
