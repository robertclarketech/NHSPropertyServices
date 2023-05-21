using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Todo.Api.Handlers.CreateTodoItem;
using Todo.Api.Services;

namespace Todo.Api.Handlers.CompleteTodoItem;

public record CompleteTodoItemRequest(Guid Id) : IRequest;

public class CompleteTodoItemHandler : IRequestHandler<CompleteTodoItemRequest>
{
	private readonly IDateTimeService _dateTimeService;
	private readonly ITodoContext _todoContext;

	public CompleteTodoItemHandler(ITodoContext todoContext, IDateTimeService dateTimeService)
	{
		_todoContext = todoContext;
		_dateTimeService = dateTimeService;
	}

	public async Task Handle(CompleteTodoItemRequest request, CancellationToken cancellationToken)
	{
		var validationResult = await new CompleteTodoItemRequestValidator().ValidateAsync(request, cancellationToken);
		if (!validationResult.IsValid)
		{
			throw new ValidationException(validationResult.Errors);
		}
		var todo = await _todoContext.TodoItems.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
		if (todo == null)
		{
			throw new ValidationException(new[]
				{new ValidationFailure(nameof(request.Id), "No todo with that Guid exists")});
		}

		todo.Completed = _dateTimeService.GetDateTimeNow();
		await _todoContext.SaveChangesAsync(cancellationToken);
	}
}