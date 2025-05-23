using DataLayer;
using MediatR;
using MediatRBlogAPI.Application.Base;
using MediatRBlogAPI.Application.Features.Categories.Query;
using MediatRBlogAPI.Application.Features.Categories.Response;
using MediatRBlogCategoryAPI.Application.Mappers;

namespace MediatRBlogAPI.Application.Features.Categories.Handlers;

public class GetAllCategoryQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllCategoryQuery, ResponseDto<List<PostCategoryResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<ResponseDto<List<PostCategoryResponse>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        var data = await _unitOfWork.BlogCategoryRepository.GetAll();
        var dtoList = data.BulkMap();

        return new ResponseDto<List<PostCategoryResponse>>()
        {
            data = dtoList,
            message = "Ok",
            is_success = true,
            response_code = 200
        };
    }
}
