using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using TaskManagerApi.Data;
using TaskManagerApi.DTOs;
using TaskManagerApi.Services;
using TaskManagerApi.Configuration;
using FluentAssertions;

public class AuthServiceTests
{
    private ApplicationDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new ApplicationDbContext(options);
    }

    private IOptions<JwtSettings> GetJwtOptions()
    {
        var jwtSettings = new JwtSettings
        {
            Secret = "test-secret-key-min-32-characters-long",
            Issuer = "TestIssuer",
            Audience = "TestAudience",
            ExpirationMinutes = 60
        };
        return Options.Create(jwtSettings);
    }

    [Fact]
    public async Task RegisterAsync_WithValidData_ReturnsAuthResponse()
    {
        var context = GetInMemoryDbContext();
        var jwtOptions = GetJwtOptions();
        var service = new AuthService(context, jwtOptions);
        var registerDto = new RegisterDto
        {
            Username = "testuser",
            Email = "test@example.com",
            Password = "password123"
        };

        var result = await service.RegisterAsync(registerDto);

        result.Should().NotBeNull();
        result!.Username.Should().Be("testuser");
        result.Email.Should().Be("test@example.com");
        result.Token.Should().NotBeEmpty();
    }

    [Fact]
    public async Task RegisterAsync_WithDuplicateEmail_ReturnsNull()
    {
        var context = GetInMemoryDbContext();
        var jwtOptions = GetJwtOptions();
        var service = new AuthService(context, jwtOptions);
        var registerDto = new RegisterDto
        {
            Username = "testuser",
            Email = "test@example.com",
            Password = "password123"
        };

        await service.RegisterAsync(registerDto);
        var result = await service.RegisterAsync(registerDto);

        result.Should().BeNull();
    }

    [Fact]
    public async Task LoginAsync_WithValidCredentials_ReturnsAuthResponse()
    {
        var context = GetInMemoryDbContext();
        var jwtOptions = GetJwtOptions();
        var service = new AuthService(context, jwtOptions);
        var registerDto = new RegisterDto
        {
            Username = "testuser",
            Email = "test@example.com",
            Password = "password123"
        };
        await service.RegisterAsync(registerDto);

        var loginDto = new LoginDto
        {
            Email = "test@example.com",
            Password = "password123"
        };
        var result = await service.LoginAsync(loginDto);

        result.Should().NotBeNull();
        result!.Token.Should().NotBeEmpty();
    }

    [Fact]
    public async Task LoginAsync_WithInvalidPassword_ReturnsNull()
    {
        var context = GetInMemoryDbContext();
        var jwtOptions = GetJwtOptions();
        var service = new AuthService(context, jwtOptions);
        var registerDto = new RegisterDto
        {
            Username = "testuser",
            Email = "test@example.com",
            Password = "password123"
        };
        await service.RegisterAsync(registerDto);

        var loginDto = new LoginDto
        {
            Email = "test@example.com",
            Password = "wrongpassword"
        };
        var result = await service.LoginAsync(loginDto);

        result.Should().BeNull();
    }
}
