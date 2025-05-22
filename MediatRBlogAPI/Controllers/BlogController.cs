using DataLayer;
using MediatR;
using MediatRBlogAPI.Application.Base;
using MediatRBlogAPI.Application.Features.Posts.Commands;
using MediatRBlogAPI.Application.Features.Posts.Query;
using Microsoft.AspNetCore.Mvc;

namespace MediatRBlogAPI.Controllers;
[Route("api/blog")]
[ApiController]
public class BlogController(IMediator _mediator) : ControllerBase
{
	private readonly IMediator _mediator = _mediator;

    [HttpGet]
    [Route("list")]
    public async Task<IActionResult> Index(GetPaginatedPostQuery query, CancellationToken cancellationToken)
    {
		var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }


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

    [HttpPost]
    [Route("edit")]
    public async Task<IActionResult> EditPost([FromForm] EditPostCommand command, CancellationToken cancellationToken)
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
	public async Task<IActionResult> GetBySlug([FromForm] GetPostBySlugQuery query, CancellationToken cancellationToken)
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

		var response = await _mediator.Send(query, cancellationToken);
		
		return Ok(response);
	}
}
