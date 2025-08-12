public class TaskManager
{
    private readonly List<TaskItem> _tasks = new();

    public void AddTask(string description, DateTime dueDate)
    {
        _tasks.Add(new TaskItem { Description = description, DueDate = dueDate });
    }

    public void ListTasks()
    {
        if (_tasks.Count == 0)
        {
            Console.WriteLine("No tasks yet!");
            return;
        }

        for (int i = 0; i < _tasks.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_tasks[i]}");
        }
    }

    public void RemoveTask(int index)
    {
        if (index >= 1 && index <= _tasks.Count)
            _tasks.RemoveAt(index - 1);
        else
            Console.WriteLine("Invalid index!");
    }
}