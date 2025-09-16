namespace AquilaTodos.TodoItems
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Model;

    public static class CreateTodoItem
    {
        public class Command : IRequest
        {
            public Command(int listId, TodoItemDto model)
            {
                this.ListId = listId;
                this.Model = model;
            }

            public int ListId { get; }

            public TodoItemDto Model { get; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly TodoContext context;

            public Handler(TodoContext context)
            {
                this.context = context;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var list = await this.context.Lists
                    .Include(l => l.Items)
                    .SingleOrDefaultAsync(l => l.Id == request.ListId, cancellationToken);

                list.Items.Add(new TodoItem
                {
                    Label = request.Model.Label,
                    Complete = false
                });

                await this.context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
