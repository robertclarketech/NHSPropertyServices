using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using Todo.Data;
using Todo.Data.Models;

namespace Todo.Tests;

public abstract class TodoContextTestsBase
{
	protected readonly Mock<ITodoContext> TodoContextMock = new();
	protected readonly List<TodoItem> TodoItems = new();
	protected readonly Mock<DbSet<TodoItem>> TodoItemSetMock;

	protected TodoContextTestsBase()
	{
		TodoItemSetMock = TodoItems.AsQueryable().BuildMockDbSet();
		TodoContextMock.Setup(e => e.TodoItems).Returns(TodoItemSetMock.Object);
	}
}