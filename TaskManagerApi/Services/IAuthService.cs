using TaskManagerApi.DTOs;

namespace TaskManagerApi.Services;

/// <summary>
/// Interface for authentication service operations.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Registers a new user account.
    /// </summary>
    /// <param name="registerDto">User registration data.</param>
    /// <returns>Authentication response with JWT token, or null if registration fails.</returns>
    Task<AuthResponseDto?> RegisterAsync(RegisterDto registerDto);
    
    /// <summary>
    /// Authenticates a user with email and password.
    /// </summary>
    /// <param name="loginDto">User login credentials.</param>
    /// <returns>Authentication response with JWT token, or null if login fails.</returns>
    Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
}
