using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Moq;
using TaskManagerApi.Data;
using TaskManagerApi.Models;
using TaskManagerApi.Services;
using TaskManagerApi.Hubs;
using FluentAssertions;

public class SearchFilterTests
{
    private ApplicationDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task GetUserTasksAsync_SearchByTitle_ReturnsMatchingTasks()
    {
        var context = GetInMemoryDbContext();
        var userId = Guid.NewGuid();
        context.Tasks.AddRange(
            new TaskItem { Title = "Meeting with client", UserId = userId },
            new TaskItem { Title = "Write report", UserId = userId }
        );
        await context.SaveChangesAsync();
        var mockHub = new Mock<IHubContext<TaskHub>>();
        var mockLogger = new Mock<ILogger<TaskService>>();
        var service = new TaskService(context, mockHub.Object, mockLogger.Object);
        var parameters = new TaskFilterParameters { Search = "meeting" };

        var result = await service.GetUserTasksAsync(userId, parameters);

        result.Items.Should().HaveCount(1);
        result.Items.First().Title.Should().Contain("Meeting");
    }

    [Fact]
    public async Task GetUserTasksAsync_FilterByCompleted_ReturnsOnlyCompleted()
    {
        var context = GetInMemoryDbContext();
        var userId = Guid.NewGuid();
        context.Tasks.AddRange(
            new TaskItem { Title = "Task1", UserId = userId, IsCompleted = true },
            new TaskItem { Title = "Task2", UserId = userId, IsCompleted = false }
        );
        await context.SaveChangesAsync();
        var mockHub = new Mock<IHubContext<TaskHub>>();
        var mockLogger = new Mock<ILogger<TaskService>>();
        var service = new TaskService(context, mockHub.Object, mockLogger.Object);
        var parameters = new TaskFilterParameters { IsCompleted = true };

        var result = await service.GetUserTasksAsync(userId, parameters);

        result.Items.Should().HaveCount(1);
        result.Items.First().IsCompleted.Should().BeTrue();
    }

    [Fact]
    public async Task GetUserTasksAsync_FilterByPriority_ReturnsMatchingPriority()
    {
        var context = GetInMemoryDbContext();
        var userId = Guid.NewGuid();
        context.Tasks.AddRange(
            new TaskItem { Title = "Task1", UserId = userId, Priority = Priority.High },
            new TaskItem { Title = "Task2", UserId = userId, Priority = Priority.Low }
        );
        await context.SaveChangesAsync();
        var mockHub = new Mock<IHubContext<TaskHub>>();
        var mockLogger = new Mock<ILogger<TaskService>>();
        var service = new TaskService(context, mockHub.Object, mockLogger.Object);
        var parameters = new TaskFilterParameters { Priority = Priority.High };

        var result = await service.GetUserTasksAsync(userId, parameters);

        result.Items.Should().HaveCount(1);
        result.Items.First().Priority.Should().Be(Priority.High);
    }

    [Fact]
    public async Task GetUserTasksAsync_FilterByDateRange_ReturnsTasksInRange()
    {
        var context = GetInMemoryDbContext();
        var userId = Guid.NewGuid();
        var today = DateTime.UtcNow.Date;
        context.Tasks.AddRange(
            new TaskItem { Title = "Task1", UserId = userId, DueDate = today.AddDays(5) },
            new TaskItem { Title = "Task2", UserId = userId, DueDate = today.AddDays(15) }
        );
        await context.SaveChangesAsync();
        var mockHub = new Mock<IHubContext<TaskHub>>();
        var mockLogger = new Mock<ILogger<TaskService>>();
        var service = new TaskService(context, mockHub.Object, mockLogger.Object);
        var parameters = new TaskFilterParameters 
        { 
            DueDateFrom = today,
            DueDateTo = today.AddDays(10)
        };

        var result = await service.GetUserTasksAsync(userId, parameters);

        result.Items.Should().HaveCount(1);
        result.Items.First().Title.Should().Be("Task1");
    }

    [Fact]
    public async Task GetUserTasksAsync_SortByTitle_ReturnsSortedTasks()
    {
        var context = GetInMemoryDbContext();
        var userId = Guid.NewGuid();
        context.Tasks.AddRange(
            new TaskItem { Title = "Zebra", UserId = userId },
            new TaskItem { Title = "Apple", UserId = userId }
        );
        await context.SaveChangesAsync();
        var mockHub = new Mock<IHubContext<TaskHub>>();
        var mockLogger = new Mock<ILogger<TaskService>>();
        var service = new TaskService(context, mockHub.Object, mockLogger.Object);
        var parameters = new TaskFilterParameters { SortBy = "title", SortOrder = "asc" };

        var result = await service.GetUserTasksAsync(userId, parameters);

        result.Items.First().Title.Should().Be("Apple");
        result.Items.Last().Title.Should().Be("Zebra");
    }

    [Fact]
    public async Task GetUserTasksAsync_CombinedFilters_ReturnsCorrectResults()
    {
        var context = GetInMemoryDbContext();
        var userId = Guid.NewGuid();
        context.Tasks.AddRange(
            new TaskItem { Title = "Urgent meeting", UserId = userId, Priority = Priority.High, IsCompleted = false },
            new TaskItem { Title = "Regular task", UserId = userId, Priority = Priority.Low, IsCompleted = false },
            new TaskItem { Title = "Urgent report", UserId = userId, Priority = Priority.High, IsCompleted = true }
        );
        await context.SaveChangesAsync();
        var mockHub = new Mock<IHubContext<TaskHub>>();
        var mockLogger = new Mock<ILogger<TaskService>>();
        var service = new TaskService(context, mockHub.Object, mockLogger.Object);
        var parameters = new TaskFilterParameters 
        { 
            Search = "urgent",
            Priority = Priority.High,
            IsCompleted = false
        };

        var result = await service.GetUserTasksAsync(userId, parameters);

        result.Items.Should().HaveCount(1);
        result.Items.First().Title.Should().Be("Urgent meeting");
    }
}
