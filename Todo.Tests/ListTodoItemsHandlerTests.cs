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
}