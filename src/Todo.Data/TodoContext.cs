using Microsoft.EntityFrameworkCore;
using Todo.Data.Models;

namespace Todo.Data;

public class TodoContext : DbContext, ITodoContext
{
	public TodoContext(DbContextOptions<TodoContext> options)
		: base(options)
	{
	}

	public DbSet<TodoItem> TodoItems => Set<TodoItem>();
}

public interface ITodoContext
{
	DbSet<TodoItem> TodoItems { get; }
}