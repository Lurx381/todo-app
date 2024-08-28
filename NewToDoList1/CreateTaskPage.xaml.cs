using Microsoft.Maui.Controls;
using System;

namespace NewToDoList
{
    public partial class CreateTaskPage : ContentPage
    {
        private DateTime scheduledDateTime;

        public CreateTaskPage()
        {
            InitializeComponent();
        }

        private void OnCreateTaskClicked(object sender, EventArgs e)
        {
            string title = taskTitleEntry.Text;
            string description = taskDescriptionEditor.Text;

            titleErrorLabel.Text = string.Empty;
            descriptionErrorLabel.Text = string.Empty;
            importanceErrorLabel.Text = string.Empty;
            scheduledDateTimeErrorLabel.Text = string.Empty;
            reminderDateTimeErrorLabel.Text = string.Empty;

            if (string.IsNullOrWhiteSpace(title))
            {
                titleErrorLabel.Text = "? Title cannot be empty.";
                return;
            }
            if (title.Length > 50)
            {
                titleErrorLabel.Text = "? Maximum characters exceeded (max 50).";
                return;
            }
            if (string.IsNullOrWhiteSpace(description))
            {
                descriptionErrorLabel.Text = "? Description cannot be empty.";
                return;
            }
            if (description.Length > 300)
            {
                descriptionErrorLabel.Text = "? Maximum characters exceeded (max 300).";
                return;
            }

            scheduledDateTime = GetDateTimeFromPickers(scheduledDatePicker.Date, scheduledTimePicker.Time);
            DateTime reminderDateTime = GetDateTimeFromPickers(reminderDatePicker.Date, reminderTimePicker.Time);

            if (scheduledDateTime <= DateTime.Now)
            {
                scheduledDateTimeErrorLabel.Text = "? Please choose a date and time later than now.";
                return;
            }

            if (reminderDateTime <= DateTime.Now)
            {
                reminderDateTimeErrorLabel.Text = "? Please choose a date and time later than now.";
                return;
            }

            if (!radioYes.IsChecked && !radioNo.IsChecked)
            {
                importanceErrorLabel.Text = "? Please choose the importance of the task.";
                return;
            }

            var newTask = new ToDoItem
            {
                Task = title,
                Status = "Pending",
                DayOfDoing = scheduledDateTime.DayOfWeek.ToString(),
                IsImportant = radioYes.IsChecked
            };

            if (IsValidTask(newTask))
            {
                TaskManager.Instance.AddTask(newTask);

                ShowTaskCreatedAlert(newTask);

                taskTitleEntry.Text = string.Empty;
                taskDescriptionEditor.Text = string.Empty;
                scheduledDatePicker.Date = DateTime.Today;
                scheduledTimePicker.Time = TimeSpan.Zero;
                reminderDatePicker.Date = DateTime.Today;
                reminderTimePicker.Time = TimeSpan.Zero;
                radioYes.IsChecked = false;
                radioNo.IsChecked = false;
            }
            else
            {
                DisplayAlert(" ? Error ? ", "Task validation failed. Please check your input.", "OK");
            }
        }

        private void OnRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            importanceErrorLabel.Text = string.Empty;
        }

        private bool IsValidTask(ToDoItem task)
        {
            scheduledDateTimeErrorLabel.Text = string.Empty;
            reminderDateTimeErrorLabel.Text = string.Empty;

            return scheduledDateTime > DateTime.Now;
        }

        private DateTime GetDateTimeFromPickers(DateTime date, TimeSpan time)
        {
            return date.Date + time;
        }

        private void ShowTaskCreatedAlert(ToDoItem newTask)
        {
            string alertMessage = $"Task created:\nTitle: {newTask.Task}\nStatus: {newTask.Status}\nDay of Doing: {newTask.DayOfDoing}\nIs Important: {newTask.IsImportant}";

            Device.InvokeOnMainThreadAsync(async () =>
            {
                await DisplayAlert("? Task Created", alertMessage, "OK");
            });
        }

        private void OnImportancePickerIndexChanged(object sender, EventArgs e)
        {
            importanceErrorLabel.Text = string.Empty;
        }
    }
}
