using System;
using System.Linq;
using SmartTaskManager.Models;
using SmartTaskManager.Services;

namespace SmartTaskManager
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataService = new DataService();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\nSmart Task Manager");
                Console.WriteLine("1. Add User");
                Console.WriteLine("2. Add Task");
                Console.WriteLine("3. View All Tasks");
                Console.WriteLine("4. Update Task");
                Console.WriteLine("5. Delete Task");
                Console.WriteLine("6. View Logs");
                Console.WriteLine("7. Exit");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddUser(dataService);
                        break;
                    case "2":
                        AddTask(dataService);
                        break;
                    case "3":
                        ViewAllTasks(dataService);
                        break;
                    case "4":
                        UpdateTask(dataService);
                        break;
                    case "5":
                        DeleteTask(dataService);
                        break;
                    case "6":
                        ViewLogs(dataService);
                        break;
                    case "7":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option, try again.");
                        break;
                }
            }
        }

        private static void AddUser(DataService dataService)
        {
            Console.Write("Enter User Name: ");
            var name = Console.ReadLine();
            Console.Write("Enter Email: ");
            var email = Console.ReadLine();

            var user = new User { Name = name, Email = email };
            dataService.AddUser(user);
            dataService.LogAction(user.UserID, "User Added");
            Console.WriteLine("User added successfully!");
        }

        private static void AddTask(DataService dataService)
        {
            Console.Write("Enter Task Title: ");
            var title = Console.ReadLine();
            Console.Write("Enter Task Description: ");
            var description = Console.ReadLine();
            Console.Write("Enter Due Date (yyyy-MM-dd): ");
            var dueDate = DateTime.Parse(Console.ReadLine());
            Console.Write("Enter User ID: ");
            var userId = int.Parse(Console.ReadLine());

            var task = new Task
            {
                Title = title,
                Description = description,
                DueDate = dueDate,
                UserID = userId
            };
            dataService.AddTask(task);
            dataService.LogAction(userId, "Task Added");
            Console.WriteLine("Task added successfully!");
        }

        private static void ViewAllTasks(DataService dataService)
        {
            var tasks = dataService.GetAllTasks();
            foreach (var task in tasks)
            {
                Console.WriteLine($"Task ID: {task.TaskID}, Title: {task.Title}, Due Date: {task.DueDate}, Completed: {task.IsCompleted}");
            }
        }

        private static void UpdateTask(DataService dataService)
        {
            Console.Write("Enter Task ID to update: ");
            var taskId = int.Parse(Console.ReadLine());
            var task = dataService.GetAllTasks().FirstOrDefault(t => t.TaskID == taskId);

            if (task != null)
            {
                Console.Write("Enter new Title (press Enter to keep current): ");
                var title = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(title))
                    task.Title = title;

                Console.Write("Enter new Description (press Enter to keep current): ");
                var description = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(description))
                    task.Description = description;

                Console.Write("Enter new Due Date (yyyy-MM-dd) (press Enter to keep current): ");
                var dueDateString = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(dueDateString))
                    task.DueDate = DateTime.Parse(dueDateString);

                Console.Write("Is task completed? (y/n): ");
                var isCompleted = Console.ReadLine().ToLower() == "y";
                task.IsCompleted = isCompleted;

                dataService.UpdateTask(task);
                dataService.LogAction(task.UserID, "Task Updated");
                Console.WriteLine("Task updated successfully!");
            }
            else
            {
                Console.WriteLine("Task not found.");
            }
        }

        private static void DeleteTask(DataService dataService)
        {
            Console.Write("Enter Task ID to delete: ");
            var taskId = int.Parse(Console.ReadLine());
            var task = dataService.GetAllTasks().FirstOrDefault(t => t.TaskID == taskId);

            if (task != null)
            {
                dataService.DeleteTask(taskId);
                dataService.LogAction(task.UserID, "Task Deleted");
                Console.WriteLine("Task deleted successfully!");
            }
            else
            {
                Console.WriteLine("Task not found.");
            }
        }

        private static void ViewLogs(DataService dataService)
        {
            var logs = dataService.GetAllLogs();
            foreach (var log in logs)
            {
                Console.WriteLine($"Log ID: {log.LogID}, User ID: {log.UserID}, Action: {log.Action}, Timestamp: {log.Timestamp}");
            }
        }
    }
}