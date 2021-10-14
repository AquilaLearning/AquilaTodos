namespace AquilaTodos.TodoLists
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public static class GetAllTodoLists
    {
        public class Request : IRequest<List<TodoListDto>>
        {
        }

        public class Handler : IRequestHandler<Request, List<TodoListDto>>
        {
            private readonly TodoContext context;

            public Handler(TodoContext context)
            {
                this.context = context;
            }

            public Task<List<TodoListDto>> Handle(Request request, CancellationToken cancellationToken)
            {
                return this.context.Lists
                    .Select(
                        l => new TodoListDto
                        {
                            Id = l.Id,
                            Name = l.Name
                        })
                    .ToListAsync(cancellationToken);
            }
        }
    }
}
