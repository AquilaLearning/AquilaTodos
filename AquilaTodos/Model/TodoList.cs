namespace AquilaTodos.Model
{
    using System.Collections.Generic;

    public class TodoList
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<TodoItem> Items { get; } = new List<TodoItem>();
    }
}
