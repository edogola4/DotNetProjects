namespace TaskManagerApi.DTOs;

public class TaskStatsDto
{
    public int CompletedTasks { get; set; }
    public int PendingTasks { get; set; }
    public int TotalTasks => CompletedTasks + PendingTasks;
}