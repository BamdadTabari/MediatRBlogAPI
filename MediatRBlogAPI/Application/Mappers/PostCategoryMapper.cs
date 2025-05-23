using DataLayer;
using MediatRBlogAPI.Application.Features.Categories.Response;

namespace MediatRBlogCategoryAPI.Application.Mappers;

public static class PostCategoryMapper
{
    public static PostCategoryResponse Map(this BlogCategory src)
    {
        return new PostCategoryResponse()
        {
            Name = src.Name,
            Description = src.Description,
            UpdatedAt = src.UpdatedAt,
            CreatedAt = src.CreatedAt,
            Slug = src.Slug,
            Id = src.Id
        };
    }

    public static List<PostCategoryResponse> BulkMap(this List<BlogCategory> src)
    {
        return src.Select(x => new PostCategoryResponse()
        {
            Name = x.Name,
            Description = x.Description,
            UpdatedAt = x.UpdatedAt,
            CreatedAt = x.CreatedAt,
            Slug = x.Slug,
            Id = x.Id
        }).ToList();
    }
}
