namespace TaskManager.Models;

/// <summary>
/// Represents a single task item.
/// </summary>
public class TaskItem
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }

    public TaskItem() { }

    public TaskItem(int id, string description)
    {
        Id = id;
        Description = description;
        IsCompleted = false;
    }

    public override string ToString()
    {
        string status = IsCompleted ? "✅" : "❌";
        return $"{Id,-3} {status}  {Description}";
    }
}
