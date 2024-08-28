

namespace NewToDoList
{
    public partial class TaskPage : ContentPage
    {
        public TaskPage()
        {
            InitializeComponent();
            RefreshTaskList();

            TaskManager.Instance.TasksUpdated += OnTasksUpdated;
        }


        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var todoItem = (ToDoItem)button.CommandParameter;

            var action = await DisplayActionSheet($"Delete '{todoItem.Task}'?", "Cancel", "Delete");

            if (action == "Delete")
            {
                TaskManager.Instance.RemoveTask(todoItem);
                RefreshTaskList();
            }
        }

        private async void OnAddTaskButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//CreateTaskPage");
        }

        private void RefreshTaskList()
        {
            taskListView.ItemsSource = TaskManager.Instance.Tasks;
        }
        private void OnTasksUpdated(object sender, EventArgs e)
        {
            RefreshTaskList();
        }
    }
}
