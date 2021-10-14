namespace AquilaTodos.TodoLists
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class TodoListController
    {
        private readonly IMediator mediator;

        public TodoListController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public Task<List<TodoListDto>> Get()
        {
            return this.mediator.Send(new GetAllTodoLists.Request());
        }

        [HttpGet("{id}")]
        public Task<TodoListDto> Get(int id)
        {
            return this.mediator.Send(new FindTodoList.Request(id));
        }

        [HttpPost]
        public Task Post([FromBody] TodoListDto request)
        {
            return this.mediator.Send(new CreateTodoList.Command(request));
        }
    }
}
