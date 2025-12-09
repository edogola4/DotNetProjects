using TaskManagerApi.DTOs;

namespace TaskManagerApi.Services;

public interface ICategoryService
{
    Task<CategoryResponseDto> CreateCategoryAsync(Guid userId, CategoryDto dto);
    Task<IEnumerable<CategoryResponseDto>> GetUserCategoriesAsync(Guid userId);
    Task<CategoryResponseDto?> UpdateCategoryAsync(Guid userId, Guid categoryId, CategoryDto dto);
    Task<bool> DeleteCategoryAsync(Guid userId, Guid categoryId);
}
