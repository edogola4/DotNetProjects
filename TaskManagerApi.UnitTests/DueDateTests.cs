using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Data;
using TaskManagerApi.Models;
using TaskManagerApi.Services;
using FluentAssertions;

namespace TaskManagerApi.UnitTests;

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
        var yesterday = DateTime.UtcNow.AddDays(-1);
        var tomorrow = DateTime.UtcNow.AddDays(1);
        context.Tasks.AddRange(
            new TaskItem { Title = "Overdue", UserId = 1, DueDate = yesterday, IsCompleted = false },
            new TaskItem { Title = "Future", UserId = 1, DueDate = tomorrow, IsCompleted = false },
            new TaskItem { Title = "Completed", UserId = 1, DueDate = yesterday, IsCompleted = true }
        );
        await context.SaveChangesAsync();
        var service = new TaskService(context);

        var result = await service.GetOverdueTasksAsync(1);

        result.Should().HaveCount(1);
        result.First().Title.Should().Be("Overdue");
    }

    [Fact]
    public async Task GetUpcomingTasksAsync_ReturnsTasksWithinDays()
    {
        var context = GetInMemoryDbContext();
        var now = DateTime.UtcNow;
        context.Tasks.AddRange(
            new TaskItem { Title = "Soon", UserId = 1, DueDate = now.AddDays(3), IsCompleted = false },
            new TaskItem { Title = "Later", UserId = 1, DueDate = now.AddDays(10), IsCompleted = false }
        );
        await context.SaveChangesAsync();
        var service = new TaskService(context);

        var result = await service.GetUpcomingTasksAsync(1, 7);

        result.Should().HaveCount(1);
        result.First().Title.Should().Be("Soon");
    }
}
