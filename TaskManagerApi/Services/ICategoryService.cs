using TaskManagerApi.DTOs;

namespace TaskManagerApi.Services;

/// <summary>
/// Interface for category management operations.
/// </summary>
public interface ICategoryService
{
    /// <summary>
    /// Creates a new category for the specified user.
    /// </summary>
    /// <param name="userId">User ID.</param>
    /// <param name="dto">Category creation data.</param>
    /// <returns>Created category details.</returns>
    Task<CategoryResponseDto> CreateCategoryAsync(Guid userId, CategoryDto dto);
    
    /// <summary>
    /// Gets all categories for the specified user.
    /// </summary>
    /// <param name="userId">User ID.</param>
    /// <returns>List of user categories.</returns>
    Task<IEnumerable<CategoryResponseDto>> GetUserCategoriesAsync(Guid userId);
    
    /// <summary>
    /// Updates an existing category for the specified user.
    /// </summary>
    /// <param name="userId">User ID.</param>
    /// <param name="categoryId">Category ID.</param>
    /// <param name="dto">Category update data.</param>
    /// <returns>Updated category details or null if not found.</returns>
    Task<CategoryResponseDto?> UpdateCategoryAsync(Guid userId, Guid categoryId, CategoryDto dto);
    
    /// <summary>
    /// Deletes a category for the specified user if no tasks are associated with it.
    /// </summary>
    /// <param name="userId">User ID.</param>
    /// <param name="categoryId">Category ID.</param>
    /// <returns>True if deleted successfully, false if not found or has associated tasks.</returns>
    Task<bool> DeleteCategoryAsync(Guid userId, Guid categoryId);
}
