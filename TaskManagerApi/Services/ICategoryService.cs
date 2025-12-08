using TaskManagerApi.DTOs;

namespace TaskManagerApi.Services;

public interface ICategoryService
{
    Task<CategoryResponseDto> CreateCategoryAsync(int userId, CategoryDto dto);
    Task<IEnumerable<CategoryResponseDto>> GetUserCategoriesAsync(int userId);
    Task<CategoryResponseDto?> UpdateCategoryAsync(int userId, int categoryId, CategoryDto dto);
    Task<bool> DeleteCategoryAsync(int userId, int categoryId);
}
