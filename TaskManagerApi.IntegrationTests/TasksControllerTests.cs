using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskManagerApi.Data;
using TaskManagerApi.DTOs;
using FluentAssertions;

namespace TaskManagerApi.IntegrationTests;

public class TasksControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public TasksControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
                if (descriptor != null) services.Remove(descriptor);
                
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("TestDb_" + Guid.NewGuid()));
            });
        });
    }

    private async Task<string> GetAuthTokenAsync(HttpClient client)
    {
        var registerDto = new RegisterDto
        {
            Username = "testuser" + Guid.NewGuid().ToString()[..8],
            Email = $"test{Guid.NewGuid().ToString()[..8]}@example.com",
            Password = "password123"
        };
        
        var response = await client.PostAsJsonAsync("/api/auth/register", registerDto);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
        return result!.Token;
    }

    [Fact]
    public async Task CreateTask_WithAuth_ReturnsCreated()
    {
        var client = _factory.CreateClient();
        var token = await GetAuthTokenAsync(client);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var dto = new CreateTaskDto { Title = "Test Task", Description = "Test" };
        var response = await client.PostAsJsonAsync("/api/tasks", dto);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var task = await response.Content.ReadFromJsonAsync<TaskResponseDto>();
        task!.Title.Should().Be("Test Task");
    }

    [Fact]
    public async Task GetTasks_WithAuth_ReturnsOk()
    {
        var client = _factory.CreateClient();
        var token = await GetAuthTokenAsync(client);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.GetAsync("/api/tasks");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetTasks_WithoutAuth_ReturnsUnauthorized()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/api/tasks");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}
