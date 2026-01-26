namespace TaskManagerApi.Models;

/// <summary>
/// Represents extended user profile information.
/// </summary>
public class UserProfile
{
    /// <summary>
    /// Unique identifier for the profile (matches User.Id).
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// User's first name.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;
    
    /// <summary>
    /// User's last name.
    /// </summary>
    public string LastName { get; set; } = string.Empty;
    
    /// <summary>
    /// User's phone number.
    /// </summary>
    public string? PhoneNumber { get; set; }
    
    /// <summary>
    /// User's timezone for date/time display.
    /// </summary>
    public string TimeZone { get; set; } = "UTC";
    
    /// <summary>
    /// User's preferred date format.
    /// </summary>
    public string DateFormat { get; set; } = "yyyy-MM-dd";
    
    /// <summary>
    /// User's avatar/profile picture URL.
    /// </summary>
    public string? AvatarUrl { get; set; }
    
    /// <summary>
    /// User's bio or description.
    /// </summary>
    public string? Bio { get; set; }
    
    /// <summary>
    /// Date and time when the profile was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; }
    
    /// <summary>
    /// Navigation property to the user.
    /// </summary>
    public User User { get; set; } = null!;
}