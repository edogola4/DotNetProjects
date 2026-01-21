# .NET Projects

A collection of .NET 10 projects demonstrating modern development practices, including RESTful APIs, authentication, and clean architecture patterns.

## 🎯 Project Overview

This repository showcases production-ready .NET development practices including:
- RESTful API design with comprehensive XML documentation
- JWT authentication & authorization with strongly-typed configuration
- Entity Framework Core with PostgreSQL
- SignalR for real-time notifications
- Comprehensive testing (unit & integration)
- Clean architecture patterns with vertical slicing
- Environment-based configuration management
- Cross-platform Linux deployment ready

## 🚀 Features

- ✅ User authentication (register/login with JWT)
- ✅ Task CRUD operations with filtering and pagination
- ✅ Task categories and tags system
- ✅ Advanced search and filtering capabilities
- ✅ Pagination support with metadata headers
- ✅ Due dates and reminders functionality
- ✅ Real-time notifications with SignalR
- ✅ Complete API documentation with XML comments
- ✅ Strongly-typed configuration models
- ✅ Environment variable support for deployment
- ✅ Cross-platform compatibility (Linux deployment ready)

## 🛠️ Tech Stack

- **Framework:** .NET 10
- **Database:** PostgreSQL 14+
- **ORM:** Entity Framework Core 10.0
- **Authentication:** JWT Bearer tokens with strongly-typed settings
- **Real-time:** SignalR with configurable endpoints
- **Documentation:** Swagger/OpenAPI with XML comments
- **Testing:** xUnit, Moq, FluentAssertions
- **Logging:** Serilog with structured logging
- **Configuration:** Strongly-typed settings with environment variable support

## 📋 Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- PostgreSQL 14+
- Visual Studio 2022 / Rider / VS Code
- Git

## 🏃 Quick Start

### 1. Clone the repository
```bash
git clone https://github.com/edogola4/DotNetProjects.git
cd DotNetProjects
```

### 2. Configure environment variables
Create environment variables or update `appsettings.json`:
```bash
# Required environment variables
export DB_HOST="localhost"
export DB_PORT="5432"
export DB_NAME="TaskManagerDb"
export DB_USER="your_username"
export DB_PASSWORD="your_password"
export JWT_SECRET="your-super-secure-jwt-secret-key-min-32-chars"
export JWT_ISSUER="TaskManagerApi"
export JWT_AUDIENCE="TaskManagerClient"
```

### 3. Apply database migrations
```bash
cd TaskManagerApi
dotnet ef database update
```

### 4. Run the application
```bash
dotnet run
```

The API will be available at `https://localhost:5001` (or check console output)

### 5. Explore the API
Navigate to `https://localhost:5001/swagger` to view the interactive API documentation with comprehensive XML comments.

## 📁 Project Structure

```
DotNetProjects/
├── TaskManagerApi/          # Main API project
│   ├── Controllers/         # API endpoints
│   ├── Models/             # Domain entities
│   ├── DTOs/               # Data transfer objects
│   ├── Services/           # Business logic
│   ├── Data/               # DbContext and repositories
│   ├── Hubs/               # SignalR hubs
│   └── Middleware/         # Custom middleware
├── TaskManagerClient/       # SignalR console client
├── TaskManagerApi.UnitTests/       # Unit tests
├── TaskManagerApi.IntegrationTests/ # Integration tests
└── TaskManagerApi.sln      # Solution file
```

## 📝 Configuration

### Environment Variables
The application supports environment-based configuration for deployment:

```bash
# Database Configuration
DB_HOST=localhost
DB_PORT=5432
DB_NAME=TaskManagerDb
DB_USER=your_username
DB_PASSWORD=your_password

# JWT Configuration
JWT_SECRET=your-super-secure-jwt-secret-key-min-32-chars
JWT_ISSUER=TaskManagerApi
JWT_AUDIENCE=TaskManagerClient
JWT_EXPIRATION_MINUTES=60

# CORS Configuration
CORS_ALLOWED_ORIGINS=http://localhost:5000,https://localhost:5001
CORS_ALLOW_ANY_ORIGIN=true
```

### Strongly-Typed Configuration
The application uses strongly-typed configuration models:
- `JwtSettings` - JWT token configuration
- `CorsSettings` - CORS policy settings
- `SignalRSettings` - SignalR hub configuration

### Development vs Production
- **Development**: Uses `appsettings.Development.json` with default values
- **Production**: Uses environment variables for security
- **Docker**: Environment variables passed via docker-compose or Kubernetes

## 🔑 Authentication

This API uses JWT Bearer token authentication with strongly-typed configuration:

1. Register a new user: `POST /api/auth/register`
2. Login: `POST /api/auth/login`
3. Use the returned token in subsequent requests:
   ```
   Authorization: Bearer <your-token>
   ```

### JWT Configuration
- Configurable secret key via environment variables
- Customizable token expiration
- Support for multiple audiences and issuers
- SignalR WebSocket authentication support

## 📚 API Endpoints

### Authentication
- `POST /api/auth/register` - Register new user
- `POST /api/auth/login` - Login and get JWT token

### Tasks
- `GET /api/tasks` - Get all tasks (paginated, filterable)
- `GET /api/tasks/{id}` - Get specific task
- `POST /api/tasks` - Create new task
- `PUT /api/tasks/{id}` - Update task
- `DELETE /api/tasks/{id}` - Delete task
- `PATCH /api/tasks/{id}/complete` - Mark task as complete

### Categories
- `GET /api/categories` - Get all categories
- `POST /api/categories` - Create category
- `PUT /api/categories/{id}` - Update category
- `DELETE /api/categories/{id}` - Delete category

See `/swagger` for complete API documentation.

## 📊 Logging

The API includes structured logging with Serilog for comprehensive monitoring and debugging.

### Features
- **Console logging** - Real-time logs during development
- **File logging** - Daily rolling log files in `logs/` directory
- **Structured data** - JSON context with request IDs, user IDs, and timestamps
- **Configurable levels** - Different log levels for development and production

### View Logs
```bash
# Real-time console logs
dotnet watch run

# View log files
tail -f TaskManagerApi/logs/taskmanager-*.log
```

### Sample Log Output
```
2025-12-10 20:38:57.633 +03:00 [INF] Task created: "019b0958-4051-7eb2-8620-b8e4f0eb5c9a" by user "019b0955-bb60-74db-ada7-9b15d5c9ee57"
```

## 🔔 Real-time Notifications

The API includes SignalR for real-time task notifications.

### SignalR Hub Endpoint
- **WebSocket:** `ws://localhost:5064/hubs/tasks`
- **HTTPS:** `wss://localhost:5064/hubs/tasks`

### Events
- `TaskCreated` - Fired when a task is created
- `TaskUpdated` - Fired when a task is updated
- `TaskDeleted` - Fired when a task is deleted

### Test with Console Client
```bash
cd TaskManagerClient
dotnet run
# Paste your JWT token when prompted
```

## 📸 Screenshots

### API Documentation (Swagger UI)
*[Screenshot placeholder - Add Swagger UI interface]*

### Real-time SignalR Notifications
*[Screenshot placeholder - Add console client showing real-time notifications]*

### Structured Logging Output
*[Screenshot placeholder - Add log output showing structured data]*

### API Response Examples
*[Screenshot placeholder - Add sample JSON responses]*

## 🧪 Testing

### Run all tests
```bash
dotnet test
```

### Run with coverage
```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
```

## 🐳 Docker Support

### Build and run with Docker Compose
```bash
docker-compose up -d
```

This will start:
- API on port 5000
- PostgreSQL on port 5432

## 📝 API Endpoints Reference

*[Detailed endpoint documentation placeholder - Add comprehensive API reference]*

## 📊 Development Roadmap

- [x] Sprint 1: Foundation & Authentication (Week 1) ✅
  - [x] Issue #1: Initial Project Setup ✅
  - [x] Issue #2: Database Design & EF Core Setup ✅
  - [x] Issue #3: JWT Authentication Infrastructure ✅
- [x] Sprint 2: Core Features (Week 2) ✅
  - [x] Issue #4: Task CRUD Operations ✅
  - [x] Issue #5: Pagination Implementation ✅
  - [x] Issue #6: Categories & Tags System ✅
  - [x] Issue #7: Search & Advanced Filtering ✅
- [x] Sprint 3: Enhancement & Polish (Week 3) ✅
  - [x] Issue #8: Due Date Management ✅

**Project Status:** Production Ready 🚀

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License - see [LICENSE](LICENSE) file for details.

## 📧 Contact

Project Link: [https://github.com/edogola4/DotNetProjects](https://github.com/edogola4/DotNetProjects)

## 🙏 Acknowledgments

- Microsoft Documentation
- Tim Corey's .NET tutorials
- Nick Chapsas for clean architecture patterns
- The .NET community

---

**Status:** ✅ Production Ready
**Version:** 3.0.0
**Framework:** .NET 10
**Last Updated:** December 2024
**Test Coverage:** 29 passing tests
**Features:** Hardcoded values removed, XML documentation complete
**Configuration:** Environment variable ready
