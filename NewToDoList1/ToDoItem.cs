namespace NewToDoList
{
    public class ToDoItem
    {
        private static ToDoItem _instance;

        public static ToDoItem Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ToDoItem();
                return _instance;
            }
        }

        public string Task { get; set; }
        public string Status { get; set; }
        public string DayOfDoing { get; set; }
        public bool IsImportant { get; set; }
    }
}
