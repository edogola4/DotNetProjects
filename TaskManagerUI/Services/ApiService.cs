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
        Console.WriteLine($"ApiService.SetAuthToken called with token: {(string.IsNullOrEmpty(token) ? "NULL/EMPTY" : "EXISTS")}");
        _httpClient.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", token);
        Console.WriteLine($"Authorization header set: {_httpClient.DefaultRequestHeaders.Authorization}");
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

    public async Task<(AuthResponseDto? result, string? error)> RegisterAsync(RegisterDto registerDto)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/register", registerDto, _jsonOptions);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>(_jsonOptions);
                return (result, null);
            }
            
            var errorContent = await response.Content.ReadAsStringAsync();
            return (null, errorContent);
        }
        catch (Exception ex)
        {
            return (null, ex.Message);
        }
    }

    // Task endpoints
    public async Task<List<TaskResponseDto>> GetTasksAsync()
    {
        try
        {
            Console.WriteLine("Calling GET api/tasks...");
            var response = await _httpClient.GetAsync("api/tasks");
            Console.WriteLine($"Response status: {response.StatusCode}");
            
            if (response.IsSuccessStatusCode)
            {
                var tasks = await response.Content.ReadFromJsonAsync<List<TaskResponseDto>>(_jsonOptions) ?? new();
                Console.WriteLine($"Retrieved {tasks.Count} tasks");
                return tasks;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error getting tasks: {errorContent}");
                return new();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception getting tasks: {ex.Message}");
            return new();
        }
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
        try
        {
            Console.WriteLine($"Creating task: {createTaskDto.Title}");
            var response = await _httpClient.PostAsJsonAsync("api/tasks", createTaskDto, _jsonOptions);
            Console.WriteLine($"Create task response status: {response.StatusCode}");
            
            if (response.IsSuccessStatusCode)
            {
                var task = await response.Content.ReadFromJsonAsync<TaskResponseDto>(_jsonOptions);
                Console.WriteLine($"Task created successfully: {task?.Id}");
                return task;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error creating task: {errorContent}");
                return null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception creating task: {ex.Message}");
            return null;
        }
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

    public async Task<TaskStatsDto?> GetTaskStatsAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/tasks/stats");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<TaskStatsDto>(_jsonOptions);
            }
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting task stats: {ex.Message}");
            return null;
        }
    }
}