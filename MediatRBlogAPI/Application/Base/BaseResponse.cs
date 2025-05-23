namespace MediatRBlogAPI.Application.Base;

public class BaseResponse
{
    public long Id { get; set; }
    public string Slug { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set;
}
