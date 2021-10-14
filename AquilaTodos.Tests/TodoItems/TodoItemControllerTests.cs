namespace AquilaTodos.Tests.TodoItems
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using AquilaTodos.Model;
    using AquilaTodos.TodoItems;
    using Shouldly;
    using Xunit;

    public class TodoItemControllerApplicationFactory : TestApplicationFactory
    {
        public int TodoListId { get; private set; }

        protected override void SeedData(TodoContext context)
        {
            var list = new TodoList
            {
                Name = "Test List"
            };

            var item1 = new TodoItem
            {
                Label = "Item 1",
                Complete = false,
                List = list
            };

            var item2 = new TodoItem
            {
                Label = "Item 2",
                Complete = true,
                List = list
            };

            var item3 = new TodoItem
            {
                Label = "Item 3",
                Complete = false,
                List = list
            };

            context.Items.AddRange(item1, item2, item3);
            this.TodoListId = list.Id;
        }
    }

    public class TodoItemControllerTests : IClassFixture<TodoItemControllerApplicationFactory>
    {
        private readonly TodoItemControllerApplicationFactory factory;

        public TodoItemControllerTests(TodoItemControllerApplicationFactory factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task Should_return_all_items_for_list()
        {
            using var client = this.factory.CreateClient();
            var result = await client.GetObjectAsync<List<TodoItemDto>>($"api/todolist/{this.factory.TodoListId}/item");

            result.Count.ShouldBe(3);

            result.ShouldContain(i => i.Label == "Item 1" && i.Complete == false);
            result.ShouldContain(i => i.Label == "Item 2" && i.Complete == true);
            result.ShouldContain(i => i.Label == "Item 3" && i.Complete == false);
        }

        [Fact]
        public async Task Should_add_item_to_list()
        {
            using var client = this.factory.CreateClient();

            var response = await client.PostAsJsonAsync(
                $"api/todolist/{this.factory.TodoListId}/item",
                new TodoItemDto
                {
                    Label = "New Item"
                });

            var result = await client.GetObjectAsync<List<TodoItemDto>>($"api/todolist/{this.factory.TodoListId}/item");

            result.Count.ShouldBe(4);

            result.ShouldContain(i => i.Label == "Item 1" && i.Complete == false);
            result.ShouldContain(i => i.Label == "Item 2" && i.Complete == true);
            result.ShouldContain(i => i.Label == "Item 3" && i.Complete == false);
            result.ShouldContain(i => i.Label == "New Item" && i.Complete == false);
        }
    }
}
