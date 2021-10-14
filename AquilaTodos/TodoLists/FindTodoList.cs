namespace AquilaTodos.TodoLists
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public static class FindTodoList
    {
        public class Request : IRequest<TodoListDto>
        {
            public Request(int listId)
            {
                this.ListId = listId;
            }

            public int ListId { get; }
        }

        public class Handler : IRequestHandler<Request, TodoListDto>
        {
            private readonly TodoContext context;

            public Handler(TodoContext context)
            {
                this.context = context;
            }

            public Task<TodoListDto> Handle(Request request, CancellationToken cancellationToken)
            {
                return this.context.Lists
                    .Select(
                        l => new TodoListDto
                        {
                            Id = l.Id,
                            Name = l.Name
                        })
                    .SingleOrDefaultAsync(l => l.Id == request.ListId, cancellationToken);
            }
        }
    }
}
