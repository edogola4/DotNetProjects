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
        var response = await _apiService.LoginAsync(loginDto);
        if (response != null)
        {
            await SaveAuthDataAsync(response);
            _apiService.SetAuthToken(response.Token);
            AuthStateChanged?.Invoke();
            return true;
        }
        return false;
    }

    public async Task<bool> RegisterAsync(RegisterDto registerDto)
    {
        var response = await _apiService.RegisterAsync(registerDto);
        if (response != null)
        {
            await SaveAuthDataAsync(response);
            _apiService.SetAuthToken(response.Token);
            AuthStateChanged?.Invoke();
            return true;
        }
        return false;
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
        if (!string.IsNullOrEmpty(token))
        {
            _apiService.SetAuthToken(token);
        }
    }

    private async Task SaveAuthDataAsync(AuthResponseDto authResponse)
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", TokenKey, authResponse.Token);
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", UserKey, JsonSerializer.Serialize(authResponse));
    }
}