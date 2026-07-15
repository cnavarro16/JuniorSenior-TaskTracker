using EasyConsole;
using Microsoft.Extensions.Configuration;
using TaskTracker.Cli.Helpers;
using TaskTracker.Cli.Models;

namespace TaskTracker.Cli;

internal class Program
{
    static void Main(string[] args)
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        bool seedTasks = config.GetValue<bool>("SeedTasks");
        bool running = true;
        
        List<TaskItem> tasks = seedTasks ? TaskHelper.SeedTasks() : new List<TaskItem>();

        var menu = new Menu()
            .Add("Add a Task", () => TaskHelper.AddTask(tasks))
            .Add("List all tasks", () => TaskHelper.ListTasks(tasks))
            .Add("Mark a task as complete", () => TaskHelper.MarkTaskComplete(tasks))
            .Add("Exit", () => { running = false; });
        do
        {
            Console.Clear();
            menu.Display();
        }
        while (running);
    }
}