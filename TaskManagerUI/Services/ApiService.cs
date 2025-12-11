using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using TaskManagerUI.DTOs;

namespace TaskManagerUI.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    public void SetAuthToken(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", token);
    }

    public void ClearAuthToken()
    {
        _httpClient.DefaultRequestHeaders.Authorization = null;
    }

    // Auth endpoints
    public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginDto, _jsonOptions);
        return response.IsSuccessStatusCode 
            ? await response.Content.ReadFromJsonAsync<AuthResponseDto>(_jsonOptions)
            : null;
    }

    public async Task<AuthResponseDto?> RegisterAsync(RegisterDto registerDto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/auth/register", registerDto, _jsonOptions);
        return response.IsSuccessStatusCode 
            ? await response.Content.ReadFromJsonAsync<AuthResponseDto>(_jsonOptions)
            : null;
    }

    // Task endpoints
    public async Task<List<TaskResponseDto>> GetTasksAsync()
    {
        var response = await _httpClient.GetAsync("api/tasks");
        return response.IsSuccessStatusCode 
            ? await response.Content.ReadFromJsonAsync<List<TaskResponseDto>>(_jsonOptions) ?? new()
            : new();
    }

    public async Task<TaskResponseDto?> GetTaskAsync(Guid id)
    {
        var response = await _httpClient.GetAsync($"api/tasks/{id}");
        return response.IsSuccessStatusCode 
            ? await response.Content.ReadFromJsonAsync<TaskResponseDto>(_jsonOptions)
            : null;
    }

    public async Task<TaskResponseDto?> CreateTaskAsync(CreateTaskDto createTaskDto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/tasks", createTaskDto, _jsonOptions);
        return response.IsSuccessStatusCode 
            ? await response.Content.ReadFromJsonAsync<TaskResponseDto>(_jsonOptions)
            : null;
    }

    public async Task<bool> DeleteTaskAsync(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"api/tasks/{id}");
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> CompleteTaskAsync(Guid id)
    {
        var response = await _httpClient.PatchAsync($"api/tasks/{id}/complete", null);
        return response.IsSuccessStatusCode;
    }

    public async Task<dynamic?> GetTaskStatsAsync()
    {
        var response = await _httpClient.GetAsync("api/tasks/stats");
        return response.IsSuccessStatusCode 
            ? await response.Content.ReadFromJsonAsync<dynamic>(_jsonOptions)
            : null;
    }
}