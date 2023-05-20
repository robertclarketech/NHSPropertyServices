using Moq;
using Todo.Api.Handlers;
using Todo.Data;
using Todo.Data.Models;

namespace Todo.Tests;

public class CreateTodoItemHandlerTests
{
	private readonly Mock<ITodoRepository> _todoRepositoryMock;
	private readonly CreateTodoItemHandler _handler;

	public CreateTodoItemHandlerTests()
	{
		_todoRepositoryMock = new Mock<ITodoRepository>();
		_handler = new CreateTodoItemHandler(_todoRepositoryMock.Object);
	}
	
	[Fact]
	public async Task Submitted_text_is_uppercase()
	{
		const string text = "this should be uppercase";
		const string expected = "THIS SHOULD BE UPPERCASE";
		
		await _handler.Handle(new CreateTodoItemRequest(text), new CancellationToken());
		
		_todoRepositoryMock.Verify(e => e.Create(It.Is<TodoItem>(x => x.Text == expected)));
	}
}