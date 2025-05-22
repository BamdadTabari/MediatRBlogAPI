using DataLayer;
using MediatR;
using MediatRBlogAPI.Application.Base;
using MediatRBlogAPI.Application.Features.Posts.Query;

namespace MediatRBlogAPI.Application.Features.Posts.Handlers;

public class GetPaginatedPostQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetPaginatedPostQuery, ResponseDto<PaginatedList<Blog>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<ResponseDto<PaginatedList<Blog>>> Handle(GetPaginatedPostQuery request, CancellationToken cancellationToken)
    {
        var filter = new DefaultPaginationFilter(request.Page, request.PageSize)
        {
            Keyword = request.SearchTerm,
            SortBy = request.SortBy,
        };
        var data = _unitOfWork.BlogRepository.GetPaginated(filter);
        return new ResponseDto<PaginatedList<Blog>>()
        {
            data = data,
            message = "Ok",
            is_success = true,
            response_code = 200
        };
    }
}
