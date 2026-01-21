using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Moq;
using TaskManagerApi.Data;
using TaskManagerApi.Models;
using TaskManagerApi.Services;
using TaskManagerApi.Hubs;
using FluentAssertions;

public class PaginationTests
{
    private ApplicationDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task GetUserTasksAsync_WithPagination_ReturnsCorrectPage()
    {
        var context = GetInMemoryDbContext();
        var userId = Guid.NewGuid();
        for (int i = 1; i <= 25; i++)
        {
            context.Tasks.Add(new TaskItem { Title = $"Task {i}", UserId = userId });
        }
        await context.SaveChangesAsync();
        var mockHub = new Mock<IHubContext<TaskHub>>();
        var mockLogger = new Mock<ILogger<TaskService>>();
        var service = new TaskService(context, mockHub.Object, mockLogger.Object);
        var parameters = new TaskFilterParameters { PageNumber = 2, PageSize = 10 };

        var result = await service.GetUserTasksAsync(userId, parameters);

        result.Items.Should().HaveCount(10);
        result.CurrentPage.Should().Be(2);
        result.TotalPages.Should().Be(3);
        result.TotalCount.Should().Be(25);
        result.HasPrevious.Should().BeTrue();
        result.HasNext.Should().BeTrue();
    }

    [Fact]
    public async Task GetUserTasksAsync_LastPage_HasNoNext()
    {
        var context = GetInMemoryDbContext();
        var userId = Guid.NewGuid();
        for (int i = 1; i <= 25; i++)
        {
            context.Tasks.Add(new TaskItem { Title = $"Task {i}", UserId = userId });
        }
        await context.SaveChangesAsync();
        var mockHub = new Mock<IHubContext<TaskHub>>();
        var mockLogger = new Mock<ILogger<TaskService>>();
        var service = new TaskService(context, mockHub.Object, mockLogger.Object);
        var parameters = new TaskFilterParameters { PageNumber = 3, PageSize = 10 };

        var result = await service.GetUserTasksAsync(userId, parameters);

        result.Items.Should().HaveCount(5);
        result.HasNext.Should().BeFalse();
        result.HasPrevious.Should().BeTrue();
    }

    [Fact]
    public void PaginationParameters_EnforcesMaxPageSize()
    {
        var parameters = new TaskFilterParameters { PageSize = 200 };

        parameters.PageSize.Should().Be(100);
    }
}
