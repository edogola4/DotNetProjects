namespace TaskManagerApi.Models;

/// <summary>
/// Generic paginated list container for API responses.
/// </summary>
/// <typeparam name="T">Type of items in the list.</typeparam>
public class PagedList<T>
{
    /// <summary>
    /// List of items for the current page.
    /// </summary>
    public List<T> Items { get; set; }
    
    /// <summary>
    /// Current page number (1-based).
    /// </summary>
    public int CurrentPage { get; set; }
    
    /// <summary>
    /// Total number of pages available.
    /// </summary>
    public int TotalPages { get; set; }
    
    /// <summary>
    /// Number of items per page.
    /// </summary>
    public int PageSize { get; set; }
    
    /// <summary>
    /// Total number of items across all pages.
    /// </summary>
    public int TotalCount { get; set; }
    
    /// <summary>
    /// Indicates whether there is a previous page.
    /// </summary>
    public bool HasPrevious => CurrentPage > 1;
    
    /// <summary>
    /// Indicates whether there is a next page.
    /// </summary>
    public bool HasNext => CurrentPage < TotalPages;

    /// <summary>
    /// Initializes a new instance of the PagedList class.
    /// </summary>
    /// <param name="items">Items for the current page.</param>
    /// <param name="count">Total count of items.</param>
    /// <param name="pageNumber">Current page number.</param>
    /// <param name="pageSize">Number of items per page.</param>
    public PagedList(List<T> items, int count, int pageNumber, int pageSize)
    {
        Items = items;
        TotalCount = count;
        PageSize = pageSize;
        CurrentPage = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
    }
}
