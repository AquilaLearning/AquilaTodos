namespace AquilaTodos.TodoItems
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/TodoList/{listId}/Item")]
    public class TodoItemController
    {
        private readonly IMediator mediator;

        public TodoItemController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public Task<List<TodoItemDto>> Get(int listId)
        {
            return this.mediator.Send(new GetAllTodoListItems.Request(listId));
        }

        [HttpPost]
        public Task Post(int listId, [FromBody] TodoItemDto request)
        {
            return this.mediator.Send(new CreateTodoItem.Command(listId, request));
        }
    }
}
