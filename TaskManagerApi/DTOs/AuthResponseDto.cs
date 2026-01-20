namespace TaskManagerApi.DTOs;

/// <summary>
/// Response DTO containing authentication information after successful login or registration.
/// </summary>
public class AuthResponseDto
{
    /// <summary>
    /// JWT token for authenticating subsequent requests.
    /// </summary>
    public string Token { get; set; } = string.Empty;
    
    /// <summary>
    /// User's display name.
    /// </summary>
    public string Username { get; set; } = string.Empty;
    
    /// <summary>
    /// User's email address.
    /// </summary>
    public string Email { get; set; } = string.Empty;
}
