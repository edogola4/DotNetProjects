using TaskManagerApi.DTOs;

namespace TaskManagerApi.Services;

public interface ITaskService
{
    Task<TaskResponseDto> CreateTaskAsync(int userId, CreateTaskDto dto);
    Task<IEnumerable<TaskResponseDto>> GetUserTasksAsync(int userId);
    Task<TaskResponseDto?> GetTaskByIdAsync(int userId, int taskId);
    Task<TaskResponseDto?> UpdateTaskAsync(int userId, int taskId, UpdateTaskDto dto);
    Task<bool> DeleteTaskAsync(int userId, int taskId);
    Task<TaskResponseDto?> CompleteTaskAsync(int userId, int taskId);
}
