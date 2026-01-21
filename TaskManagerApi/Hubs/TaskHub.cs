using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace TaskManagerApi.Hubs;

/// <summary>
/// SignalR hub for real-time task notifications.
/// </summary>
[Authorize]
public class TaskHub : Hub
{
    /// <summary>
    /// Sends a general task notification to a specific user.
    /// </summary>
    /// <param name="userId">Target user ID.</param>
    /// <param name="message">Notification message.</param>
    public async Task SendTaskNotification(string userId, string message)
    {
        await Clients.User(userId).SendAsync("ReceiveTaskNotification", message);
    }

    /// <summary>
    /// Notifies a user that a new task has been created.
    /// </summary>
    /// <param name="userId">Target user ID.</param>
    /// <param name="task">Created task data.</param>
    public async Task NotifyTaskCreated(string userId, object task)
    {
        await Clients.User(userId).SendAsync("TaskCreated", task);
    }

    /// <summary>
    /// Notifies a user that a task has been updated.
    /// </summary>
    /// <param name="userId">Target user ID.</param>
    /// <param name="task">Updated task data.</param>
    public async Task NotifyTaskUpdated(string userId, object task)
    {
        await Clients.User(userId).SendAsync("TaskUpdated", task);
    }

    /// <summary>
    /// Notifies a user that a task has been deleted.
    /// </summary>
    /// <param name="userId">Target user ID.</param>
    /// <param name="taskId">ID of the deleted task.</param>
    public async Task NotifyTaskDeleted(string userId, string taskId)
    {
        await Clients.User(userId).SendAsync("TaskDeleted", taskId);
    }
}
