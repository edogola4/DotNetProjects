using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace TaskManagerApi.Hubs;

[Authorize]
public class TaskHub : Hub
{
    public async Task SendTaskNotification(string userId, string message)
    {
        await Clients.User(userId).SendAsync("ReceiveTaskNotification", message);
    }

    public async Task NotifyTaskCreated(string userId, object task)
    {
        await Clients.User(userId).SendAsync("TaskCreated", task);
    }

    public async Task NotifyTaskUpdated(string userId, object task)
    {
        await Clients.User(userId).SendAsync("TaskUpdated", task);
    }

    public async Task NotifyTaskDeleted(string userId, string taskId)
    {
        await Clients.User(userId).SendAsync("TaskDeleted", taskId);
    }
}
