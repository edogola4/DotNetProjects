using TaskManagerApi.DTOs;
using TaskManagerApi.Models;

namespace TaskManagerApi.Services;

/// <summary>
/// Interface for task management operations.
/// </summary>
public interface ITaskService
{
    /// <summary>
    /// Creates a new task for the specified user.
    /// </summary>
    /// <param name="userId">User ID.</param>
    /// <param name="dto">Task creation data.</param>
    /// <returns>Created task details.</returns>
    Task<TaskResponseDto> CreateTaskAsync(Guid userId, CreateTaskDto dto);
    
    /// <summary>
    /// Gets paginated tasks for the specified user with filtering options.
    /// </summary>
    /// <param name="userId">User ID.</param>
    /// <param name="parameters">Filter and pagination parameters.</param>
    /// <returns>Paginated list of tasks.</returns>
    Task<PagedList<TaskResponseDto>> GetUserTasksAsync(Guid userId, TaskFilterParameters parameters);
    
    /// <summary>
    /// Gets a specific task by ID for the specified user.
    /// </summary>
    /// <param name="userId">User ID.</param>
    /// <param name="taskId">Task ID.</param>
    /// <returns>Task details or null if not found.</returns>
    Task<TaskResponseDto?> GetTaskByIdAsync(Guid userId, Guid taskId);
    
    /// <summary>
    /// Updates an existing task for the specified user.
    /// </summary>
    /// <param name="userId">User ID.</param>
    /// <param name="taskId">Task ID.</param>
    /// <param name="dto">Task update data.</param>
    /// <returns>Updated task details or null if not found.</returns>
    Task<TaskResponseDto?> UpdateTaskAsync(Guid userId, Guid taskId, UpdateTaskDto dto);
    
    /// <summary>
    /// Deletes a task for the specified user.
    /// </summary>
    /// <param name="userId">User ID.</param>
    /// <param name="taskId">Task ID.</param>
    /// <returns>True if deleted successfully, false if not found.</returns>
    Task<bool> DeleteTaskAsync(Guid userId, Guid taskId);
    
    /// <summary>
    /// Marks a task as completed for the specified user.
    /// </summary>
    /// <param name="userId">User ID.</param>
    /// <param name="taskId">Task ID.</param>
    /// <returns>Updated task details or null if not found.</returns>
    Task<TaskResponseDto?> CompleteTaskAsync(Guid userId, Guid taskId);
    
    /// <summary>
    /// Adds tags to a task for the specified user.
    /// </summary>
    /// <param name="userId">User ID.</param>
    /// <param name="taskId">Task ID.</param>
    /// <param name="tagNames">List of tag names to add.</param>
    Task AddTagsToTaskAsync(Guid userId, Guid taskId, List<string> tagNames);
    
    /// <summary>
    /// Removes tags from a task for the specified user.
    /// </summary>
    /// <param name="userId">User ID.</param>
    /// <param name="taskId">Task ID.</param>
    /// <param name="tagNames">List of tag names to remove.</param>
    Task RemoveTagsFromTaskAsync(Guid userId, Guid taskId, List<string> tagNames);
    
    /// <summary>
    /// Gets overdue tasks for the specified user.
    /// </summary>
    /// <param name="userId">User ID.</param>
    /// <returns>List of overdue tasks.</returns>
    Task<IEnumerable<TaskResponseDto>> GetOverdueTasksAsync(Guid userId);
    
    /// <summary>
    /// Gets upcoming tasks within the specified number of days for the user.
    /// </summary>
    /// <param name="userId">User ID.</param>
    /// <param name="days">Number of days to look ahead.</param>
    /// <returns>List of upcoming tasks.</returns>
    Task<IEnumerable<TaskResponseDto>> GetUpcomingTasksAsync(Guid userId, int days);
    
    /// <summary>
    /// Gets task statistics for the specified user.
    /// </summary>
    /// <param name="userId">User ID.</param>
    /// <returns>Task statistics.</returns>
    Task<TaskStatsDto> GetTaskStatsAsync(Guid userId);
}
