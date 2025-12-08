using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Data;
using TaskManagerApi.Models;
using TaskManagerApi.Services;
using FluentAssertions;

namespace TaskManagerApi.UnitTests;

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
        var task = new TaskItem { Title = "Test", UserId = 1 };
        context.Tasks.Add(task);
        await context.SaveChangesAsync();
        var service = new TaskService(context);

        await service.AddTagsToTaskAsync(1, task.Id, new List<string> { "urgent", "work" });

        var updatedTask = await context.Tasks.Include(t => t.Tags).FirstAsync(t => t.Id == task.Id);
        updatedTask.Tags.Should().HaveCount(2);
    }

    [Fact]
    public async Task RemoveTagsFromTaskAsync_RemovesTags()
    {
        var context = GetInMemoryDbContext();
        var tag = new Tag { Name = "urgent" };
        var task = new TaskItem { Title = "Test", UserId = 1 };
        task.Tags.Add(tag);
        context.Tasks.Add(task);
        await context.SaveChangesAsync();
        var service = new TaskService(context);

        await service.RemoveTagsFromTaskAsync(1, task.Id, new List<string> { "urgent" });

        var updatedTask = await context.Tasks.Include(t => t.Tags).FirstAsync(t => t.Id == task.Id);
        updatedTask.Tags.Should().BeEmpty();
    }

    [Fact]
    public async Task GetUserTasksAsync_FiltersByCategory()
    {
        var context = GetInMemoryDbContext();
        context.Tasks.AddRange(
            new TaskItem { Title = "Task1", UserId = 1, CategoryId = 1 },
            new TaskItem { Title = "Task2", UserId = 1, CategoryId = 2 }
        );
        await context.SaveChangesAsync();
        var service = new TaskService(context);
        var parameters = new PaginationParameters();

        var result = await service.GetUserTasksAsync(1, parameters, categoryId: 1);

        result.Items.Should().HaveCount(1);
        result.Items.First().Title.Should().Be("Task1");
    }
}
