namespace AquilaTodos.TodoLists
{
    using System.Threading;
    using System.Threading.Tasks;
    using AquilaTodos.Model;
    using MediatR;

    public static class CreateTodoList
    {
        public class Command : IRequest
        {
            public Command(TodoListDto model)
            {
                this.Model = model;
            }
            public TodoListDto Model { get; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly TodoContext context;

            public Handler(TodoContext context)
            {
                this.context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var todoList = new TodoList
                {
                    Name = request.Model.Name
                };

                await this.context.Lists.AddAsync(todoList, cancellationToken);
                await this.context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
