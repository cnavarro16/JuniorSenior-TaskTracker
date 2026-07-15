namespace TaskTracker.Cli;

internal class Program
{
    static void Main(string[] args)
    {
        bool seedTasks = false;
        string? choice = string.Empty;
        List<string> tasks = seedTasks ? SeedTasks() : new List<string>();

        do
        {
            Console.Clear();
            ShowMenu();
            choice = Console.ReadLine();
            GoToScreen(choice, tasks);
        }
        while (true);
    }

    static List<string> SeedTasks()
    {
        List<string> tasks = new List<string>();
        tasks.Add("[%PENDING%] Test Task 1 (Due: 28/9/26)");
        tasks.Add("[%PENDING%] Test Task 2 (Due: 20/9/26)");
        tasks.Add("[%PENDING%] Test Task 3 (Due: 20/1/27)");
        return tasks;
    }

    static void ShowMenu()
    {
        var menu = """
            Task Manager v1.0

            Available Functionality:
            1. Add a task
            2. List all tasks
            3. Mark a task as complete
            4. Exit

            Please enter a selection: 
            """;
        Console.Clear();
        Console.Write(menu);
    }

    static void AddTask(List<string> tasks)
    {
        Console.Clear();
        Console.Write("Enter a task description: ");
        var taskDescription = Console.ReadLine();
        Console.Write("Enter a task due date: ");
        var taskDueDate = Console.ReadLine();
        tasks.Add($"[%PENDING%] {taskDescription} (Due: {taskDueDate?.Replace("-","/")})");
        Console.WriteLine($"\nTask added to task list...");
        Console.WriteLine("\nPress ENTER to return to main menu...");
        Console.ReadLine();
    }

    static void ListTasks(List<string> tasks)
    {
        Console.Clear();
        ShowTasks(tasks);
        Console.WriteLine("\nPress ENTER to return to main menu...");
        Console.ReadLine();
    }

    static void MarkTaskComplete(List<string> tasks)
    {
        Console.Clear();
        ShowTasks(tasks);
        Console.Write("Enter id for task to mark as complete: ");
        var choice = Console.ReadLine();
        var id = int.Parse(choice) - 1;
        var task = tasks[id];
        if (task.Contains("%PENDING%"))
        {
            tasks[id] = task.Replace("%PENDING%", "X");
        }
        ListTasks(tasks);
    }

    static void ShowTasks(List<string> tasks)
    {
        if(tasks.Any())
        {
            Console.WriteLine("Task List:");
            var i = 0;
            foreach (var task in tasks)
            {
                i++;
                Console.WriteLine($"{i} - {task.Replace("%PENDING%", " ")}");
            }
        }
        else
        {
            Console.WriteLine("No tasks in the system...");
        }
        
    }

    static void GoToScreen(string? choice, List<string> tasks)
    {
        switch (choice)
        {
            case "1":
                AddTask(tasks);
                break;
            case "2":
                ListTasks(tasks); 
                break;
            case "3":
                MarkTaskComplete(tasks);
                break;
            case "4":
                ExitApp();
                break;
            default:
                ShowMenu();
                break;
        }
    }

    static void ExitApp()
    {
        Environment.Exit(0);
    }
}