using TaskManagerApi.Constants;

namespace TaskManagerApi.Models;

/// <summary>
/// Base class for pagination parameters.
/// </summary>
public class PaginationParameters
{
    private int _pageSize = ValidationConstants.Pagination.DefaultPageSize;

    /// <summary>
    /// Page number (1-based).
    /// </summary>
    public int PageNumber { get; set; } = ValidationConstants.Pagination.DefaultPageNumber;

    /// <summary>
    /// Number of items per page.
    /// </summary>
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > ValidationConstants.Pagination.MaxPageSize 
            ? ValidationConstants.Pagination.MaxPageSize 
            : value < ValidationConstants.Pagination.MinPageSize 
                ? ValidationConstants.Pagination.MinPageSize 
                : value;
    }
}
