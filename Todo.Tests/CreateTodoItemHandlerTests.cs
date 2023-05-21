using FluentAssertions;
using FluentValidation;
using Moq;
using Todo.Api.Handlers.CreateTodoItem;
using Todo.Api.Services;
using Todo.Data.Models;

namespace Todo.Tests;

public class CreateTodoItemHandlerTests : TodoContextTestsBase
{
	private readonly Mock<IDateTimeService> _dateTimeServiceMock = new();
	private readonly CreateTodoItemHandler _handler;

	public CreateTodoItemHandlerTests()
	{
		_handler = new CreateTodoItemHandler(TodoContextMock.Object, _dateTimeServiceMock.Object);
	}

	[Fact]
	public async Task New_todos_are_set_from_datetime_service()
	{
		var dateTime = DateTime.UtcNow;
		_dateTimeServiceMock.Setup(e => e.GetDateTimeNow()).Returns(dateTime);

		await _handler.Handle(new CreateTodoItemRequest("todo"), CancellationToken.None);

		_dateTimeServiceMock.Verify(e => e.GetDateTimeNow(), Times.Once);
		TodoItemSetMock.Verify(e => e.Add(It.Is<TodoItem>(x => x.Created == dateTime)));
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
	public async Task Throw_validation_exception_if_empty()
	{
		await _handler.Invoking(e => e.Handle(new CreateTodoItemRequest(string.Empty), CancellationToken.None))
			.Should().ThrowAsync<ValidationException>();
	}
}