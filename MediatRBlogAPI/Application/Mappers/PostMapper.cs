using DataLayer;
using MediatRBlogAPI.Application.Features.Posts.Response;

namespace MediatRBlogAPI.Application.Mappers;

public static class PostMapper
{
	public static PostResponse Map(this Blog src)
	{
		return new PostResponse()
		{
			BlogText = src.BlogText,
			CategoryName = src.BlogCategory.Name,
			Name = src.Name,
			Image = src.Image,
			ImageAlt = src.ImageAlt,
			KeyWords = src.KeyWords,
			ShortDescription = src.ShortDescription,
			ShowBlog = src.ShowBlog,
			EstimatedReadTime = src.EstimatedReadTime,
			MetaDescription = src.MetaDescription,
			UpdatedAt = src.UpdatedAt,
			CreatedAt = src.CreatedAt,
			Slug = src.Slug,
			Id = src.Id
		};
	}

	public static List<PostResponse> BulkMap(this List<Blog> src)
	{		
		return src.Select(x => new PostResponse()
		{
			BlogText = x.BlogText,
			CategoryName = x.BlogCategory.Name,
			Name = x.Name,
			Image = x.Image,
			ImageAlt = x.ImageAlt,
			KeyWords = x.KeyWords,
			ShortDescription = x.ShortDescription,
			ShowBlog = x.ShowBlog,
			EstimatedReadTime = x.EstimatedReadTime,
			MetaDescription = x.MetaDescription,
            UpdatedAt = x.UpdatedAt,
            CreatedAt = x.CreatedAt,
            Slug = x.Slug,
            Id = x.Id
        }).ToList();
	}
}
