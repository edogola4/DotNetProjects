using TaskManagerApi.DTOs;
using TaskManagerApi.Models;

namespace TaskManagerApi.Services;

public interface ITaskService
{
    Task<TaskResponseDto> CreateTaskAsync(Guid userId, CreateTaskDto dto);
    Task<PagedList<TaskResponseDto>> GetUserTasksAsync(Guid userId, TaskFilterParameters parameters);
    Task<TaskResponseDto?> GetTaskByIdAsync(Guid userId, Guid taskId);
    Task<TaskResponseDto?> UpdateTaskAsync(Guid userId, Guid taskId, UpdateTaskDto dto);
    Task<bool> DeleteTaskAsync(Guid userId, Guid taskId);
    Task<TaskResponseDto?> CompleteTaskAsync(Guid userId, Guid taskId);
    Task AddTagsToTaskAsync(Guid userId, Guid taskId, List<string> tagNames);
    Task RemoveTagsFromTaskAsync(Guid userId, Guid taskId, List<string> tagNames);
    Task<IEnumerable<TaskResponseDto>> GetOverdueTasksAsync(Guid userId);
    Task<IEnumerable<TaskResponseDto>> GetUpcomingTasksAsync(Guid userId, int days);
    Task<object> GetTaskStatsAsync(Guid userId);
}
