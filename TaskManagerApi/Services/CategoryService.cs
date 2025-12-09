using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Data;
using TaskManagerApi.DTOs;
using TaskManagerApi.Models;

namespace TaskManagerApi.Services;

public class CategoryService : ICategoryService
{
    private readonly ApplicationDbContext _context;

    public CategoryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CategoryResponseDto> CreateCategoryAsync(Guid userId, CategoryDto dto)
    {
        var category = new Category
        {
            Name = dto.Name,
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return MapToDto(category);
    }

    public async Task<IEnumerable<CategoryResponseDto>> GetUserCategoriesAsync(Guid userId)
    {
        var categories = await _context.Categories
            .Where(c => c.UserId == userId)
            .OrderBy(c => c.Name)
            .ToListAsync();

        return categories.Select(MapToDto);
    }

    public async Task<CategoryResponseDto?> UpdateCategoryAsync(Guid userId, Guid categoryId, CategoryDto dto)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == categoryId && c.UserId == userId);

        if (category == null) return null;

        category.Name = dto.Name;
        await _context.SaveChangesAsync();

        return MapToDto(category);
    }

    public async Task<bool> DeleteCategoryAsync(Guid userId, Guid categoryId)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == categoryId && c.UserId == userId);

        if (category == null) return false;

        var hasTasksWithCategory = await _context.Tasks
            .AnyAsync(t => t.CategoryId == categoryId);

        if (hasTasksWithCategory) return false;

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return true;
    }

    private static CategoryResponseDto MapToDto(Category category)
    {
        return new CategoryResponseDto
        {
            Id = category.Id,
            Name = category.Name,
            CreatedAt = category.CreatedAt
        };
    }
}
