using FluentAssertions;
using FluentValidation;
using Moq;
using Todo.Api.Handlers.CompleteTodoItem;
using Todo.Api.Handlers.CreateTodoItem;
using Todo.Api.Services;
using Todo.Data.Models;

namespace Todo.Tests;

public class CompleteTodoItemHandlerTests : TodoContextTestsBase
{
	private readonly Mock<IDateTimeService> _dateTimeServiceMock = new();
	private readonly CompleteTodoItemHandler _handler;

	public CompleteTodoItemHandlerTests()
	{
		_handler = new CompleteTodoItemHandler(TodoContextMock.Object, _dateTimeServiceMock.Object);
	}

	[Fact]
	public async Task Throws_validation_error_if_guid_is_default()
	{
		await _handler.Invoking(e => e.Handle(new CompleteTodoItemRequest(default), CancellationToken.None))
			.Should().ThrowAsync<ValidationException>();
	}
	
	[Fact]
	public async Task Throws_validation_error_if_item_with_guid_does_not_exist()
	{
		await _handler.Invoking(e => e.Handle(new CompleteTodoItemRequest(Guid.NewGuid()), CancellationToken.None))
			.Should().ThrowAsync<ValidationException>();
	}

	[Fact]
	public async Task Complete_todo_should_set_completed_time_to_now()
	{
		var guid = Guid.NewGuid();
		var todo = new TodoItem
		{
			Id = guid
		};
		TodoItems.Add(todo);
		var dateTime = DateTime.UtcNow;
		_dateTimeServiceMock.Setup(e => e.GetDateTimeNow()).Returns(dateTime);
		
		await _handler.Handle(new CompleteTodoItemRequest(guid), CancellationToken.None);

		todo.Completed.Should().Be(dateTime);
	}
}