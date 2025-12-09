# .NET Projects

A collection of .NET 9 projects demonstrating modern development practices, including RESTful APIs, authentication, and clean architecture patterns.

## 🎯 Project Overview

This repository showcases production-ready .NET development practices including:
- RESTful API design
- JWT authentication & authorization
- Entity Framework Core with PostgreSQL
- SignalR for real-time notifications
- Comprehensive testing (unit & integration)
- Clean architecture patterns
- API documentation with Swagger

## 🚀 Features

- ✅ User authentication (register/login with JWT)
- ✅ Task CRUD operations
- ✅ Task categories and tags
- ✅ Advanced search and filtering
- ✅ Pagination support
- ✅ Due dates and reminders
- ✅ Real-time notifications with SignalR
- ✅ Complete API documentation

## 🛠️ Tech Stack

- **Framework:** .NET 9
- **Database:** PostgreSQL 14+
- **ORM:** Entity Framework Core 9.0
- **Authentication:** JWT Bearer tokens
- **Real-time:** SignalR
- **Documentation:** Swagger/OpenAPI
- **Testing:** xUnit, Moq, FluentAssertions
- **Logging:** ASP.NET Core built-in (Serilog optional)

## 📋 Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- PostgreSQL 14+
- Visual Studio 2022 / Rider / VS Code
- Git

## 🏃 Quick Start

### 1. Clone the repository
```bash
git clone https://github.com/edogola4/DotNetProjects.git
cd DotNetProjects
```

### 2. Configure database connection
Update `TaskManagerApi/appsettings.json` with your PostgreSQL connection string:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=TaskManagerDb;Username=your_username;Password=your_password"
  }
}
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
Navigate to `https://localhost:5001/swagger` to view the interactive API documentation.

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

## 🔑 Authentication

This API uses JWT Bearer token authentication. To access protected endpoints:

1. Register a new user: `POST /api/auth/register`
2. Login: `POST /api/auth/login`
3. Use the returned token in subsequent requests:
   ```
   Authorization: Bearer <your-token>
   ```

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
**Version:** 1.0.0
**Last Updated:** December 2024
**Test Coverage:** 29 passing tests
**Story Points Completed:** 31/60
