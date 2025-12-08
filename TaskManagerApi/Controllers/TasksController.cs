using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.DTOs;
using TaskManagerApi.Models;
using TaskManagerApi.Services;

namespace TaskManagerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    private int GetUserId() => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto dto)
    {
        var task = await _taskService.CreateTaskAsync(GetUserId(), dto);
        return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
    }

    [HttpGet]
    public async Task<IActionResult> GetTasks([FromQuery] TaskFilterParameters parameters)
    {
        var pagedTasks = await _taskService.GetUserTasksAsync(GetUserId(), parameters);
        
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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTask(int id)
    {
        var task = await _taskService.GetTaskByIdAsync(GetUserId(), id);
        return task == null ? NotFound() : Ok(task);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id, [FromBody] UpdateTaskDto dto)
    {
        var task = await _taskService.UpdateTaskAsync(GetUserId(), id, dto);
        return task == null ? NotFound() : Ok(task);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var result = await _taskService.DeleteTaskAsync(GetUserId(), id);
        return result ? NoContent() : NotFound();
    }

    [HttpPatch("{id}/complete")]
    public async Task<IActionResult> CompleteTask(int id)
    {
        var task = await _taskService.CompleteTaskAsync(GetUserId(), id);
        return task == null ? NotFound() : Ok(task);
    }

    [HttpPost("{id}/tags")]
    public async Task<IActionResult> AddTags(int id, [FromBody] List<string> tags)
    {
        await _taskService.AddTagsToTaskAsync(GetUserId(), id, tags);
        return NoContent();
    }

    [HttpDelete("{id}/tags")]
    public async Task<IActionResult> RemoveTags(int id, [FromBody] List<string> tags)
    {
        await _taskService.RemoveTagsFromTaskAsync(GetUserId(), id, tags);
        return NoContent();
    }
}
