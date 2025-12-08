using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Data;
using TaskManagerApi.DTOs;
using TaskManagerApi.Models;

namespace TaskManagerApi.Services;

public class TaskService : ITaskService
{
    private readonly ApplicationDbContext _context;

    public TaskService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TaskResponseDto> CreateTaskAsync(int userId, CreateTaskDto dto)
    {
        var task = new TaskItem
        {
            Title = dto.Title,
            Description = dto.Description,
            DueDate = dto.DueDate,
            Priority = dto.Priority,
            CategoryId = dto.CategoryId,
            UserId = userId
        };

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        return MapToDto(task);
    }

    public async Task<PagedList<TaskResponseDto>> GetUserTasksAsync(int userId, PaginationParameters parameters)
    {
        var query = _context.Tasks
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.CreatedAt);

        var count = await query.CountAsync();
        
        var tasks = await query
            .Skip((parameters.PageNumber - 1) * parameters.PageSize)
            .Take(parameters.PageSize)
            .ToListAsync();

        var items = tasks.Select(MapToDto).ToList();
        return new PagedList<TaskResponseDto>(items, count, parameters.PageNumber, parameters.PageSize);
    }

    public async Task<TaskResponseDto?> GetTaskByIdAsync(int userId, int taskId)
    {
        var task = await _context.Tasks
            .FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId);

        return task == null ? null : MapToDto(task);
    }

    public async Task<TaskResponseDto?> UpdateTaskAsync(int userId, int taskId, UpdateTaskDto dto)
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

        return MapToDto(task);
    }

    public async Task<bool> DeleteTaskAsync(int userId, int taskId)
    {
        var task = await _context.Tasks
            .FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId);

        if (task == null) return false;

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<TaskResponseDto?> CompleteTaskAsync(int userId, int taskId)
    {
        var task = await _context.Tasks
            .FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId);

        if (task == null) return null;

        task.IsCompleted = true;
        task.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return MapToDto(task);
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
