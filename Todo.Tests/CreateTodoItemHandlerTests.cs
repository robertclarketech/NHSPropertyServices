using Moq;
using Todo.Api.Handlers;
using Todo.Data.Models;

namespace Todo.Tests;

public class CreateTodoItemHandlerTests : TodoContextTestsBase
{
	private readonly CreateTodoItemHandler _handler;

	public CreateTodoItemHandlerTests()
	{
		_handler = new CreateTodoItemHandler(TodoContextMock.Object);
	}

	[Fact]
	public async Task Submitted_text_is_uppercase()
	{
		const string text = "this should be uppercase";
		const string expected = "THIS SHOULD BE UPPERCASE";

		await _handler.Handle(new CreateTodoItemRequest(text), CancellationToken.None);

		TodoItemSetMock.Verify(e => e.Add(It.Is<TodoItem>(x => x.Text == expected)));
	}
}