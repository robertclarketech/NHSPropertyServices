using Microsoft.EntityFrameworkCore;
using Todo.Data.Models;

namespace Todo.Data;

public interface ITodoContext
{
	DbSet<TodoItem> TodoItems { get; }
	Task SaveChangesAsync(CancellationToken token = default);
}