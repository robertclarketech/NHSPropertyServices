using Microsoft.EntityFrameworkCore;
using Todo.Data.Models;

namespace Todo.Api.Handlers;

public record ListTodoItemsRequest(bool ShowCompleted = false) : IRequest<IEnumerable<TodoItem>>;

public class ListTodoItemsHandler : IRequestHandler<ListTodoItemsRequest, IEnumerable<TodoItem>>
{
	private readonly ITodoContext _todoContext;

	public ListTodoItemsHandler(ITodoContext todoContext)
	{
		_todoContext = todoContext;
	}

	public async Task<IEnumerable<TodoItem>> Handle(ListTodoItemsRequest request, CancellationToken cancellationToken)
	{
		return await _todoContext
			.TodoItems
			.OrderByDescending(e => e.Created)
			.Where(e => request.ShowCompleted || e.Completed == null)
			.ToListAsync(cancellationToken);
	}
}