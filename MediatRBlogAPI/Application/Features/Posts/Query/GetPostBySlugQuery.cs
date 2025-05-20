using MediatR;
using MediatRBlogAPI.Application.Base;
using MediatRBlogAPI.Application.Features.Posts.Response;
using System.ComponentModel.DataAnnotations;

namespace MediatRBlogAPI.Application.Features.Posts.Query;

public class GetPostBySlugQuery : IRequest<ResponseDto<PostResponse>>
{
	[Display(Name ="نامک")]
	[Required(ErrorMessage = "لطفا مقدار {0} را وارد کنید")]
	public string Slug { get; set; }
}
