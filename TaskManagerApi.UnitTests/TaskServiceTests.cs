using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Moq;
using TaskManagerApi.Data;
using TaskManagerApi.DTOs;
using TaskManagerApi.Models;
using TaskManagerApi.Services;
using TaskManagerApi.Hubs;
using FluentAssertions;

public class TaskServiceTests
{
    private ApplicationDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task CreateTaskAsync_CreatesTask()
    {
        var context = GetInMemoryDbContext();
        var mockHub = new Mock<IHubContext<TaskHub>>();
        var mockLogger = new Mock<ILogger<TaskService>>();
        var service = new TaskService(context, mockHub.Object, mockLogger.Object);
        var userId = Guid.NewGuid();
        var dto = new CreateTaskDto { Title = "Test Task", Description = "Test Description" };

        var result = await service.CreateTaskAsync(userId, dto);

        result.Should().NotBeNull();
        result.Title.Should().Be("Test Task");
        result.Description.Should().Be("Test Description");
    }

    [Fact]
    public async Task GetUserTasksAsync_ReturnsOnlyUserTasks()
    {
        var context = GetInMemoryDbContext();
        var userId1 = Guid.NewGuid();
        var userId2 = Guid.NewGuid();
        context.Tasks.AddRange(
            new TaskItem { Title = "User1 Task", UserId = userId1 },
            new TaskItem { Title = "User2 Task", UserId = userId2 }
        );
        await context.SaveChangesAsync();
        var mockHub = new Mock<IHubContext<TaskHub>>();
        var mockLogger = new Mock<ILogger<TaskService>>();
        var service = new TaskService(context, mockHub.Object, mockLogger.Object);
        var parameters = new TaskFilterParameters();

        var result = await service.GetUserTasksAsync(userId1, parameters);

        result.Items.Should().HaveCount(1);
        result.Items.First().Title.Should().Be("User1 Task");
    }

    [Fact]
    public async Task GetTaskByIdAsync_ReturnsTask_WhenUserOwnsIt()
    {
        var context = GetInMemoryDbContext();
        var userId = Guid.NewGuid();
        var task = new TaskItem { Title = "Test Task", UserId = userId };
        context.Tasks.Add(task);
        await context.SaveChangesAsync();
        var mockHub = new Mock<IHubContext<TaskHub>>();
        var mockLogger = new Mock<ILogger<TaskService>>();
        var service = new TaskService(context, mockHub.Object, mockLogger.Object);

        var result = await service.GetTaskByIdAsync(userId, task.Id);

        result.Should().NotBeNull();
        result!.Title.Should().Be("Test Task");
    }

    [Fact]
    public async Task GetTaskByIdAsync_ReturnsNull_WhenUserDoesNotOwnIt()
    {
        var context = GetInMemoryDbContext();
        var userId1 = Guid.NewGuid();
        var userId2 = Guid.NewGuid();
        var task = new TaskItem { Title = "Test Task", UserId = userId1 };
        context.Tasks.Add(task);
        await context.SaveChangesAsync();
        var mockHub = new Mock<IHubContext<TaskHub>>();
        var mockLogger = new Mock<ILogger<TaskService>>();
        var service = new TaskService(context, mockHub.Object, mockLogger.Object);

        var result = await service.GetTaskByIdAsync(userId2, task.Id);

        result.Should().BeNull();
    }

    [Fact]
    public async Task UpdateTaskAsync_UpdatesTask()
    {
        var context = GetInMemoryDbContext();
        var userId = Guid.NewGuid();
        var task = new TaskItem { Title = "Old Title", UserId = userId };
        context.Tasks.Add(task);
        await context.SaveChangesAsync();
        var mockHub = new Mock<IHubContext<TaskHub>>();
        var mockLogger = new Mock<ILogger<TaskService>>();
        var service = new TaskService(context, mockHub.Object, mockLogger.Object);
        var dto = new UpdateTaskDto { Title = "New Title" };

        var result = await service.UpdateTaskAsync(userId, task.Id, dto);

        result.Should().NotBeNull();
        result!.Title.Should().Be("New Title");
    }

    [Fact]
    public async Task DeleteTaskAsync_DeletesTask()
    {
        var context = GetInMemoryDbContext();
        var userId = Guid.NewGuid();
        var task = new TaskItem { Title = "Test Task", UserId = userId };
        context.Tasks.Add(task);
        await context.SaveChangesAsync();
        var mockHub = new Mock<IHubContext<TaskHub>>();
        var mockLogger = new Mock<ILogger<TaskService>>();
        var service = new TaskService(context, mockHub.Object, mockLogger.Object);

        var result = await service.DeleteTaskAsync(userId, task.Id);

        result.Should().BeTrue();
        context.Tasks.Should().BeEmpty();
    }

    [Fact]
    public async Task CompleteTaskAsync_MarksTaskAsCompleted()
    {
        var context = GetInMemoryDbContext();
        var userId = Guid.NewGuid();
        var task = new TaskItem { Title = "Test Task", UserId = userId, IsCompleted = false };
        context.Tasks.Add(task);
        await context.SaveChangesAsync();
        var mockHub = new Mock<IHubContext<TaskHub>>();
        var mockLogger = new Mock<ILogger<TaskService>>();
        var service = new TaskService(context, mockHub.Object, mockLogger.Object);

        var result = await service.CompleteTaskAsync(userId, task.Id);

        result.Should().NotBeNull();
        result!.IsCompleted.Should().BeTrue();
    }
}
