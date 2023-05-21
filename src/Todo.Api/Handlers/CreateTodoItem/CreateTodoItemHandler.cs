using FluentValidation;
using Todo.Api.Services;
using Todo.Data.Models;

namespace Todo.Api.Handlers.CreateTodoItem;

public record CreateTodoItemRequest(string Text) : IRequest<Guid>;

public class CreateTodoItemHandler : IRequestHandler<CreateTodoItemRequest, Guid>
{
	private readonly IDateTimeService _dateTimeService;
	private readonly ITodoContext _todoContext;

	public CreateTodoItemHandler(ITodoContext todoContext, IDateTimeService dateTimeService)
	{
		_todoContext = todoContext;
		_dateTimeService = dateTimeService;
	}

	public async Task<Guid> Handle(CreateTodoItemRequest request, CancellationToken cancellationToken)
	{
		var validationResult = await new CreateTodoItemRequestValidator().ValidateAsync(request, cancellationToken);
		if (!validationResult.IsValid)
		{
			throw new ValidationException(validationResult.Errors);
		}

		var item = new TodoItem
		{
			Created = _dateTimeService.GetDateTimeNow(),
			Id = Guid.NewGuid(),
			Text = request.Text.ToUpperInvariant()
		};

		_todoContext.TodoItems.Add(item);
		await _todoContext.SaveChangesAsync(cancellationToken);
		return item.Id;
	}
}