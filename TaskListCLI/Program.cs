using TaskManager.Models;
using TaskManager.Services;

namespace TaskManager;

class Program
{
    static readonly string DataFile = "tasks.json";

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var repo = new TaskRepository(DataFile);
        var tasks = repo.Load();

        int nextId = tasks.Count == 0 ? 1 : tasks.Max(t => t.Id) + 1;

        bool exit = false;
        while (!exit)
        {
            Console.Clear();
            PrintHeader();
            PrintMenu();

            Console.Write("Select an option: ");
            string? option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    CreateTask(tasks, ref nextId, repo);
                    break;

                case "2":
                    ListTasks(tasks);
                    break;

                case "3":
                    CompleteTask(tasks, repo);
                    break;

                case "4":
                    DeleteTask(tasks, repo);
                    break;

                case "5":
                    EditTask(tasks, repo);
                    break;

                case "6":
                    SearchTasks(tasks);
                    break;

                case "7":
                    exit = true;
                    break;

                default:
                    Pause("Invalid option. Press any key to continue...");
                    break;
            }
        }
    }

    static void PrintHeader()
    {
        Console.WriteLine("===== TASK MANAGER (.NET 9 / C#) =====");
        Console.WriteLine();
    }

    static void PrintMenu()
    {
        Console.WriteLine("1. Create task");
        Console.WriteLine("2. List tasks");
        Console.WriteLine("3. Mark task as completed");
        Console.WriteLine("4. Delete task");
        Console.WriteLine("5. Edit task description");
        Console.WriteLine("6. Search tasks by keyword");
        Console.WriteLine("7. Exit");
        Console.WriteLine();
    }

    static void CreateTask(List<TaskItem> tasks, ref int nextId, TaskRepository repo)
    {
        Console.Write("Enter task description: ");
        string? description = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(description))
        {
            Pause("⚠ Description cannot be empty.");
            return;
        }

        var task = new TaskItem(nextId++, description.Trim());
        tasks.Add(task);
        repo.Save(tasks);

        Pause("✅ Task created.");
    }

    static void ListTasks(List<TaskItem> tasks)
    {
        Console.WriteLine();
        Console.WriteLine("===== TASK LIST =====");
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks yet.");
        }
        else
        {
            foreach (var t in tasks.OrderBy(t => t.IsCompleted).ThenBy(t => t.Id))
            {
                Console.WriteLine(t);
            }
        }
        Pause();
    }

    static void CompleteTask(List<TaskItem> tasks, TaskRepository repo)
    {
        Console.Write("Enter task ID to complete: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Pause("⚠ Invalid ID.");
            return;
        }

        var task = tasks.FirstOrDefault(t => t.Id == id);
        if (task is null)
        {
            Pause("⚠ Task not found.");
            return;
        }

        if (task.IsCompleted)
        {
            Pause("ℹ Task is already completed.");
            return;
        }

        task.IsCompleted = true;
        repo.Save(tasks);
        Pause("✅ Task marked as completed.");
    }

    static void DeleteTask(List<TaskItem> tasks, TaskRepository repo)
    {
        Console.Write("Enter task ID to delete: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Pause("⚠ Invalid ID.");
            return;
        }

        var task = tasks.FirstOrDefault(t => t.Id == id);
        if (task is null)
        {
            Pause("⚠ Task not found.");
            return;
        }

        tasks.Remove(task);
        repo.Save(tasks);
        Pause("🗑 Task deleted.");
    }

    static void EditTask(List<TaskItem> tasks, TaskRepository repo)
    {
        Console.Write("Enter task ID to edit: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Pause("⚠ Invalid ID.");
            return;
        }

        var task = tasks.FirstOrDefault(t => t.Id == id);
        if (task is null)
        {
            Pause("⚠ Task not found.");
            return;
        }

        Console.WriteLine($"Current description: {task.Description}");
        Console.Write("New description (leave empty to cancel): ");
        string? newDesc = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(newDesc))
        {
            Pause("✋ Edit cancelled.");
            return;
        }

        task.Description = newDesc.Trim();
        repo.Save(tasks);
        Pause("✏️ Task updated.");
    }

    static void SearchTasks(List<TaskItem> tasks)
    {
        Console.Write("Enter keyword: ");
        string? q = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(q))
        {
            Pause("⚠ Keyword cannot be empty.");
            return;
        }

        q = q.Trim();
        var results = tasks
            .Where(t => t.Description.Contains(q, StringComparison.OrdinalIgnoreCase))
            .OrderBy(t => t.IsCompleted)
            .ThenBy(t => t.Id)
            .ToList();

        Console.WriteLine();
        Console.WriteLine($"===== SEARCH: \"{q}\" =====");
        if (results.Count == 0)
        {
            Console.WriteLine("No matches.");
        }
        else
        {
            foreach (var t in results)
                Console.WriteLine(t);
        }
        Pause();
    }

    static void Pause(string? msg = null)
    {
        if (!string.IsNullOrEmpty(msg))
            Console.WriteLine(msg);
        Console.WriteLine();
        Console.Write("Press any key to continue...");
        Console.ReadKey(true);
    }
}
