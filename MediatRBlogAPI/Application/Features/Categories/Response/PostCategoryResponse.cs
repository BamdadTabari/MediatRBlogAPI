using MediatRBlogAPI.Application.Base;

namespace MediatRBlogAPI.Application.Features.Categories.Response;

public class PostCategoryResponse : BaseResponse
{
    public string Name { get; set; }
    public string? Description { get; set; }
}
