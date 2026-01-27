using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Constants;
using TaskManagerApi.DTOs;
using TaskManagerApi.Services;

namespace TaskManagerApi.Controllers;

/// <summary>
/// Controller for user authentication operations.
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    /// <summary>
    /// Initializes a new instance of the AuthController.
    /// </summary>
    /// <param name="authService">Authentication service.</param>
    /// <param name="logger">Logger instance.</param>
    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    /// <summary>
    /// Registers a new user account.
    /// </summary>
    /// <param name="registerDto">User registration data.</param>
    /// <returns>Authentication response with JWT token.</returns>
    /// <response code="200">User registered successfully.</response>
    /// <response code="400">Invalid registration data or user already exists.</response>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        _logger.LogInformation("Register attempt for user: {Username}, {Email}", registerDto?.Username, registerDto?.Email);
        
        if (!ModelState.IsValid || registerDto == null)
        {
            _logger.LogWarning("Registration failed due to invalid model state or null data");
            return BadRequest(ModelState);
        }
        
        try
        {
            var result = await _authService.RegisterAsync(registerDto);
            
            if (result == null)
            {
                _logger.LogWarning("Registration failed - user already exists");
                return BadRequest(new { message = ErrorMessages.Auth.UserAlreadyExists });
            }

            _logger.LogInformation("Registration successful for user: {Username}", registerDto.Username);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Registration error for user: {Username}", registerDto?.Username);
            return BadRequest(new { message = ErrorMessages.Auth.RegistrationFailed });
        }
    }

    /// <summary>
    /// Authenticates a user with email and password.
    /// </summary>
    /// <param name="loginDto">User login credentials.</param>
    /// <returns>Authentication response with JWT token.</returns>
    /// <response code="200">Login successful.</response>
    /// <response code="400">Invalid request data.</response>
    /// <response code="401">Invalid email or password.</response>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var result = await _authService.LoginAsync(loginDto);
        
        if (result == null)
            return Unauthorized(new { message = ErrorMessages.Auth.InvalidCredentials });

        return Ok(result);
    }
}
