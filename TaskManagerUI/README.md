# Task Manager UI

A Blazor WebAssembly frontend for the Task Manager API.

## Features

- **Authentication**: Login/Register with JWT tokens
- **Task Management**: Create, view, complete, and delete tasks
- **Real-time Updates**: SignalR integration for live task notifications
- **Responsive Design**: Bootstrap-based UI that works on all devices
- **Priority System**: Visual priority indicators and sorting

## Prerequisites

- .NET 9 SDK
- Task Manager API running on `https://localhost:5001`

## Quick Start

1. **Start the API first**:
   ```bash
   cd ../TaskManagerApi
   dotnet run
   ```

2. **Run the Blazor UI**:
   ```bash
   cd TaskManagerUI
   dotnet run
   ```

3. **Open browser**: Navigate to `https://localhost:5002` (or check console output)

## Usage

1. **Register/Login**: Create an account or login with existing credentials
2. **Create Tasks**: Click "New Task" to add tasks with title, description, priority, and due date
3. **Manage Tasks**: Complete or delete tasks using the action buttons
4. **Real-time Updates**: See live updates when tasks are modified (SignalR connection status shown)

## Architecture

- **Services**: API communication, authentication state, SignalR real-time updates
- **Components**: Reusable Razor components with Bootstrap styling
- **State Management**: Local storage for JWT tokens, reactive UI updates
- **Real-time**: SignalR client for live task notifications

## API Integration

The UI consumes the following API endpoints:
- `POST /api/auth/register` - User registration
- `POST /api/auth/login` - User authentication
- `GET /api/tasks` - Fetch user tasks
- `POST /api/tasks` - Create new task
- `PATCH /api/tasks/{id}/complete` - Mark task complete
- `DELETE /api/tasks/{id}` - Delete task
- `WS /hubs/tasks` - SignalR real-time notifications