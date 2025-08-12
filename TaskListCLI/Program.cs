class Program
{
    static void Main()
    {
        var manager = new TaskManager();

        while (true)
        {
            Console.WriteLine("\n1. Add Task\n2. List Tasks\n3. Remove Task\n4. Exit");
            Console.Write("Select an option: ");
            var input = Console.ReadLine();

            if (input == "1")
            {
                Console.Write("Description: ");
                var description = Console.ReadLine();
                Console.Write("Due date (yyyy-mm-dd): ");
                if (DateTime.TryParse(Console.ReadLine(), out var dueDate))
                {
                    manager.AddTask(description, dueDate);
                }
                else
                {
                    Console.WriteLine("Invalid date format!");
                }
            }
            else if (input == "2")
            {
                manager.ListTasks();
            }
            else if (input == "3")
            {
                Console.Write("Task number to remove: ");
                if (int.TryParse(Console.ReadLine(), out var index))
                {
                    manager.RemoveTask(index);
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                }
            }
            else if (input == "4")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid option. Please try again.");
            }
        }
    }
}