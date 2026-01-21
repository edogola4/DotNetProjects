namespace TaskManagerApi.Configuration;

/// <summary>
/// Configuration settings for Cross-Origin Resource Sharing (CORS).
/// </summary>
public class CorsSettings
{
    /// <summary>
    /// Configuration section name in appsettings.json.
    /// </summary>
    public const string SectionName = "CorsSettings";

    /// <summary>
    /// Policy name for Blazor UI CORS configuration.
    /// </summary>
    public string PolicyName { get; set; } = "BlazorUI";

    /// <summary>
    /// Allowed origins for CORS requests.
    /// </summary>
    public string[] AllowedOrigins { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Whether to allow any origin (development only).
    /// </summary>
    public bool AllowAnyOrigin { get; set; } = false;

    /// <summary>
    /// Whether to allow credentials in CORS requests.
    /// </summary>
    public bool AllowCredentials { get; set; } = true;
}