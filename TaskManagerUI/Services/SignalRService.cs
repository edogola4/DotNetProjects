using Microsoft.AspNetCore.SignalR.Client;
using TaskManagerUI.DTOs;

namespace TaskManagerUI.Services;

public class SignalRService : IAsyncDisposable
{
    private HubConnection? _hubConnection;
    
    public event Action<TaskResponseDto>? TaskCreated;
    public event Action<TaskResponseDto>? TaskUpdated;
    public event Action<Guid>? TaskDeleted;
    public event Action<string>? ConnectionStatusChanged;

    public async Task StartAsync(string hubUrl, string? accessToken = null)
    {
        var builder = new HubConnectionBuilder()
            .WithUrl(hubUrl, options =>
            {
                if (!string.IsNullOrEmpty(accessToken))
                {
                    options.AccessTokenProvider = () => Task.FromResult(accessToken);
                }
            });

        _hubConnection = builder.Build();

        _hubConnection.On<TaskResponseDto>("TaskCreated", task => TaskCreated?.Invoke(task));
        _hubConnection.On<TaskResponseDto>("TaskUpdated", task => TaskUpdated?.Invoke(task));
        _hubConnection.On<Guid>("TaskDeleted", taskId => TaskDeleted?.Invoke(taskId));

        _hubConnection.Closed += async (error) =>
        {
            ConnectionStatusChanged?.Invoke("Disconnected");
            await Task.Delay(5000);
            await StartConnectionAsync();
        };

        await StartConnectionAsync();
    }

    private async Task StartConnectionAsync()
    {
        if (_hubConnection?.State == HubConnectionState.Disconnected)
        {
            try
            {
                await _hubConnection.StartAsync();
                ConnectionStatusChanged?.Invoke("Connected");
            }
            catch
            {
                ConnectionStatusChanged?.Invoke("Failed to connect");
            }
        }
    }

    public async Task StopAsync()
    {
        if (_hubConnection != null)
        {
            await _hubConnection.StopAsync();
            ConnectionStatusChanged?.Invoke("Disconnected");
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_hubConnection != null)
        {
            await _hubConnection.DisposeAsync();
        }
    }
}