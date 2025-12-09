using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TaskManagerApi.DTOs;

public class LoginDto
{
    [Required]
    [EmailAddress]
    [DefaultValue("john.doe@example.com")]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    [DefaultValue("SecurePass123!")]
    public string Password { get; set; } = string.Empty;
}
