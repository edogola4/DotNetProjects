using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Moq;
using TaskManagerApi.Data;
using TaskManagerApi.Models;
using TaskManagerApi.Services;
using TaskManagerApi.Hubs;
using FluentAssertions;

public class DueDateTests
{
    private ApplicationDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task GetOverdueTasksAsync_ReturnsOnlyOverdueTasks()
    {
        var context = GetInMemoryDbContext();
        var userId = Guid.NewGuid();
        var yesterday = DateTime.UtcNow.AddDays(-1);
        var tomorrow = DateTime.UtcNow.AddDays(1);
        context.Tasks.AddRange(
            new TaskItem { Title = "Overdue", UserId = userId, DueDate = yesterday, IsCompleted = false },
            new TaskItem { Title = "Future", UserId = userId, DueDate = tomorrow, IsCompleted = false },
            new TaskItem { Title = "Completed", UserId = userId, DueDate = yesterday, IsCompleted = true }
        );
        await context.SaveChangesAsync();
        var mockHub = new Mock<IHubContext<TaskHub>>();
        var mockLogger = new Mock<ILogger<TaskService>>();
        var service = new TaskService(context, mockHub.Object, mockLogger.Object);

        var result = await service.GetOverdueTasksAsync(userId);

        result.Should().HaveCount(1);
        result.First().Title.Should().Be("Overdue");
    }

    [Fact]
    public async Task GetUpcomingTasksAsync_ReturnsTasksWithinDays()
    {
        var context = GetInMemoryDbContext();
        var userId = Guid.NewGuid();
        var now = DateTime.UtcNow;
        context.Tasks.AddRange(
            new TaskItem { Title = "Soon", UserId = userId, DueDate = now.AddDays(3), IsCompleted = false },
            new TaskItem { Title = "Later", UserId = userId, DueDate = now.AddDays(10), IsCompleted = false }
        );
        await context.SaveChangesAsync();
        var mockHub = new Mock<IHubContext<TaskHub>>();
        var mockLogger = new Mock<ILogger<TaskService>>();
        var service = new TaskService(context, mockHub.Object, mockLogger.Object);

        var result = await service.GetUpcomingTasksAsync(userId, 7);

        result.Should().HaveCount(1);
        result.First().Title.Should().Be("Soon");
    }
}
