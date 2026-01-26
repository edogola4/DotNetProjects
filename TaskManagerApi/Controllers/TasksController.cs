using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.DTOs;
using TaskManagerApi.Models;
using TaskManagerApi.Services;

namespace TaskManagerApi.Controllers;

/// <summary>
/// Controller for task management operations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    /// <summary>
    /// Initializes a new instance of the TasksController.
    /// </summary>
    /// <param name="taskService">Task service.</param>
    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    /// <summary>
    /// Gets the current user's ID from JWT claims.
    /// </summary>
    /// <returns>User ID.</returns>
    private Guid GetUserId() => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    /// <summary>
    /// Creates a new task for the authenticated user.
    /// </summary>
    /// <param name="dto">Task creation data.</param>
    /// <returns>Created task details.</returns>
    /// <response code="201">Task created successfully.</response>
    /// <response code="400">Invalid task data.</response>
    [HttpPost]
    public async Task<ActionResult<TaskResponseDto>> CreateTask([FromBody] CreateTaskDto dto)
    {
        var task = await _taskService.CreateTaskAsync(GetUserId(), dto);
        return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
    }

    /// <summary>
    /// Gets paginated tasks for the authenticated user with optional filtering.
    /// </summary>
    /// <param name="parameters">Filter and pagination parameters.</param>
    /// <returns>Paginated list of tasks with metadata in X-Pagination header.</returns>
    /// <response code="200">Tasks retrieved successfully.</response>
    [HttpGet]
    public async Task<ActionResult<List<TaskResponseDto>>> GetTasks([FromQuery] TaskFilterParameters parameters)
    {
        var userId = GetUserId();
        var pagedTasks = await _taskService.GetUserTasksAsync(userId, parameters);
        
        var metadata = new
        {
            pagedTasks.TotalCount,
            pagedTasks.PageSize,
            pagedTasks.CurrentPage,
            pagedTasks.TotalPages,
            pagedTasks.HasNext,
            pagedTasks.HasPrevious
        };
        
        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metadata));
        return Ok(pagedTasks.Items);
    }

    /// <summary>
    /// Gets a specific task by ID for the authenticated user.
    /// </summary>
    /// <param name="id">Task ID.</param>
    /// <returns>Task details.</returns>
    /// <response code="200">Task found.</response>
    /// <response code="404">Task not found.</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskResponseDto>> GetTask(Guid id)
    {
        var task = await _taskService.GetTaskByIdAsync(GetUserId(), id);
        return task == null ? NotFound() : Ok(task);
    }

    /// <summary>
    /// Updates an existing task for the authenticated user.
    /// </summary>
    /// <param name="id">Task ID to update.</param>
    /// <param name="dto">Updated task data.</param>
    /// <returns>Updated task details.</returns>
    /// <response code="200">Task updated successfully.</response>
    /// <response code="404">Task not found.</response>
    /// <response code="400">Invalid task data.</response>
    [HttpPut("{id}")]
    public async Task<ActionResult<TaskResponseDto>> UpdateTask(Guid id, [FromBody] UpdateTaskDto dto)
    {
        var task = await _taskService.UpdateTaskAsync(GetUserId(), id, dto);
        return task == null ? NotFound() : Ok(task);
    }

    /// <summary>
    /// Deletes a task for the authenticated user.
    /// </summary>
    /// <param name="id">Task ID to delete.</param>
    /// <returns>No content on successful deletion.</returns>
    /// <response code="204">Task deleted successfully.</response>
    /// <response code="404">Task not found.</response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(Guid id)
    {
        var result = await _taskService.DeleteTaskAsync(GetUserId(), id);
        return result ? NoContent() : NotFound();
    }

    /// <summary>
    /// Marks a task as completed for the authenticated user.
    /// </summary>
    /// <param name="id">Task ID to complete.</param>
    /// <returns>Updated task with completion status.</returns>
    /// <response code="200">Task marked as completed.</response>
    /// <response code="404">Task not found.</response>
    [HttpPatch("{id}/complete")]
    public async Task<IActionResult> CompleteTask(Guid id)
    {
        var task = await _taskService.CompleteTaskAsync(GetUserId(), id);
        return task == null ? NotFound() : Ok(task);
    }

    /// <summary>
    /// Gets all overdue tasks for the authenticated user.
    /// </summary>
    /// <returns>List of overdue tasks.</returns>
    /// <response code="200">Overdue tasks retrieved successfully.</response>
    [HttpGet("overdue")]
    public async Task<ActionResult<List<TaskResponseDto>>> GetOverdueTasks()
    {
        var tasks = await _taskService.GetOverdueTasksAsync(GetUserId());
        return Ok(tasks);
    }

    /// <summary>
    /// Gets upcoming tasks due within the specified number of days.
    /// </summary>
    /// <param name="days">Number of days to look ahead (default: 7).</param>
    /// <returns>List of upcoming tasks.</returns>
    /// <response code="200">Upcoming tasks retrieved successfully.</response>
    [HttpGet("upcoming")]
    public async Task<ActionResult<List<TaskResponseDto>>> GetUpcomingTasks([FromQuery] int days = 7)
    {
        var tasks = await _taskService.GetUpcomingTasksAsync(GetUserId(), days);
        return Ok(tasks);
    }

    /// <summary>
    /// Gets task statistics for the authenticated user.
    /// </summary>
    /// <returns>Task statistics including counts by status and priority.</returns>
    /// <response code="200">Task statistics retrieved successfully.</response>
    [HttpGet("stats")]
    public async Task<IActionResult> GetTaskStats()
    {
        var userId = GetUserId();
        var stats = await _taskService.GetTaskStatsAsync(userId);
        return Ok(stats);
    }
}
