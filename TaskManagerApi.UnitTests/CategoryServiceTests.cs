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
        var dto = new CategoryDto { Name = "Work" };

        var result = await service.CreateCategoryAsync(1, dto);

        result.Should().NotBeNull();
        result.Name.Should().Be("Work");
    }

    [Fact]
    public async Task GetUserCategoriesAsync_ReturnsOnlyUserCategories()
    {
        var context = GetInMemoryDbContext();
        context.Categories.AddRange(
            new Category { Name = "User1 Cat", UserId = 1 },
            new Category { Name = "User2 Cat", UserId = 2 }
        );
        await context.SaveChangesAsync();
        var service = new CategoryService(context);

        var result = await service.GetUserCategoriesAsync(1);

        result.Should().HaveCount(1);
        result.First().Name.Should().Be("User1 Cat");
    }

    [Fact]
    public async Task DeleteCategoryAsync_FailsIfHasTasks()
    {
        var context = GetInMemoryDbContext();
        var category = new Category { Name = "Work", UserId = 1 };
        context.Categories.Add(category);
        await context.SaveChangesAsync();
        context.Tasks.Add(new TaskItem { Title = "Task", UserId = 1, CategoryId = category.Id });
        await context.SaveChangesAsync();
        var service = new CategoryService(context);

        var result = await service.DeleteCategoryAsync(1, category.Id);

        result.Should().BeFalse();
    }
}
