using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Moq;
using TaskManagerApi.Data;
using TaskManagerApi.Models;
using TaskManagerApi.Services;
using TaskManagerApi.Hubs;
using FluentAssertions;

public class TagTests
{
    private ApplicationDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task AddTagsToTaskAsync_AddsNewTags()
    {
        var context = GetInMemoryDbContext();
        var userId = Guid.NewGuid();
        var task = new TaskItem { Title = "Test", UserId = userId };
        context.Tasks.Add(task);
        await context.SaveChangesAsync();
        var mockHub = new Mock<IHubContext<TaskHub>>();
        var mockLogger = new Mock<ILogger<TaskService>>();
        var service = new TaskService(context, mockHub.Object, mockLogger.Object);

        await service.AddTagsToTaskAsync(userId, task.Id, new List<string> { "urgent", "work" });

        var updatedTask = await context.Tasks.Include(t => t.Tags).FirstAsync(t => t.Id == task.Id);
        updatedTask.Tags.Should().HaveCount(2);
    }

    [Fact]
    public async Task RemoveTagsFromTaskAsync_RemovesTags()
    {
        var context = GetInMemoryDbContext();
        var userId = Guid.NewGuid();
        var tag = new Tag { Name = "urgent" };
        var task = new TaskItem { Title = "Test", UserId = userId };
        task.Tags.Add(tag);
        context.Tasks.Add(task);
        await context.SaveChangesAsync();
        var mockHub = new Mock<IHubContext<TaskHub>>();
        var mockLogger = new Mock<ILogger<TaskService>>();
        var service = new TaskService(context, mockHub.Object, mockLogger.Object);

        await service.RemoveTagsFromTaskAsync(userId, task.Id, new List<string> { "urgent" });

        var updatedTask = await context.Tasks.Include(t => t.Tags).FirstAsync(t => t.Id == task.Id);
        updatedTask.Tags.Should().BeEmpty();
    }

    [Fact]
    public async Task GetUserTasksAsync_FiltersByCategory()
    {
        var context = GetInMemoryDbContext();
        var userId = Guid.NewGuid();
        var categoryId1 = Guid.NewGuid();
        var categoryId2 = Guid.NewGuid();
        context.Tasks.AddRange(
            new TaskItem { Title = "Task1", UserId = userId, CategoryId = categoryId1 },
            new TaskItem { Title = "Task2", UserId = userId, CategoryId = categoryId2 }
        );
        await context.SaveChangesAsync();
        var mockHub = new Mock<IHubContext<TaskHub>>();
        var mockLogger = new Mock<ILogger<TaskService>>();
        var service = new TaskService(context, mockHub.Object, mockLogger.Object);
        var parameters = new TaskFilterParameters { CategoryId = categoryId1 };

        var result = await service.GetUserTasksAsync(userId, parameters);

        result.Items.Should().HaveCount(1);
        result.Items.First().Title.Should().Be("Task1");
    }
}
