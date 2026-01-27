using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace TaskManagerApi.Controllers;

/// <summary>
/// Provides API version information.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[ApiVersion("1.0")]
public class VersionController : ControllerBase
{
    /// <summary>
    /// Gets the current API version information.
    /// </summary>
    /// <returns>API version details including major, minor, and build versions.</returns>
    /// <response code="200">Returns the current API version information.</response>
    [HttpGet]
    [SwaggerOperation(Summary = "Get API Version", Description = "Returns information about the current API version")]
    public ActionResult<VersionInfo> GetVersion()
    {
        var assembly = GetType().Assembly;
        var version = assembly.GetName().Version;
        var informationalVersion = assembly
            .GetCustomAttributes(typeof(System.Reflection.AssemblyInformationalVersionAttribute), false)
            .OfType<System.Reflection.AssemblyInformationalVersionAttribute>()
            .FirstOrDefault()?.InformationalVersion ?? "1.0.0";

        return Ok(new VersionInfo
        {
            ApiVersion = "1.0",
            AssemblyVersion = version?.ToString() ?? "1.0.0.0",
            InformationalVersion = informationalVersion,
            BuildDate = System.IO.File.GetLastWriteTime(assembly.Location),
            FrameworkVersion = System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription
        });
    }

    /// <summary>
    /// Gets a health check for the API.
    /// </summary>
    /// <returns>API health status.</returns>
    /// <response code="200">The API is healthy and responding.</response>
    [HttpGet("health")]
    [SwaggerOperation(Summary = "Health Check", Description = "Performs a health check on the API")]
    public ActionResult<object> HealthCheck()
    {
        return Ok(new
        {
            status = "healthy",
            timestamp = DateTime.UtcNow,
            message = "API is running successfully"
        });
    }
}

/// <summary>
/// Represents API version information.
/// </summary>
public class VersionInfo
{
    /// <summary>
    /// Gets or sets the API version in semantic versioning format.
    /// </summary>
    public string? ApiVersion { get; set; }

    /// <summary>
    /// Gets or sets the assembly version.
    /// </summary>
    public string? AssemblyVersion { get; set; }

    /// <summary>
    /// Gets or sets the informational version including pre-release or build metadata.
    /// </summary>
    public string? InformationalVersion { get; set; }

    /// <summary>
    /// Gets or sets the build date and time.
    /// </summary>
    public DateTime BuildDate { get; set; }

    /// <summary>
    /// Gets or sets the .NET framework version.
    /// </summary>
    public string? FrameworkVersion { get; set; }
}
