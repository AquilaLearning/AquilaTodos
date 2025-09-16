namespace AquilaTodos.Tests.TodoLists
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using Model;
    using AquilaTodos.TodoLists;
    using Shouldly;
    using Xunit;

    public class TodoListApplicationFactory : TestApplicationFactory
    {
        public int TodoListId { get; private set; }

        protected override void SeedData(TodoContext context)
        {
            var todoList = new TodoList
            {
                Name = "Test List"
            };

            context.Lists.AddRange(todoList);
            this.TodoListId = todoList.Id;
        }
    }

    public class TodoListControllerTests : IClassFixture<TodoListApplicationFactory>
    {
        private readonly TodoListApplicationFactory factory;

        public TodoListControllerTests(TodoListApplicationFactory factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task Should_return_all_lists()
        {
            using var client = this.factory.CreateClient();
            var result = await client.GetObjectAsync<List<TodoListDto>>("api/todolist");

            result.Count.ShouldBe(1);
            result.Single().Name.ShouldBe("Test List");
        }

        [Fact]
        public async Task Should_return_individual_list()
        {
            using var client = this.factory.CreateClient();
            var result = await client.GetObjectAsync<TodoListDto>($"api/todolist/{this.factory.TodoListId}");

            result.ShouldNotBeNull();
            result.Name.ShouldBe("Test List");
        }
    }

    public class TodoListControllerCreateTests : IClassFixture<TodoListApplicationFactory>
    {
        private readonly TodoListApplicationFactory factory;

        public TodoListControllerCreateTests(TodoListApplicationFactory factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task Should_create_new_list()
        {
            using var client = this.factory.CreateClient();
            var response = await client.PostAsJsonAsync(
                "api/todolist",
                new TodoListDto
                {
                    Name = "New List"
                });

            response.EnsureSuccessStatusCode();

            var result = await client.GetObjectAsync<List<TodoListDto>>("api/todolist");
            result.Count.ShouldBe(2);
            result.ShouldContain(r => r.Name == "New List");
        }
    }
}
