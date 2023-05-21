using FluentValidation;

namespace Todo.Api.Handlers.CreateTodoItem;

public class CreateTodoItemRequestValidator : AbstractValidator<CreateTodoItemRequest>
{
	public CreateTodoItemRequestValidator()
	{
		RuleFor(e => e.Text).NotEmpty();
	}
}