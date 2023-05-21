using Microsoft.AspNetCore.Mvc;
using Todo.Api.Handlers;
using Todo.Api.Handlers.CompleteTodoItem;
using Todo.Api.Handlers.CreateTodoItem;
using Todo.Data.Models;

namespace Todo.Api.Controllers;

[ApiController]
[Route("todo")]
public class TodoController : ControllerBase
{
	private readonly ISender _sender;

	public TodoController(ISender sender)
	{
		_sender = sender;
	}

	[HttpPost]
	public async Task<ActionResult<Guid>> Create([FromBody] CreateTodoItemRequest request, CancellationToken token)
	{
		return Ok(await _sender.Send(request, token));
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<TodoItem>>> List(CancellationToken token)
	{
		return Ok(await _sender.Send(new ListTodoItemsRequest(), token));
	}
	
	[HttpPost("{id:guid}/complete")]
	public async Task<ActionResult> Complete(Guid id, CancellationToken token)
	{
		await _sender.Send(new CompleteTodoItemRequest(id), token);
		return Ok();
	}
}