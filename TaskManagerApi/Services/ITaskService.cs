using TaskManagerApi.DTOs;
using TaskManagerApi.Models;

namespace TaskManagerApi.Services;

public interface ITaskService
{
    Task<TaskResponseDto> CreateTaskAsync(int userId, CreateTaskDto dto);
    Task<PagedList<TaskResponseDto>> GetUserTasksAsync(int userId, TaskFilterParameters parameters);
    Task<TaskResponseDto?> GetTaskByIdAsync(int userId, int taskId);
    Task<TaskResponseDto?> UpdateTaskAsync(int userId, int taskId, UpdateTaskDto dto);
    Task<bool> DeleteTaskAsync(int userId, int taskId);
    Task<TaskResponseDto?> CompleteTaskAsync(int userId, int taskId);
    Task AddTagsToTaskAsync(int userId, int taskId, List<string> tagNames);
    Task RemoveTagsFromTaskAsync(int userId, int taskId, List<string> tagNames);
    Task<IEnumerable<TaskResponseDto>> GetOverdueTasksAsync(int userId);
    Task<IEnumerable<TaskResponseDto>> GetUpcomingTasksAsync(int userId, int days);
}
