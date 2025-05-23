using DataLayer;
using MediatR;
using MediatRBlogAPI.Application.Base;
using MediatRBlogAPI.Application.Features.Categories.Commands;

namespace MediatRBlogAPI.Application.Features.Categories.Handlers;

public class CreateCategoryCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateCategoryCommand, ResponseDto<string>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<ResponseDto<string>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.BlogCategoryRepository.ExistsAsync(x => x.Name == request.Name))
            return new ResponseDto<string>()
            {
                data = null,
                message = "دسته بندی با این نام وجود دارد",
                is_success = false,
                response_code = 400
            };

        var slug = request.Slug ?? SlugHelper.GenerateSlug(request.Name);
        if (await _unitOfWork.BlogCategoryRepository.ExistsAsync(x => x.Slug == slug))
            return new ResponseDto<string>()
            {
                data = slug,
                message = "دسته بندی با این نامک وجود دارد",
                is_success = false,
                response_code = 400
            };
        var postCategory = new BlogCategory()
        {
            Name = request.Name,
            Description = request.Description,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Slug = slug,
        };

        await _unitOfWork.BlogCategoryRepository.AddAsync(postCategory);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseDto<string>()
        {
            data = null,
            is_success = true,
            message = "عملیات موفقیت آمیز بود",
            response_code = 201
        };
    }
}
