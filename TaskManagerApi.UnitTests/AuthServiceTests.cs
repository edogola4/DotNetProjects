using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using TaskManagerApi.Data;
using TaskManagerApi.DTOs;
using TaskManagerApi.Services;
using FluentAssertions;

namespace TaskManagerApi.UnitTests;

public class AuthServiceTests
{
    private ApplicationDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new ApplicationDbContext(options);
    }

    private IConfiguration GetConfiguration()
    {
        var config = new Dictionary<string, string>
        {
            {"JwtSettings:Secret", "test-secret-key-min-32-characters-long"},
            {"JwtSettings:Issuer", "TestIssuer"},
            {"JwtSettings:Audience", "TestAudience"},
            {"JwtSettings:ExpirationMinutes", "60"}
        };
        return new ConfigurationBuilder().AddInMemoryCollection(config!).Build();
    }

    [Fact]
    public async Task RegisterAsync_WithValidData_ReturnsAuthResponse()
    {
        var context = GetInMemoryDbContext();
        var config = GetConfiguration();
        var service = new AuthService(context, config);
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
        var config = GetConfiguration();
        var service = new AuthService(context, config);
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
        var config = GetConfiguration();
        var service = new AuthService(context, config);
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
        var config = GetConfiguration();
        var service = new AuthService(context, config);
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
