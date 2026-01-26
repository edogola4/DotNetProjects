using System.ComponentModel.DataAnnotations;

namespace TaskManagerApi.DTOs;

/// <summary>
/// DTO for updating user profile information.
/// </summary>
public class UpdateUserProfileDto
{
    /// <summary>
    /// User's first name.
    /// </summary>
    [StringLength(50)]
    public string? FirstName { get; set; }
    
    /// <summary>
    /// User's last name.
    /// </summary>
    [StringLength(50)]
    public string? LastName { get; set; }
    
    /// <summary>
    /// User's phone number.
    /// </summary>
    [Phone]
    [StringLength(20)]
    public string? PhoneNumber { get; set; }
    
    /// <summary>
    /// User's timezone.
    /// </summary>
    [StringLength(50)]
    public string? TimeZone { get; set; }
    
    /// <summary>
    /// User's preferred date format.
    /// </summary>
    [StringLength(20)]
    public string? DateFormat { get; set; }
    
    /// <summary>
    /// User's bio or description.
    /// </summary>
    [StringLength(500)]
    public string? Bio { get; set; }
}

/// <summary>
/// DTO for user profile response data.
/// </summary>
public class UserProfileResponseDto
{
    /// <summary>
    /// User ID.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Username.
    /// </summary>
    public string Username { get; set; } = string.Empty;
    
    /// <summary>
    /// Email address.
    /// </summary>
    public string Email { get; set; } = string.Empty;
    
    /// <summary>
    /// First name.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;
    
    /// <summary>
    /// Last name.
    /// </summary>
    public string LastName { get; set; } = string.Empty;
    
    /// <summary>
    /// Phone number.
    /// </summary>
    public string? PhoneNumber { get; set; }
    
    /// <summary>
    /// Timezone.
    /// </summary>
    public string TimeZone { get; set; } = "UTC";
    
    /// <summary>
    /// Date format preference.
    /// </summary>
    public string DateFormat { get; set; } = "yyyy-MM-dd";
    
    /// <summary>
    /// Avatar URL.
    /// </summary>
    public string? AvatarUrl { get; set; }
    
    /// <summary>
    /// User bio.
    /// </summary>
    public string? Bio { get; set; }
    
    /// <summary>
    /// Account creation date.
    /// </summary>
    public DateTime CreatedAt { get; set; }
}