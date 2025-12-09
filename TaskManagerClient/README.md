# TaskManager SignalR Client

A .NET console application that demonstrates real-time task notifications using SignalR.

## Features

- Real-time connection to TaskManager API
- Receives instant notifications for:
  - Task creation
  - Task updates
  - Task deletion
- JWT authentication support
- Automatic reconnection

## Prerequisites

- .NET 9 SDK
- Running TaskManagerApi instance

## Usage

1. Start the TaskManagerApi:
```bash
cd ../TaskManagerApi
dotnet run
```

2. Run the client:
```bash
cd TaskManagerClient
dotnet run
```

3. Enter your JWT token when prompted (get it from `/api/auth/login`)

4. The client will display real-time notifications as tasks are created, updated, or deleted

## Events

The client listens for the following SignalR events:

- **TaskCreated** - Displays full task details when a new task is created
- **TaskUpdated** - Displays updated task details when a task is modified
- **TaskDeleted** - Displays task ID when a task is deleted
- **ReceiveTaskNotification** - Displays custom notification messages

## Example Output

```
SignalR Task Manager Client
Enter JWT Token: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
Connected to SignalR hub!
Listening for task notifications... Press any key to exit.
[TaskCreated] {"id":"019b04b2-98cc-7200-b3ea-56858b55177b","title":"Complete project documentation",...}
[TaskUpdated] {"id":"019b04b2-98cc-7200-b3ea-56858b55177b","title":"Updated task title",...}
[TaskDeleted] Task ID: 019b04b2-98cc-7200-b3ea-56858b55177b
```

## Configuration

The client connects to `http://localhost:5064/hubs/tasks` by default. Update the URL in `Program.cs` if your API runs on a different port.
