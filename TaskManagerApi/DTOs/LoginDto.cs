using System.ComponentModel.DataAnnotations;
using TaskManagerApi.Constants;

namespace TaskManagerApi.DTOs;

/// <summary>
/// DTO for user login credentials.
/// </summary>
public class LoginDto
{
    /// <summary>
    /// User's email address (required, must be valid email format).
    /// </summary>
    [Required]
    [EmailAddress]
    [StringLength(ValidationConstants.User.EmailMaxLength)]
    public string Email { get; set; } = string.Empty;
    
    /// <summary>
    /// User's password (required).
    /// </summary>
    [Required]
    [StringLength(ValidationConstants.User.PasswordMaxLength, MinimumLength = ValidationConstants.User.PasswordMinLength)]
    public string Password { get; set; } = string.Empty;
}
