using MediatR;
using MediatRBlogAPI.Application.Base;
using MediatRBlogAPI.Application.Features.Posts.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediatRBlogAPI.Controllers;
[Route("api/blog")]
[ApiController]
public class BlogController(IMediator _mediator) : ControllerBase
{
	private readonly IMediator _mediator = _mediator;

	[HttpPost]
	[Route("create")]
	public async Task<IActionResult> CreatePost([FromForm] CreatePostCommand command, CancellationToken cancellationToken)
	{
		if (!ModelState.IsValid)
		{
			var error = string.Join(" | ", ModelState.Values
				.SelectMany(v => v.Errors)
				.Select(e => e.ErrorMessage));
			return BadRequest(new ResponseDto<string>()
			{
				data = null,
				is_success = false,
				message = error,
				response_code = 400
			});
		}

		var response = await _mediator.Send(command, cancellationToken);

		return Ok(response);
	}

	[HttpGet]
	[Route("get")]
	public async Task<IActionResult> Get([FromForm] string slug)
	{
		// برای بعد
		return Ok();
	}
}
