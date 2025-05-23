using MediatRBlogAPI.Application.Base;

namespace MediatRBlogAPI.Application.Features.Posts.Response;

public class PostResponse : BaseResponse
{
	public string Name { get; set; }
	public string ShortDescription { get; set; }
	public string Image { get; set; }
	public string ImageAlt { get; set; }
	public string BlogText { get; set; }
	public bool ShowBlog { get; set; }
	public string KeyWords { get; set; }
	public string CategoryName { get; set; }
	public string MetaDescription { get; set; }
	public int EstimatedReadTime { get; set; }

}
