using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace NewToDoList
{
    public class TaskManager
    {
        private const string TasksFileName = "tasks.json";
        private static TaskManager _instance;
        private List<ToDoItem> _tasks;

        public event EventHandler TasksUpdated;

        public static TaskManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TaskManager();
                return _instance;
            }
        }

        private TaskManager()
        {
            _tasks = LoadTasksFromStorage() ?? new List<ToDoItem>();
        }

        public IReadOnlyList<ToDoItem> Tasks => _tasks.AsReadOnly();

        public void AddTask(ToDoItem task)
        {
            _tasks.Add(task);
            SaveTasksToStorage();

            OnTasksUpdated();
        }

        public void RemoveTask(ToDoItem task)
        {
            _tasks.Remove(task);
            SaveTasksToStorage();

            OnTasksUpdated();
        }

        private void SaveTasksToStorage()
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), TasksFileName);
            string serializedTasks = JsonConvert.SerializeObject(_tasks);
            File.WriteAllText(filePath, serializedTasks);
        }

        private List<ToDoItem> LoadTasksFromStorage()
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), TasksFileName);

            if (File.Exists(filePath))
            {
                string serializedTasks = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<List<ToDoItem>>(serializedTasks);
            }

            return null;
        }

        private void OnTasksUpdated()
        {
            TasksUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}
