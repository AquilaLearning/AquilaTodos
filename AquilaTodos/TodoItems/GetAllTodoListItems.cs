namespace AquilaTodos.TodoItems
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public static class GetAllTodoListItems
    {
        public class Request : IRequest<List<TodoItemDto>>
        {
            public Request(int listId)
            {
                this.ListId = listId;
            }

            public int ListId { get; }
        }

        public class Handler : IRequestHandler<Request, List<TodoItemDto>>
        {
            private readonly TodoContext context;

            public Handler(TodoContext context)
            {
                this.context = context;
            }

            public async Task<List<TodoItemDto>> Handle(Request request, CancellationToken cancellationToken)
            {
                var result = await this.context.Items                             
                    .Select(
                        i => new TodoItemDto
                        {
                            Id = i.Id,
                            Label = i.Label,
                            Complete = i.Complete
                        })
                    .ToListAsync(cancellationToken);

                return result;
            }
        }
    }
}
