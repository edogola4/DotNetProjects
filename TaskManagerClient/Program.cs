using Microsoft.AspNetCore.SignalR.Client;

Console.WriteLine("SignalR Task Manager Client");
Console.Write("Enter JWT Token: ");
var token = Console.ReadLine();

var connection = new HubConnectionBuilder()
    .WithUrl("http://localhost:5064/hubs/tasks", options =>
    {
        options.AccessTokenProvider = () => Task.FromResult(token);
    })
    .WithAutomaticReconnect()
    .Build();

connection.On<object>("TaskCreated", task =>
{
    Console.WriteLine($"[TaskCreated] {System.Text.Json.JsonSerializer.Serialize(task)}");
});

connection.On<object>("TaskUpdated", task =>
{
    Console.WriteLine($"[TaskUpdated] {System.Text.Json.JsonSerializer.Serialize(task)}");
});

connection.On<string>("TaskDeleted", taskId =>
{
    Console.WriteLine($"[TaskDeleted] Task ID: {taskId}");
});

connection.On<string>("ReceiveTaskNotification", message =>
{
    Console.WriteLine($"[Notification] {message}");
});

try
{
    await connection.StartAsync();
    Console.WriteLine("Connected to SignalR hub!");
    Console.WriteLine("Listening for task notifications... Press any key to exit.");
    Console.ReadKey();
    await connection.StopAsync();
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
