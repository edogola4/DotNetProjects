using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TaskManagerApi.DTOs;

public class RegisterDto
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    [DefaultValue("bran don")]
    public string Username { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    [DefaultValue("bran.don@example.com")]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    [StringLength(100, MinimumLength = 6)]
    [DefaultValue("SecurePass123!")]
    public string Password { get; set; } = string.Empty;
}
