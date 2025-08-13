using System.Text.Json;
using TaskManager.Models;

namespace TaskManager.Services;

/// <summary>
/// Simple repository to persist tasks to a JSON file.
/// </summary>
public class TaskRepository
{
    private readonly string _filePath;
    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public TaskRepository(string filePath)
    {
        _filePath = filePath;
    }

    /// <summary>
    /// Loads tasks from disk. If the file doesn't exist or is invalid, returns an empty list.
    /// </summary>
    public List<TaskItem> Load()
    {
        try
        {
            if (!File.Exists(_filePath))
                return new List<TaskItem>();

            string json = File.ReadAllText(_filePath);
            var data = JsonSerializer.Deserialize<List<TaskItem>>(json, _jsonOptions);
            return data ?? new List<TaskItem>();
        }
        catch
        {
            return new List<TaskItem>();
        }
    }

    /// <summary>
    /// Saves tasks to disk in JSON format.
    /// </summary>
    public void Save(List<TaskItem> tasks)
    {
        string json = JsonSerializer.Serialize(tasks, _jsonOptions);
        File.WriteAllText(_filePath, json);
    }
}
