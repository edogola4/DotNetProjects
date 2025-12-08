namespace TaskManagerApi.Models;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime? DueDate { get; set; }
    public bool IsCompleted { get; set; }
    public Priority Priority { get; set; } = Priority.Medium;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    
    public int? CategoryId { get; set; }
    public Category? Category { get; set; }
    
    public ICollection<Tag> Tags { get; set; } = new List<Tag>();
}

public enum Priority
{
    Low = 0,
    Medium = 1,
    High = 2,
    Urgent = 3
}
