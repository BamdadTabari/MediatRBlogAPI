using DataLayer;
using MediatR;
using MediatRBlogAPI.Application.Base;
using MediatRBlogAPI.Application.Features.Categories.Commands;

namespace MediatRBlogAPI.Application.Features.Categories.Handlers;

public class DeleteCategoryCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteCategoryCommand, ResponseDto<string>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<ResponseDto<string>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.BlogCategoryRepository.Get(request.Id);
        if (entity == null)
            return new ResponseDto<string>()
            {
                data = null,
                message = "دسته بندی پیدا نشد",
                is_success = false,
                response_code = 404
            };

        _unitOfWork.BlogCategoryRepository.Remove(entity);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseDto<string>()
        {
            data = null,
            message = "عملیات موفقیت آمیز بود",
            is_success = false,
            response_code = 200
        };
    }
}
