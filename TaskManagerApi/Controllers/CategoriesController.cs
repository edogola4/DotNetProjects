using System.Security.Claims;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.DTOs;
using TaskManagerApi.Services;

namespace TaskManagerApi.Controllers;

/// <summary>
/// Controller for category management operations.
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[Authorize]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    /// <summary>
    /// Initializes a new instance of the CategoriesController.
    /// </summary>
    /// <param name="categoryService">Category service.</param>
    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    /// <summary>
    /// Gets the current user's ID from JWT claims.
    /// </summary>
    /// <returns>User ID.</returns>
    private Guid GetUserId() => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    /// <summary>
    /// Creates a new category for the authenticated user.
    /// </summary>
    /// <param name="dto">Category creation data.</param>
    /// <returns>Created category details.</returns>
    /// <response code="201">Category created successfully.</response>
    /// <response code="400">Invalid category data.</response>
    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryDto dto)
    {
        var category = await _categoryService.CreateCategoryAsync(GetUserId(), dto);
        return CreatedAtAction(nameof(GetCategories), new { id = category.Id }, category);
    }

    /// <summary>
    /// Gets all categories for the authenticated user.
    /// </summary>
    /// <returns>List of user categories.</returns>
    /// <response code="200">Categories retrieved successfully.</response>
    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _categoryService.GetUserCategoriesAsync(GetUserId());
        return Ok(categories);
    }

    /// <summary>
    /// Updates an existing category for the authenticated user.
    /// </summary>
    /// <param name="id">Category ID to update.</param>
    /// <param name="dto">Updated category data.</param>
    /// <returns>Updated category details.</returns>
    /// <response code="200">Category updated successfully.</response>
    /// <response code="404">Category not found.</response>
    /// <response code="400">Invalid category data.</response>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] CategoryDto dto)
    {
        var category = await _categoryService.UpdateCategoryAsync(GetUserId(), id, dto);
        return category == null ? NotFound() : Ok(category);
    }

    /// <summary>
    /// Deletes a category for the authenticated user.
    /// </summary>
    /// <param name="id">Category ID to delete.</param>
    /// <returns>No content on successful deletion.</returns>
    /// <response code="204">Category deleted successfully.</response>
    /// <response code="404">Category not found or has associated tasks.</response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        var result = await _categoryService.DeleteCategoryAsync(GetUserId(), id);
        return result ? NoContent() : NotFound(new { message = "Category not found or has associated tasks" });
    }
}
