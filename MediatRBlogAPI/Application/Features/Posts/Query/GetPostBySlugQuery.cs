using MediatR;
using MediatRBlogAPI.Application.Base;
using MediatRBlogAPI.Application.Features.Posts.Response;

namespace MediatRBlogAPI.Application.Features.Posts.Query;

public class GetPostBySlugQuery : IRequest<ResponseDto<PostResponse>>
{
	public string Slug { get; set; }
}
