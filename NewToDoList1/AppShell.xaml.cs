namespace NewToDoList
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private async void OnHomeButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//MainPage");
        }

        private async void OnTaskButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//TaskPage");
        }

        private async void OnCreateTaskButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//CreateTaskPage");
        }
    }
}
