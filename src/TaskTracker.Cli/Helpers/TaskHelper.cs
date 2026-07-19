using EasyConsole;
using TaskTracker.Cli.Enums;
using TaskTracker.Cli.Models;

namespace TaskTracker.Cli.Helpers
{
    internal class TaskHelper
    {
        public static List<TaskItem> SeedTasks()
        {
            var tasks = new List<TaskItem>
            {
                new TaskItem(1, "Test Task 1", DateTime.Now.AddDays(20), PriorityLevel.High),
                new TaskItem(2, "Test Task 2", DateTime.Now.AddDays(50), PriorityLevel.Medium),
                new TaskItem(3, "Test Task 3", DateTime.Now.AddDays(35), PriorityLevel.Low)
            };
            return tasks;
        }

        public static void AddTask(List<TaskItem> tasks)
        {
            bool dateIsValid;
            DateTime dateValue;
            Console.Clear();
            var taskDescription = string.Empty;
            var priorityInput = string.Empty;

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

            do
            {
                Console.WriteLine("\nSelect a priority level:");
                Console.WriteLine("1. Low");
                Console.WriteLine("2. Medium");
                Console.WriteLine("3. High");
                Console.Write("Enter your choice (1-3): ");
                priorityInput = Console.ReadLine();
                if (string.IsNullOrEmpty(priorityInput))
                    Output.WriteLine(ConsoleColor.Red, "No priority level provided, please try again...");
            }
            while (string.IsNullOrEmpty(priorityInput));

            var numbericChoice = int.Parse(priorityInput); // Assuming valid value
            var priorityLevel = (PriorityLevel)numbericChoice; // Assuming enum value exists

            tasks.Add(new TaskItem(GetNextId(tasks), taskDescription, dateValue, priorityLevel));
            
            Console.WriteLine($"\nTask added to task list...");
            ShowReturnToMenuText();
        }

        public static void GetTaskList(List<TaskItem> tasks)
        {
            if (tasks.Any())
            {
                Console.WriteLine("Task List:");
                foreach (var task in tasks)
                {
                    var checkbox = task.IsCompleted ? "[X]" : "[ ]";
                    Console.WriteLine($"{task.Id} - {checkbox} {task.Description} (Due Date: {task.DueDate.ToString("dd-MM-yy")} | Priority: {task.Priority})");
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
            var choice = string.Empty;

            do
            {
                Console.Write("\nEnter id for task to mark as complete: ");
                choice = Console.ReadLine();
                if (string.IsNullOrEmpty(choice))
                    Output.WriteLine(ConsoleColor.Red, "No task id provided, please try again...");
            }
            while (string.IsNullOrEmpty(choice));
            

            var taskId = int.Parse(choice); // Assuming valid value
            var task = tasks.FirstOrDefault(t => t.Id == taskId);

            if (task != null)
            {
                task.MarkComplete();
                Console.Clear();
                GetTaskList(tasks);
                Console.WriteLine($"\nTask marked as completed...");
            }
            else
            {
                Console.WriteLine($"Task {taskId} does not exist...");
            }

            ShowReturnToMenuText();
        }

        public static void DeleteTask(List<TaskItem> tasks)
        {
            Console.Clear();
            GetTaskList(tasks);
            var choice = string.Empty;

            do
            {
                Console.Write("\nEnter id for task to delete: ");
                choice = Console.ReadLine();
                if (string.IsNullOrEmpty(choice))
                    Output.WriteLine(ConsoleColor.Red, "No task id provided, please try again...");
            }
            while (string.IsNullOrEmpty(choice));


            var taskId = int.Parse(choice); // Assuming valid value
            var task = tasks.FirstOrDefault(t => t.Id == taskId);
            if(task != null)
            {
                tasks.Remove(task);
                Console.WriteLine($"Task {taskId} has been removed...");
            }
            else
            {
                Console.WriteLine($"Task {taskId} does not exist...");
            }
            
            ShowReturnToMenuText();
        }

        private static void ShowReturnToMenuText()
        {
            Console.WriteLine("\nPress ENTER to return to main menu...");
            Console.ReadLine();
        }

        private static int GetNextId(List<TaskItem> tasks)
        {
            if (!tasks.Any())
                return 1;

            var topTaskId = tasks.OrderByDescending(t => t.Id).Select(i => i.Id).FirstOrDefault();
            return topTaskId + 1;
        }
    }
}
