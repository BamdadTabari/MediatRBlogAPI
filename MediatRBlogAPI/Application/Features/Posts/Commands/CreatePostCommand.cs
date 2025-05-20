using MediatR;

namespace MediatRBlogAPI.Application.Features.Posts.Commands;

public class CreatePostCommand : IRequest<long>
{
    public string Name { get; set; }
    public string ShortDescription { get; set; }
    public IFormFile Image { get; set; }
    public string ImageAlt { get; set; }
    public string BlogText { get; set; }
    public bool ShowBlog { get; set; }
    public string KeyWords { get; set; }


    public int BlogCategoryId { get; set; }
}
