# API Versioning Guide

## Overview

This project implements API versioning following semantic versioning principles (MAJOR.MINOR.PATCH). Versioning is implemented at multiple levels:

### 1. Assembly Versioning
- **File**: `Directory.Build.props`
- **Properties**:
  - `Version`: NuGet package version (e.g., `1.0.0`)
  - `AssemblyVersion`: CLR assembly version (e.g., `1.0.0.0`)
  - `FileVersion`: File version (e.g., `1.0.0.0`)
  - `InformationalVersion`: Informational version (e.g., `1.0.0`)

### 2. API Versioning
- **Library**: `Asp.Versioning.Mvc.ApiExplorer`
- **Default Version**: 1.0
- **Format**: `/api/v{version:apiVersion}/{controller}/{action}`

### 3. Version Information Endpoint
- **Endpoint**: `GET /api/v1/version`
- **Response**: Returns API version details including assembly version, build date, and framework version

## Version Management

### Current Version
- **API Version**: 1.0
- **Assembly Version**: 1.0.0.0
- **Build Date**: Dynamic (based on build time)

### Updating Versions

#### Update Assembly Version
To bump the assembly version, edit `Directory.Build.props`:

```xml
<Version>1.1.0</Version>
<AssemblyVersion>1.1.0.0</AssemblyVersion>
<FileVersion>1.1.0.0</FileVersion>
<InformationalVersion>1.1.0</InformationalVersion>
<VersionPrefix>1.1.0</VersionPrefix>
```

#### Add New API Version
To introduce a new API version:

1. Update `Program.cs` to register the new version:
   ```csharp
   .AddApiVersioning(options =>
   {
       options.DefaultApiVersion = new ApiVersion(1, 0);
       options.AssumeDefaultVersionWhenUnspecified = true;
       options.ReportApiVersions = true;
       options.ApiVersionReader = ApiVersionReader.Combine(
           new UrlSegmentApiVersionReader(),
           new HeaderApiVersionReader("api-version"),
           new MediaTypeApiVersionReader("api-version"));
   })
   ```

2. Register the new version in Swagger:
   ```csharp
   c.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
   {
       Title = "Task Manager API",
       Version = "v2",
       Description = "API for managing tasks, categories, and user accounts"
   });
   ```

3. Create versioned controllers or mark existing actions with the version:
   ```csharp
   [ApiVersion("2.0")]
   [Route("api/v{version:apiVersion}/[controller]")]
   public class TasksController : ControllerBase
   {
       // V2 endpoints
   }
   ```

## API Version Access Methods

The API supports three methods to specify the version:

### 1. URL Path Parameter
```
GET /api/v1/tasks
```

### 2. Header Parameter
```
GET /api/tasks
Headers: api-version: 1.0
```

### 3. Media Type Parameter
```
GET /api/tasks
Headers: Accept: application/json; version=1.0
```

## Versioning Strategy

### Semantic Versioning Rules
- **MAJOR** (1.0): Breaking changes to the API contract
- **MINOR** (x.1): New features that are backward compatible
- **PATCH** (x.x.1): Bug fixes and hotfixes

### When to Version
- **New Version**: When introducing breaking changes or significant new features
- **New Patch**: When fixing bugs without changing the API contract
- **No Change**: When internal refactoring doesn't affect the API

## Version Deprecation

To deprecate a version:

1. Add a deprecation warning header in responses:
   ```csharp
   [Obsolete("This version is deprecated. Please use v2 instead.")]
   [ApiVersion("1.0", Deprecated = true)]
   public class TasksController : ControllerBase { }
   ```

2. Document the deprecation timeline
3. Communicate to API consumers with a sunset date

## Checking API Version

### Version Endpoint
```bash
curl https://localhost:5001/api/v1/version

# Response:
{
  "apiVersion": "1.0",
  "assemblyVersion": "1.0.0.0",
  "informationalVersion": "1.0.0",
  "buildDate": "2026-01-27T00:00:00Z",
  "frameworkVersion": ".NET 10.0.0"
}
```

### Health Check Endpoint
```bash
curl https://localhost:5001/api/v1/version/health

# Response:
{
  "status": "healthy",
  "timestamp": "2026-01-27T12:34:56.789Z",
  "message": "API is running successfully"
}
```

## Build and Release

### Local Build
```bash
dotnet build
```

The version will be automatically set based on `Directory.Build.props`.

### Publishing
```bash
dotnet publish -c Release
```

Version information will be embedded in the generated assemblies.

## Integration Points

### Swagger/OpenAPI
- All versions are documented in Swagger
- Access Swagger UI at `/swagger`
- OpenAPI schema available at `/swagger/v{version}/swagger.json`

### NuGet (if publishing)
The `Version` property in `Directory.Build.props` controls the NuGet package version.

## Backward Compatibility

The current implementation ensures:
- Version 1.0 is the default version when no version is specified
- All controllers default to version 1.0 unless explicitly marked otherwise
- Multiple API versions can coexist in the same codebase

## Best Practices

1. **Version Planning**: Plan versioning strategy before releasing v1.0
2. **Documentation**: Keep API documentation updated with version changes
3. **Deprecation Notice**: Always communicate deprecation timelines
4. **Consumer Support**: Maintain backward compatibility when possible
5. **Testing**: Test different versions with integration tests
6. **Changelog**: Maintain a changelog documenting version differences

## References

- [Asp.Versioning Documentation](https://github.com/dotnet/aspnet-api-versioning)
- [Semantic Versioning](https://semver.org/)
- [OpenAPI Specification](https://spec.openapis.org/)
