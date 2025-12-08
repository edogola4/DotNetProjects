using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Data;
using TaskManagerApi.DTOs;
using TaskManagerApi.Models;
using TaskManagerApi.Services;
using FluentAssertions;

namespace TaskManagerApi.UnitTests;

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
        var service = new TaskService(context);
        var dto = new CreateTaskDto { Title = "Test Task", Description = "Test Description" };

        var result = await service.CreateTaskAsync(1, dto);

        result.Should().NotBeNull();
        result.Title.Should().Be("Test Task");
        result.Description.Should().Be("Test Description");
    }

    [Fact]
    public async Task GetUserTasksAsync_ReturnsOnlyUserTasks()
    {
        var context = GetInMemoryDbContext();
        context.Tasks.AddRange(
            new TaskItem { Title = "User1 Task", UserId = 1 },
            new TaskItem { Title = "User2 Task", UserId = 2 }
        );
        await context.SaveChangesAsync();
        var service = new TaskService(context);
        var parameters = new TaskFilterParameters();

        var result = await service.GetUserTasksAsync(1, parameters);

        result.Items.Should().HaveCount(1);
        result.Items.First().Title.Should().Be("User1 Task");
    }

    [Fact]
    public async Task GetTaskByIdAsync_ReturnsTask_WhenUserOwnsIt()
    {
        var context = GetInMemoryDbContext();
        var task = new TaskItem { Title = "Test Task", UserId = 1 };
        context.Tasks.Add(task);
        await context.SaveChangesAsync();
        var service = new TaskService(context);

        var result = await service.GetTaskByIdAsync(1, task.Id);

        result.Should().NotBeNull();
        result!.Title.Should().Be("Test Task");
    }

    [Fact]
    public async Task GetTaskByIdAsync_ReturnsNull_WhenUserDoesNotOwnIt()
    {
        var context = GetInMemoryDbContext();
        var task = new TaskItem { Title = "Test Task", UserId = 1 };
        context.Tasks.Add(task);
        await context.SaveChangesAsync();
        var service = new TaskService(context);

        var result = await service.GetTaskByIdAsync(2, task.Id);

        result.Should().BeNull();
    }

    [Fact]
    public async Task UpdateTaskAsync_UpdatesTask()
    {
        var context = GetInMemoryDbContext();
        var task = new TaskItem { Title = "Old Title", UserId = 1 };
        context.Tasks.Add(task);
        await context.SaveChangesAsync();
        var service = new TaskService(context);
        var dto = new UpdateTaskDto { Title = "New Title" };

        var result = await service.UpdateTaskAsync(1, task.Id, dto);

        result.Should().NotBeNull();
        result!.Title.Should().Be("New Title");
    }

    [Fact]
    public async Task DeleteTaskAsync_DeletesTask()
    {
        var context = GetInMemoryDbContext();
        var task = new TaskItem { Title = "Test Task", UserId = 1 };
        context.Tasks.Add(task);
        await context.SaveChangesAsync();
        var service = new TaskService(context);

        var result = await service.DeleteTaskAsync(1, task.Id);

        result.Should().BeTrue();
        context.Tasks.Should().BeEmpty();
    }

    [Fact]
    public async Task CompleteTaskAsync_MarksTaskAsCompleted()
    {
        var context = GetInMemoryDbContext();
        var task = new TaskItem { Title = "Test Task", UserId = 1, IsCompleted = false };
        context.Tasks.Add(task);
        await context.SaveChangesAsync();
        var service = new TaskService(context);

        var result = await service.CompleteTaskAsync(1, task.Id);

        result.Should().NotBeNull();
        result!.IsCompleted.Should().BeTrue();
    }
}
