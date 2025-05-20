using MediatR;
using MediatRBlogAPI.Application.Base;

namespace MediatRBlogAPI.Application.Features.Posts.Commands;

public class CreatePostCommand : IRequest<ResponseDto<string>>
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
