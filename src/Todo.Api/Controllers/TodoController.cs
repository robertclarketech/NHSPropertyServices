using Microsoft.AspNetCore.Mvc;
using Todo.Api.Handlers;

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

	[HttpPost("create")]
	public async Task<IActionResult> Get([FromBody] CreateTodoItemRequest request)
	{
		return Ok(await _sender.Send(request));
	}

	[HttpGet("list")]
	public async Task<IActionResult> List()
	{
		return Ok(await _sender.Send(new ListTodoItemsRequest()));
	}
}