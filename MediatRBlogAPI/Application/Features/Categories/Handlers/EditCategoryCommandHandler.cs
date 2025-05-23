using DataLayer;
using MediatR;
using MediatRBlogAPI.Application.Base;
using MediatRBlogAPI.Application.Features.Categories.Commands;

namespace MediatRBlogAPI.Application.Features.Categories.Handlers;

public class EditCategoryCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<EditCategoryCommand, ResponseDto<string>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<ResponseDto<string>> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
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
        if ((await _unitOfWork.BlogCategoryRepository.ExistsAsync(x => x.Name == request.Name))
            && entity.Name != request.Name)
            return new ResponseDto<string>()
            {
                data = null,
                message = "دسته بندی با این نام وجود دارد",
                is_success = false,
                response_code = 400
            };

        var slug = request.Slug ?? SlugHelper.GenerateSlug(request.Name);
        if ((await _unitOfWork.BlogCategoryRepository.ExistsAsync(x => x.Slug == slug))
            && entity.Slug != slug)
            return new ResponseDto<string>()
            {
                data = slug,
                message = "دسته بندی با این نامک وجود دارد",
                is_success = false,
                response_code = 400
            };
        entity.Slug = slug;
        entity.Name = request.Name;
        entity.Description = request.Description;
        entity.UpdatedAt = DateTime.Now;

        _unitOfWork.BlogCategoryRepository.Update(entity);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseDto<string>()
        {
            data = null,
            is_success = true,
            message = "عملیات موفقیت آمیز بود",
            response_code = 204
        };
    }
}
