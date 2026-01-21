using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Data;
using TaskManagerApi.DTOs;
using TaskManagerApi.Models;
using TaskManagerApi.Services;
using FluentAssertions;

namespace TaskManagerApi.UnitTests;

public class CategoryServiceTests
{
    private ApplicationDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task CreateCategoryAsync_CreatesCategory()
    {
        var context = GetInMemoryDbContext();
        var service = new CategoryService(context);
        var userId = Guid.NewGuid();
        var dto = new CategoryDto { Name = "Work" };

        var result = await service.CreateCategoryAsync(userId, dto);

        result.Should().NotBeNull();
        result.Name.Should().Be("Work");
    }

    [Fact]
    public async Task GetUserCategoriesAsync_ReturnsOnlyUserCategories()
    {
        var context = GetInMemoryDbContext();
        var userId1 = Guid.NewGuid();
        var userId2 = Guid.NewGuid();
        context.Categories.AddRange(
            new Category { Name = "User1 Cat", UserId = userId1 },
            new Category { Name = "User2 Cat", UserId = userId2 }
        );
        await context.SaveChangesAsync();
        var service = new CategoryService(context);

        var result = await service.GetUserCategoriesAsync(userId1);

        result.Should().HaveCount(1);
        result.First().Name.Should().Be("User1 Cat");
    }

    [Fact]
    public async Task DeleteCategoryAsync_FailsIfHasTasks()
    {
        var context = GetInMemoryDbContext();
        var userId = Guid.NewGuid();
        var category = new Category { Name = "Work", UserId = userId };
        context.Categories.Add(category);
        await context.SaveChangesAsync();
        context.Tasks.Add(new TaskItem { Title = "Task", UserId = userId, CategoryId = category.Id });
        await context.SaveChangesAsync();
        var service = new CategoryService(context);

        var result = await service.DeleteCategoryAsync(userId, category.Id);

        result.Should().BeFalse();
    }
}
