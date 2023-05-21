using Moq;
using Todo.Api.Handlers;
using Todo.Api.Services;
using Todo.Data.Models;

namespace Todo.Tests;

public class CreateTodoItemHandlerTests : TodoContextTestsBase
{
	private readonly CreateTodoItemHandler _handler;
	private readonly Mock<IDateTimeService> _dateTimeServiceMock = new Mock<IDateTimeService>();

	public CreateTodoItemHandlerTests()
	{
		_handler = new CreateTodoItemHandler(TodoContextMock.Object, _dateTimeServiceMock.Object);
	}

	[Fact]
	public async Task Submitted_text_is_uppercase()
	{
		const string text = "this should be uppercase";
		const string expected = "THIS SHOULD BE UPPERCASE";

		await _handler.Handle(new CreateTodoItemRequest(text), CancellationToken.None);

		TodoItemSetMock.Verify(e => e.Add(It.Is<TodoItem>(x => x.Text == expected)));
	}
	
	[Fact]
	public async Task New_todos_are_set_from_datetime_service()
	{
		var dateTime = DateTime.UtcNow;
		_dateTimeServiceMock.Setup(e => e.GetDateTimeNow()).Returns(dateTime);
		
		await _handler.Handle(new CreateTodoItemRequest(string.Empty), CancellationToken.None);

		_dateTimeServiceMock.Verify(e => e.GetDateTimeNow(), Times.Once);
		TodoItemSetMock.Verify(e => e.Add(It.Is<TodoItem>(x => x.Created == dateTime)));
	}
}