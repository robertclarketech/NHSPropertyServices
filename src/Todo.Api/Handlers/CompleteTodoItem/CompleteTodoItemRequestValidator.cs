using FluentValidation;

namespace Todo.Api.Handlers.CompleteTodoItem;

public class CompleteTodoItemRequestValidator : AbstractValidator<CompleteTodoItemRequest>
{
	public CompleteTodoItemRequestValidator()
	{
		RuleFor(e => e.Id).NotEmpty();
	}
}