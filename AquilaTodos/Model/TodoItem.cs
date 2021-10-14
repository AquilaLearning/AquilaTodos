namespace AquilaTodos.Model
{
    public class TodoItem
    {
        public int Id { get; set; }

        public TodoList List { get; set; }

        public int ListId { get; set; }

        public string Label { get; set; }

        public bool Complete { get; set; }
    }
}
