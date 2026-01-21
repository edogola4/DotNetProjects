using System.ComponentModel.DataAnnotations;
using TaskManagerApi.Constants;

namespace TaskManagerApi.DTOs;

/// <summary>
/// DTO for user registration.
/// </summary>
public class RegisterDto
{
    /// <summary>
    /// Username (required).
    /// </summary>
    [Required]
    [StringLength(ValidationConstants.User.UsernameMaxLength, MinimumLength = ValidationConstants.User.UsernameMinLength)]
    public string Username { get; set; } = string.Empty;
    
    /// <summary>
    /// Email address (required, must be valid email format).
    /// </summary>
    [Required]
    [EmailAddress]
    [StringLength(ValidationConstants.User.EmailMaxLength)]
    public string Email { get; set; } = string.Empty;
    
    /// <summary>
    /// Password (required, must meet complexity requirements).
    /// </summary>
    [Required]
    [StringLength(ValidationConstants.User.PasswordMaxLength, MinimumLength = ValidationConstants.User.PasswordMinLength)]
    public string Password { get; set; } = string.Empty;
}
