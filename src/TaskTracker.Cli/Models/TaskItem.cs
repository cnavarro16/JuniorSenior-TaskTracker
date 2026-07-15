namespace TaskTracker.Cli.Models;

internal class TaskItem
{
    public required string Description { get; set; }
    public required DateTime DueDate { get; set; }
    public bool IsCompleted { get; set; }
}
