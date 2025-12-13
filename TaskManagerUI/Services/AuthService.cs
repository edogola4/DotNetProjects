using Microsoft.JSInterop;
using System.Text.Json;
using TaskManagerUI.DTOs;

namespace TaskManagerUI.Services;

public class AuthService
{
    private readonly IJSRuntime _jsRuntime;
    private readonly ApiService _apiService;
    private const string TokenKey = "authToken";
    private const string UserKey = "userData";

    public AuthService(IJSRuntime jsRuntime, ApiService apiService)
    {
        _jsRuntime = jsRuntime;
        _apiService = apiService;
    }

    public event Action? AuthStateChanged;

    public async Task<bool> LoginAsync(LoginDto loginDto)
    {
        Console.WriteLine($"Attempting login for: {loginDto.Email}");
        var response = await _apiService.LoginAsync(loginDto);
        if (response != null)
        {
            Console.WriteLine($"Login successful, saving token and user data");
            await SaveAuthDataAsync(response);
            _apiService.SetAuthToken(response.Token);
            AuthStateChanged?.Invoke();
            return true;
        }
        Console.WriteLine("Login failed - no response from API");
        return false;
    }

    public async Task<(bool success, string? error)> RegisterAsync(RegisterDto registerDto)
    {
        var (response, error) = await _apiService.RegisterAsync(registerDto);
        if (response != null)
        {
            await SaveAuthDataAsync(response);
            _apiService.SetAuthToken(response.Token);
            AuthStateChanged?.Invoke();
            return (true, null);
        }
        return (false, error);
    }

    public async Task LogoutAsync()
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", TokenKey);
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", UserKey);
        _apiService.ClearAuthToken();
        AuthStateChanged?.Invoke();
    }

    public async Task<bool> IsAuthenticatedAsync()
    {
        var token = await GetTokenAsync();
        return !string.IsNullOrEmpty(token);
    }

    public async Task<string?> GetTokenAsync()
    {
        return await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", TokenKey);
    }

    public async Task<AuthResponseDto?> GetUserDataAsync()
    {
        var userData = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", UserKey);
        return string.IsNullOrEmpty(userData) 
            ? null 
            : JsonSerializer.Deserialize<AuthResponseDto>(userData);
    }

    public async Task InitializeAsync()
    {
        var token = await GetTokenAsync();
        Console.WriteLine($"AuthService.InitializeAsync - Token: {(string.IsNullOrEmpty(token) ? "NULL/EMPTY" : "EXISTS")}");
        if (!string.IsNullOrEmpty(token))
        {
            _apiService.SetAuthToken(token);
            Console.WriteLine("Token set on ApiService");
        }
        else
        {
            Console.WriteLine("No token found in localStorage");
        }
    }

    private async Task SaveAuthDataAsync(AuthResponseDto authResponse)
    {
        Console.WriteLine($"Saving auth data to localStorage");
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", TokenKey, authResponse.Token);
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", UserKey, JsonSerializer.Serialize(authResponse));
        Console.WriteLine($"Auth data saved successfully");
    }
}