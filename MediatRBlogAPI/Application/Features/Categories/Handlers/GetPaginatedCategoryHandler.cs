using DataLayer;
using MediatR;
using MediatRBlogAPI.Application.Base;
using MediatRBlogAPI.Application.Features.Categories.Query;

namespace MediatRBlogAPI.Application.Features.Categories.Handlers;

public class GetPaginatedCategoryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetPaginatedCategoryQuery, ResponseDto<PaginatedList<BlogCategory>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<ResponseDto<PaginatedList<BlogCategory>>> Handle(GetPaginatedCategoryQuery request, CancellationToken cancellationToken)
    {
        var filter = new DefaultPaginationFilter(request.Page, request.PageSize)
        {
            Keyword = request.SearchTerm,
            SortBy = request.SortBy,
        };
        var data = _unitOfWork.BlogCategoryRepository.GetPaginated(filter);
        return new ResponseDto<PaginatedList<BlogCategory>>()
        {
            data = data,
            message = "Ok",
            is_success = true,
            response_code = 200
        };
    }
}
