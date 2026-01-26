using System.ComponentModel;

namespace TaskManagerApi.Models;

/// <summary>
/// Enumeration of task status values for workflow management.
/// </summary>
public enum TaskStatus
{
    /// <summary>
    /// Task has been created but not started.
    /// </summary>
    [Description("Task is created but not yet started")]
    NotStarted = 0,
    
    /// <summary>
    /// Task is currently being worked on.
    /// </summary>
    [Description("Task is in progress")]
    InProgress = 1,
    
    /// <summary>
    /// Task is blocked by external dependencies.
    /// </summary>
    [Description("Task is blocked and cannot proceed")]
    Blocked = 2,
    
    /// <summary>
    /// Task is completed successfully.
    /// </summary>
    [Description("Task has been completed")]
    Completed = 3,
    
    /// <summary>
    /// Task has been cancelled and will not be completed.
    /// </summary>
    [Description("Task has been cancelled")]
    Cancelled = 4
}