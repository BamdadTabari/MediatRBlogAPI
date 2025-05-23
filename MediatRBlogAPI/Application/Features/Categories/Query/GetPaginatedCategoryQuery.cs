using DataLayer;
using MediatR;
using MediatRBlogAPI.Application.Base;

namespace MediatRBlogAPI.Application.Features.Categories.Query;

public class GetPaginatedCategoryQuery : IRequest<ResponseDto<PaginatedList<BlogCategory>>>
{
    public string? SearchTerm { get; set; }
    public SortByEnum SortBy { get; set; } = SortByEnum.CreationDate;
    public int PageSize { get; set; } = 10;
    public int Page { get; set; } = 1;
}
