namespace NewToDoList
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnTaskButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//TaskPage");
        }
    }

}
