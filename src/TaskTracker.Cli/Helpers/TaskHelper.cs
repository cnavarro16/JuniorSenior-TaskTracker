using EasyConsole;
using System.Security.Cryptography;
using TaskTracker.Cli.Models;

namespace TaskTracker.Cli.Helpers
{
    internal class TaskHelper
    {
        public static List<TaskItem> SeedTasks()
        {
            var tasks = new List<TaskItem>();
            tasks.Add(new TaskItem()
            {
                Description = "Test Task 1",
                DueDate = DateTime.Now.AddDays(20),
                IsCompleted = false
            });
            tasks.Add(new TaskItem()
            {
                Description = "Test Task 2",
                DueDate = DateTime.Now.AddDays(5),
                IsCompleted = false
            });
            tasks.Add(new TaskItem()
            {
                Description = "Test Task 3",
                DueDate = DateTime.Now.AddDays(50),
                IsCompleted = false
            });
            return tasks;
        }

        public static void AddTask(List<TaskItem> tasks)
        {
            bool dateIsValid;
            DateTime dateValue;
            Console.Clear();
            var taskDescription = string.Empty;

            do
            {
                Console.Write("Enter a task description: ");
                taskDescription = Console.ReadLine();
                if (string.IsNullOrEmpty(taskDescription))
                    Output.WriteLine(ConsoleColor.Red, "No task description provided, please try again...");
            }
            while (string.IsNullOrEmpty(taskDescription));

            do
            {
                Console.Write("\nEnter a task due date: ");
                var taskDueDate = Console.ReadLine();
                dateIsValid = DateTime.TryParse(taskDueDate, out dateValue);
                if (!dateIsValid)
                    Output.WriteLine(ConsoleColor.Red, "Invalid due date format provided, please try again...");
            }
            while (!dateIsValid);

            tasks.Add(new TaskItem()
            {
                Description = taskDescription,
                DueDate = dateValue,
                IsCompleted = false
            });

            Console.WriteLine($"\nTask added to task list...");
            ShowReturnToMenuText();
        }

        public static void GetTaskList(List<TaskItem> tasks)
        {
            if (tasks.Any())
            {
                Console.WriteLine("Task List:");
                var i = 0;
                foreach (var task in tasks)
                {
                    i++;
                    var checkbox = task.IsCompleted ? "[X]" : "[ ]";
                    Console.WriteLine($"{i} - {checkbox} {task.Description} (Due Date: {task.DueDate.ToString("dd-MM-yy")})");
                }
            }
            else
            {
                Console.WriteLine("No tasks in the system...");
            }
        }
        public static void ListTasks(List<TaskItem> tasks)
        {
            Console.Clear();
            GetTaskList(tasks);
            ShowReturnToMenuText();
        }

        public static void MarkTaskComplete(List<TaskItem> tasks)
        {
            Console.Clear();
            GetTaskList(tasks);
            Console.Write("Enter id for task to mark as complete: ");
            var choice = Console.ReadLine();
            var id = int.Parse(choice) - 1;
            var task = tasks[id];
            if (!task.IsCompleted)
            {
                task.IsCompleted = true;
            }
            ListTasks(tasks);
        }

        private static void ShowReturnToMenuText()
        {
            Console.WriteLine("\nPress ENTER to return to main menu...");
            Console.ReadLine();
        }
    }
}
