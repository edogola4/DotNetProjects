# API Versioning Quick Reference

## Current Version
- **API Version**: 1.0
- **Assembly Version**: 1.0.0.0

## Accessing the API

### Method 1: URL Path (Recommended)
```bash
curl https://localhost:5001/api/v1/tasks
curl https://localhost:5001/api/v1/auth/login
curl https://localhost:5001/api/v1/categories
```

### Method 2: Header
```bash
curl https://localhost:5001/api/tasks \
  -H "api-version: 1.0"
```

### Method 3: Media Type
```bash
curl https://localhost:5001/api/tasks \
  -H "Accept: application/json; api-version=1.0"
```

## Version Information

### Check API Version
```bash
curl https://localhost:5001/api/v1/version
```

Response:
```json
{
  "apiVersion": "1.0",
  "assemblyVersion": "1.0.0.0",
  "informationalVersion": "1.0.0",
  "buildDate": "2026-01-27T12:34:56.789Z",
  "frameworkVersion": ".NET 10.0.0"
}
```

### Health Check
```bash
curl https://localhost:5001/api/v1/version/health
```

## Available Endpoints

All endpoints are now versioned:

| Endpoint | Old Route | New Route |
|----------|-----------|-----------|
| Authentication | `/api/auth` | `/api/v1/auth` |
| Tasks | `/api/tasks` | `/api/v1/tasks` |
| Categories | `/api/categories` | `/api/v1/categories` |
| Version Info | *(new)* | `/api/v1/version` |

## Version 1.0 Operations

### Auth Controller
- `POST /api/v1/auth/register` - Register new user
- `POST /api/v1/auth/login` - Login user
- `POST /api/v1/auth/refresh` - Refresh token

### Tasks Controller
- `GET /api/v1/tasks` - List all tasks
- `GET /api/v1/tasks/{id}` - Get specific task
- `POST /api/v1/tasks` - Create task
- `PUT /api/v1/tasks/{id}` - Update task
- `DELETE /api/v1/tasks/{id}` - Delete task

### Categories Controller
- `GET /api/v1/categories` - List categories
- `GET /api/v1/categories/{id}` - Get category
- `POST /api/v1/categories` - Create category
- `PUT /api/v1/categories/{id}` - Update category
- `DELETE /api/v1/categories/{id}` - Delete category

## Version Control

### Update API Version
Edit `Directory.Build.props`:
```xml
<Version>1.1.0</Version>
<AssemblyVersion>1.1.0.0</AssemblyVersion>
```

### Add New Version (v2.0)
1. Create v2 controllers with `[ApiVersion("2.0")]`
2. Update `Program.cs` to register new version
3. Existing v1.0 controllers remain unchanged
4. Both versions run side-by-side

## Swagger Documentation

- **URL**: `https://localhost:5001/swagger`
- **OpenAPI Schema**: `https://localhost:5001/swagger/v1/swagger.json`
- **Try it out**: Use Swagger UI to test endpoints

## Documentation Files

- **[VERSIONING.md](../VERSIONING.md)** - Complete versioning guide
- **[VERSIONING_IMPLEMENTATION.md](../VERSIONING_IMPLEMENTATION.md)** - Implementation details

## Backward Compatibility

- Default version is 1.0 (if no version specified, v1.0 is used)
- Old URLs may still work during transition period
- Plan migration strategy for API consumers
