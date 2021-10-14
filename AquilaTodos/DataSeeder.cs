namespace AquilaTodos
{
    using System.Linq;
    using AquilaTodos.Model;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public static class DataSeeder
    {
        public static void Seed(IHost host)
        {
            using (var scope = host.Services.GetService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = scope.ServiceProvider.GetRequiredService<TodoContext>())
                {
                    context.Database.OpenConnection();
                    context.Database.EnsureCreated();
                    context.Database.Migrate();
                    SeedTodos(context);
                }
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
