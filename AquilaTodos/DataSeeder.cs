namespace AquilaTodos
{
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Model;

    public static class DataSeeder
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TodoContext>();
            if (context.Database.IsRelational())
            {
                context.Database.Migrate();
            }

            if (context.Lists.Any() == false)
            {
                SeedTodos(context);
            }
        }


        private static void SeedTodos(TodoContext context)
        {
            var exists = context.Lists.Any();

            if (exists)
            {
                return;
            }

            var todoList = new TodoList
            {
                Name = "My first list"
            };

            todoList.Items.Add(new TodoItem
            {
                Label = "Check emails"
            });

            todoList.Items.Add(new TodoItem
            {
                Label = "Code reviews"
            });

            todoList.Items.Add(new TodoItem
            {
                Label = "Fix bugs"
            });

            context.Lists.Add(todoList);
            context.SaveChanges();
        }
    }
}
