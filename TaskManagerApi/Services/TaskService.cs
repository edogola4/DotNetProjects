using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Data;
using TaskManagerApi.DTOs;
using TaskManagerApi.Hubs;
using TaskManagerApi.Models;

namespace TaskManagerApi.Services;

public class TaskService : ITaskService
{
    private readonly ApplicationDbContext _context;
    private readonly IHubContext<TaskHub> _hubContext;
    private readonly ILogger<TaskService> _logger;

    public TaskService(ApplicationDbContext context, IHubContext<TaskHub> hubContext, ILogger<TaskService> logger)
    {
        _context = context;
        _hubContext = hubContext;
        _logger = logger;
    }

    public async Task<TaskResponseDto> CreateTaskAsync(Guid userId, CreateTaskDto dto)
    {
        var now = DateTime.UtcNow;
        var task = new TaskItem
        {
            Title = dto.Title,
            Description = dto.Description,
            DueDate = dto.DueDate,
            Priority = dto.Priority,
            CategoryId = dto.CategoryId,
            UserId = userId,
            CreatedAt = now,
            UpdatedAt = now
        };

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Task created: {TaskId} by user {UserId}", task.Id, userId);
        
        var taskDto = MapToDto(task);
        await _hubContext.Clients.User(userId.ToString()).SendAsync("TaskCreated", taskDto);
        return taskDto;
    }

    public async Task<PagedList<TaskResponseDto>> GetUserTasksAsync(Guid userId, TaskFilterParameters parameters)
    {
        var query = _context.Tasks
            .Include(t => t.Tags)
            .Where(t => t.UserId == userId);

        if (!string.IsNullOrEmpty(parameters.Search))
            query = query.Where(t => t.Title.ToLower().Contains(parameters.Search.ToLower()) || t.Description.ToLower().Contains(parameters.Search.ToLower()));

        if (parameters.IsCompleted.HasValue)
            query = query.Where(t => t.IsCompleted == parameters.IsCompleted.Value);

        if (parameters.Priority.HasValue)
            query = query.Where(t => t.Priority == parameters.Priority.Value);

        if (parameters.CategoryId.HasValue)
            query = query.Where(t => t.CategoryId == parameters.CategoryId.Value);

        if (!string.IsNullOrEmpty(parameters.Tags))
        {
            var tagList = parameters.Tags.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim().ToLower());
            query = query.Where(t => t.Tags.Any(tag => tagList.Contains(tag.Name.ToLower())));
        }

        if (parameters.DueDateFrom.HasValue)
            query = query.Where(t => t.DueDate >= parameters.DueDateFrom.Value);

        if (parameters.DueDateTo.HasValue)
            query = query.Where(t => t.DueDate <= parameters.DueDateTo.Value);

        query = parameters.SortBy.ToLower() switch
        {
            "title" => parameters.SortOrder.ToLower() == "asc" ? query.OrderBy(t => t.Title) : query.OrderByDescending(t => t.Title),
            "duedate" => parameters.SortOrder.ToLower() == "asc" ? query.OrderBy(t => t.DueDate) : query.OrderByDescending(t => t.DueDate),
            "priority" => parameters.SortOrder.ToLower() == "asc" ? query.OrderBy(t => t.Priority) : query.OrderByDescending(t => t.Priority),
            _ => parameters.SortOrder.ToLower() == "asc" ? query.OrderBy(t => t.CreatedAt) : query.OrderByDescending(t => t.CreatedAt)
        };

        var count = await query.CountAsync();
        
        var tasks = await query
            .Skip((parameters.PageNumber - 1) * parameters.PageSize)
            .Take(parameters.PageSize)
            .ToListAsync();

        var items = tasks.Select(MapToDto).ToList();
        return new PagedList<TaskResponseDto>(items, count, parameters.PageNumber, parameters.PageSize);
    }

    public async Task<TaskResponseDto?> GetTaskByIdAsync(Guid userId, Guid taskId)
    {
        var task = await _context.Tasks
            .FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId);

        return task == null ? null : MapToDto(task);
    }

    public async Task<TaskResponseDto?> UpdateTaskAsync(Guid userId, Guid taskId, UpdateTaskDto dto)
    {
        var task = await _context.Tasks
            .FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId);

        if (task == null) return null;

        if (dto.Title != null) task.Title = dto.Title;
        if (dto.Description != null) task.Description = dto.Description;
        if (dto.DueDate.HasValue) task.DueDate = dto.DueDate;
        if (dto.IsCompleted.HasValue) task.IsCompleted = dto.IsCompleted.Value;
        if (dto.Priority.HasValue) task.Priority = dto.Priority.Value;
        if (dto.CategoryId.HasValue) task.CategoryId = dto.CategoryId;

        task.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        var result = MapToDto(task);
        await _hubContext.Clients.User(userId.ToString()).SendAsync("TaskUpdated", result);
        return result;
    }

    public async Task<bool> DeleteTaskAsync(Guid userId, Guid taskId)
    {
        var task = await _context.Tasks
            .FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId);

        if (task == null) return false;

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        
        _logger.LogInformation("Task deleted: {TaskId} by user {UserId}", taskId, userId);
        
        await _hubContext.Clients.User(userId.ToString()).SendAsync("TaskDeleted", taskId.ToString());
        return true;
    }

    public async Task<TaskResponseDto?> CompleteTaskAsync(Guid userId, Guid taskId)
    {
        var task = await _context.Tasks
            .FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId);

        if (task == null) return null;

        task.IsCompleted = true;
        task.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        var result = MapToDto(task);
        await _hubContext.Clients.User(userId.ToString()).SendAsync("TaskUpdated", result);
        return result;
    }

    public async Task AddTagsToTaskAsync(Guid userId, Guid taskId, List<string> tagNames)
    {
        var task = await _context.Tasks
            .Include(t => t.Tags)
            .FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId);

        if (task == null) return;

        foreach (var tagName in tagNames)
        {
            var tag = await _context.Tags.FirstOrDefaultAsync(t => t.Name.ToLower() == tagName.ToLower());
            if (tag == null)
            {
                tag = new Tag { Name = tagName.ToLower() };
                _context.Tags.Add(tag);
            }
            if (!task.Tags.Contains(tag))
                task.Tags.Add(tag);
        }
        await _context.SaveChangesAsync();
    }

    public async Task RemoveTagsFromTaskAsync(Guid userId, Guid taskId, List<string> tagNames)
    {
        var task = await _context.Tasks
            .Include(t => t.Tags)
            .FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId);

        if (task == null) return;

        var tagsToRemove = task.Tags.Where(t => tagNames.Contains(t.Name, StringComparer.OrdinalIgnoreCase)).ToList();
        foreach (var tag in tagsToRemove)
            task.Tags.Remove(tag);

        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<TaskResponseDto>> GetOverdueTasksAsync(Guid userId)
    {
        var now = DateTime.UtcNow;
        var tasks = await _context.Tasks
            .Where(t => t.UserId == userId && !t.IsCompleted && t.DueDate.HasValue && t.DueDate.Value < now)
            .OrderBy(t => t.DueDate)
            .ToListAsync();

        return tasks.Select(MapToDto);
    }

    public async Task<IEnumerable<TaskResponseDto>> GetUpcomingTasksAsync(Guid userId, int days)
    {
        var now = DateTime.UtcNow;
        var futureDate = now.AddDays(days);
        var tasks = await _context.Tasks
            .Where(t => t.UserId == userId && !t.IsCompleted && t.DueDate.HasValue && t.DueDate.Value >= now && t.DueDate.Value <= futureDate)
            .OrderBy(t => t.DueDate)
            .ToListAsync();

        return tasks.Select(MapToDto);
    }

    private static TaskResponseDto MapToDto(TaskItem task)
    {
        return new TaskResponseDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            DueDate = task.DueDate,
            IsCompleted = task.IsCompleted,
            Priority = task.Priority,
            CategoryId = task.CategoryId,
            CreatedAt = task.CreatedAt,
            UpdatedAt = task.UpdatedAt
        };
    }
}
