namespace TaskManagerApi.Configuration;

/// <summary>
/// Configuration settings for SignalR hubs.
/// </summary>
public class SignalRSettings
{
    /// <summary>
    /// Configuration section name in appsettings.json.
    /// </summary>
    public const string SectionName = "SignalRSettings";

    /// <summary>
    /// Hub endpoint path for task notifications.
    /// </summary>
    public string TaskHubPath { get; set; } = "/hubs/tasks";

    /// <summary>
    /// Query parameter name for access token in WebSocket connections.
    /// </summary>
    public string AccessTokenParameter { get; set; } = "access_token";
}