using DataLayer;
using MediatR;
using MediatRBlogAPI.Application.Base;
using MediatRBlogAPI.Application.Features.Categories.Query;
using MediatRBlogAPI.Application.Features.Categories.Response;
using MediatRBlogCategoryAPI.Application.Mappers;

namespace MediatRBlogAPI.Application.Features.Categories.Handlers;

public class GetCategoryBySlugQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetCategoryBySlugQuery, ResponseDto<PostCategoryResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<ResponseDto<PostCategoryResponse>> Handle(GetCategoryBySlugQuery request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.BlogCategoryRepository.Get(request.Slug);
        if (entity == null)
            return new ResponseDto<PostCategoryResponse>()
            {
                data = null,
                message = "دسته بندی مقاله پیدا نشد",
                is_success = false,
                response_code = 404
            };
        var dto = entity.Map();
        return new ResponseDto<PostCategoryResponse>()
        {
            data = dto,
            message = "Ok",
            is_success = true,
            response_code = 200
        };
    }
}
