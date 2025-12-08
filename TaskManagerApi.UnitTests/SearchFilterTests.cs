using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Data;
using TaskManagerApi.Models;
using TaskManagerApi.Services;
using FluentAssertions;

namespace TaskManagerApi.UnitTests;

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
        context.Tasks.AddRange(
            new TaskItem { Title = "Meeting with client", UserId = 1 },
            new TaskItem { Title = "Write report", UserId = 1 }
        );
        await context.SaveChangesAsync();
        var service = new TaskService(context);
        var parameters = new TaskFilterParameters { Search = "meeting" };

        var result = await service.GetUserTasksAsync(1, parameters);

        result.Items.Should().HaveCount(1);
        result.Items.First().Title.Should().Contain("Meeting");
    }

    [Fact]
    public async Task GetUserTasksAsync_FilterByCompleted_ReturnsOnlyCompleted()
    {
        var context = GetInMemoryDbContext();
        context.Tasks.AddRange(
            new TaskItem { Title = "Task1", UserId = 1, IsCompleted = true },
            new TaskItem { Title = "Task2", UserId = 1, IsCompleted = false }
        );
        await context.SaveChangesAsync();
        var service = new TaskService(context);
        var parameters = new TaskFilterParameters { IsCompleted = true };

        var result = await service.GetUserTasksAsync(1, parameters);

        result.Items.Should().HaveCount(1);
        result.Items.First().IsCompleted.Should().BeTrue();
    }

    [Fact]
    public async Task GetUserTasksAsync_FilterByPriority_ReturnsMatchingPriority()
    {
        var context = GetInMemoryDbContext();
        context.Tasks.AddRange(
            new TaskItem { Title = "Task1", UserId = 1, Priority = Priority.High },
            new TaskItem { Title = "Task2", UserId = 1, Priority = Priority.Low }
        );
        await context.SaveChangesAsync();
        var service = new TaskService(context);
        var parameters = new TaskFilterParameters { Priority = Priority.High };

        var result = await service.GetUserTasksAsync(1, parameters);

        result.Items.Should().HaveCount(1);
        result.Items.First().Priority.Should().Be(Priority.High);
    }

    [Fact]
    public async Task GetUserTasksAsync_FilterByDateRange_ReturnsTasksInRange()
    {
        var context = GetInMemoryDbContext();
        var today = DateTime.UtcNow.Date;
        context.Tasks.AddRange(
            new TaskItem { Title = "Task1", UserId = 1, DueDate = today.AddDays(5) },
            new TaskItem { Title = "Task2", UserId = 1, DueDate = today.AddDays(15) }
        );
        await context.SaveChangesAsync();
        var service = new TaskService(context);
        var parameters = new TaskFilterParameters 
        { 
            DueDateFrom = today,
            DueDateTo = today.AddDays(10)
        };

        var result = await service.GetUserTasksAsync(1, parameters);

        result.Items.Should().HaveCount(1);
        result.Items.First().Title.Should().Be("Task1");
    }

    [Fact]
    public async Task GetUserTasksAsync_SortByTitle_ReturnsSortedTasks()
    {
        var context = GetInMemoryDbContext();
        context.Tasks.AddRange(
            new TaskItem { Title = "Zebra", UserId = 1 },
            new TaskItem { Title = "Apple", UserId = 1 }
        );
        await context.SaveChangesAsync();
        var service = new TaskService(context);
        var parameters = new TaskFilterParameters { SortBy = "title", SortOrder = "asc" };

        var result = await service.GetUserTasksAsync(1, parameters);

        result.Items.First().Title.Should().Be("Apple");
        result.Items.Last().Title.Should().Be("Zebra");
    }

    [Fact]
    public async Task GetUserTasksAsync_CombinedFilters_ReturnsCorrectResults()
    {
        var context = GetInMemoryDbContext();
        context.Tasks.AddRange(
            new TaskItem { Title = "Urgent meeting", UserId = 1, Priority = Priority.High, IsCompleted = false },
            new TaskItem { Title = "Regular task", UserId = 1, Priority = Priority.Low, IsCompleted = false },
            new TaskItem { Title = "Urgent report", UserId = 1, Priority = Priority.High, IsCompleted = true }
        );
        await context.SaveChangesAsync();
        var service = new TaskService(context);
        var parameters = new TaskFilterParameters 
        { 
            Search = "urgent",
            Priority = Priority.High,
            IsCompleted = false
        };

        var result = await service.GetUserTasksAsync(1, parameters);

        result.Items.Should().HaveCount(1);
        result.Items.First().Title.Should().Be("Urgent meeting");
    }
}
