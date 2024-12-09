using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SmartTaskManager.Data;
using SmartTaskManager.Models;

namespace SmartTaskManager.Services
{
    public class DataService
    {
        private readonly TaskManagerContext _context;

        public DataService()
        {
            _context = new TaskManagerContext();
        }

        // User methods
        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(User updatedUser)
        {
            var user = _context.Users.Find(updatedUser.UserID);
            if (user != null)
            {
                user.Name = updatedUser.Name;
                user.Email = updatedUser.Email;
                user.UpdatedAt = DateTime.Now;
                _context.SaveChanges();
            }
        }

        public void DeleteUser(int userId)
        {
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        // Task methods
        public void AddTask(Task task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
        }

        public void UpdateTask(Task updatedTask)
        {
            var task = _context.Tasks.Find(updatedTask.TaskID);
            if (task != null)
            {
                task.Title = updatedTask.Title;
                task.Description = updatedTask.Description;
                task.DueDate = updatedTask.DueDate;
                task.IsCompleted = updatedTask.IsCompleted;
                task.UpdatedAt = DateTime.Now;
                _context.SaveChanges();
            }
        }

        public void DeleteTask(int taskId)
        {
            var task = _context.Tasks.Find(taskId);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
            }
        }

        public List<Task> GetTasksByUser(int userId)
        {
            return _context.Tasks.Where(t => t.UserID == userId).ToList();
        }

        public List<Task> GetAllTasks()
        {
            return _context.Tasks.ToList();
        }

        // Log methods
        public void AddLog(Log log)
        {
            _context.Logs.Add(log);
            _context.SaveChanges();
        }

        public List<Log> GetLogsByUser(int userId)
        {
            return _context.Logs.Where(l => l.UserID == userId).ToList();
        }

        public List<Log> GetAllLogs()
        {
            return _context.Logs.ToList();
        }

        // Helper method to log actions
        public void LogAction(int userId, string action, string errorMessage = null)
        {
            var log = new Log
            {
                UserID = userId,
                Action = action,
                ErrorMessage = errorMessage,
                Timestamp = DateTime.Now
            };
            AddLog(log);
        }
    }
}