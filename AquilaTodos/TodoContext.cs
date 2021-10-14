namespace AquilaTodos
{
    using AquilaTodos.Model;
    using Microsoft.EntityFrameworkCore;

    public class TodoContext : DbContext
    {
        public string DbPath { get; private set; }

        public DbSet<TodoList> Lists { get; set; }

        public DbSet<TodoItem> Items { get; set; }

        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }
    }
}
