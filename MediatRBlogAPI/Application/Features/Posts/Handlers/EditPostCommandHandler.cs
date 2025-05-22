using DataLayer;
using MediatR;
using MediatRBlogAPI.Application.Base;
using MediatRBlogAPI.Application.Features.Posts.Commands;

namespace MediatRBlogAPI.Application.Features.Posts.Handlers;

public class EditPostCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<EditPostCommand, ResponseDto<string>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ResponseDto<string>> Handle(EditPostCommand request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.BlogRepository.Get(request.Id);
        if (entity == null)
            return new ResponseDto<string> 
            {
                data = null,
                message = "مقاله پیدا نشد",
                is_success = false,
                response_code = 404
            };

        if ((await _unitOfWork.BlogRepository.ExistsAsync(x => x.Name == request.Name))
            && entity.Name != request.Name)
            return new ResponseDto<string>()
            {
                data = null,
                message = "مقاله با این نام وجود دارد",
                is_success = false,
                response_code = 400
            };

        var slug = request.Slug ?? SlugHelper.GenerateSlug(request.Name);
        if ((await _unitOfWork.BlogRepository.ExistsAsync(x => x.Slug == slug)) 
            && entity.Slug != request.Slug)
            return new ResponseDto<string>()
            {
                data = slug,
                message = "مقاله با این نامک وجود دارد",
                is_success = false,
                response_code = 400
            };
        entity.Name = request.Name;
        entity.KeyWords = request.KeyWords;
        entity.ShortDescription = request.ShortDescription;
        entity.BlogText = request.BlogText;
        entity.EstimatedReadTime = request.EstimatedReadTime;
        entity.BlogCategoryId = request.BlogCategoryId;
        entity.ImageAlt = request.ImageAlt;
        entity.MetaDescription = request.MetaDescription;
        entity.ShowBlog = request.ShowBlog;
        entity.Slug = slug;

        if (request.Image != null)
        {
            // delete old image
            if(File.Exists(entity.Image))
                File.Delete(entity.Image);

            // Define the directory for uploads 
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "images");

            // Create directory if not Exist
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            // Build file name
            var newFileName = Guid.NewGuid().ToString() + Path.GetExtension(request.Image.FileName);
            var imagePath = Path.Combine(uploadPath, newFileName);

            // Save Image
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await request.Image.CopyToAsync(stream);
            }
            entity.Image = imagePath;
        }

        _unitOfWork.BlogRepository.Update(entity);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseDto<string>()
        {
            data = null,
            message = "عملیات موفقیت آمیز بود",
            is_success = true,
            response_code = 204
        };
    }
}
