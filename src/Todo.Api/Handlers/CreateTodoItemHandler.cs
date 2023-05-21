using Todo.Data.Models;

namespace Todo.Api.Handlers;

public record CreateTodoItemRequest(string Text) : IRequest<Guid>;

public class CreateTodoItemHandler : IRequestHandler<CreateTodoItemRequest, Guid>
{
	private readonly ITodoContext _todoContext;

	public CreateTodoItemHandler(ITodoContext todoContext)
	{
		_todoContext = todoContext;
	}

	public Task<Guid> Handle(CreateTodoItemRequest request, CancellationToken cancellationToken)
	{
		var item = new TodoItem
		{
			Created = DateTime.Now,
			Id = Guid.NewGuid(),
			Text = request.Text.ToUpperInvariant()
		};

		_todoContext.TodoItems.Add(item);
		return Task.FromResult(item.Id);
	}
}