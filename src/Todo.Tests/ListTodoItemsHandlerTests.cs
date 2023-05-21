using FluentAssertions;
using Todo.Api.Handlers;
using Todo.Data.Models;

namespace Todo.Tests;

public class ListTodoItemsHandlerTests : TodoContextTestsBase
{
	private readonly ListTodoItemsHandler _handler;

	public ListTodoItemsHandlerTests()
	{
		_handler = new ListTodoItemsHandler(TodoContextMock.Object);
	}

	[Fact]
	public async Task Most_recently_created_todo_is_always_first()
	{
		var old = new TodoItem {Created = DateTime.UtcNow.AddDays(-1)};
		var latest = new TodoItem {Created = DateTime.UtcNow};
		TodoItems.AddRange(new[] {old, latest});

		var result = await _handler.Handle(new ListTodoItemsRequest(), CancellationToken.None);

		result.Should().StartWith(latest).And.EndWith(old);
	}

	[Fact]
	public async Task Show_completed_true_should_return_completed_todos()
	{
		var completed = new TodoItem {Completed = DateTime.UtcNow.AddDays(-1)};
		var uncompleted = new TodoItem();
		TodoItems.AddRange(new[] {completed, uncompleted});

		var result = await _handler.Handle(new ListTodoItemsRequest(true), CancellationToken.None);

		result.Should().Contain(completed).And.Contain(uncompleted);
	}
	
	[Fact]
	public async Task Show_completed_false_should_not_return_completed_todos()
	{
		var completed = new TodoItem {Completed = DateTime.UtcNow.AddDays(-1)};
		var uncompleted = new TodoItem();
		TodoItems.AddRange(new[] {completed, uncompleted});

		var result = await _handler.Handle(new ListTodoItemsRequest(), CancellationToken.None);

		result.Should().NotContain(completed).And.Contain(uncompleted);
	}
}