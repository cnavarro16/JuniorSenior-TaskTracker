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

    private static List<string> SeedTasks()
    {
        List<string> tasks = new List<string>();
        tasks.Add("[%PENDING%] Test Task 1 (Due: 28/9/26)");
        tasks.Add("[%PENDING%] Test Task 2 (Due: 20/9/26)");
        tasks.Add("[%PENDING%] Test Task 3 (Due: 20/1/27)");
        return tasks;
    }

    private static void ShowMenu()
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

    private static void AddTask(List<string> tasks)
    {
        Console.Clear();
        Console.Write("Enter a task description: ");
        var taskDescription = Console.ReadLine();
        Console.Write("Enter a task due date: ");
        var taskDueDate = Console.ReadLine();
        Console.WriteLine($"\nAdding {taskDescription} to task list, with due date of {taskDueDate}");
        tasks.Add($"[%PENDING%] {taskDescription} (Due: {taskDueDate})");
        Console.WriteLine("\nPress ENTER to return to main menu...");
        Console.ReadLine();
    }

    private static void ListTasks(List<string> tasks)
    {
        Console.Clear();
        ShowTasks(tasks);
        Console.WriteLine("\nPress ENTER to return to main menu...");
        Console.ReadLine();
    }

    private static void MarkTaskComplete(List<string> tasks)
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

    private static void ShowTasks(List<string> tasks)
    {
        Console.WriteLine("Task List:");
        var i = 0;
        foreach (var task in tasks)
        {
            i++;
            Console.WriteLine($"{i} - {task.Replace("%PENDING%", " ")}");
        }
    }

    public static void GoToScreen(string? choice, List<string> tasks)
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

    private static void ExitApp()
    {
        Environment.Exit(0);
    }
}