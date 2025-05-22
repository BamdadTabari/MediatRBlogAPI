using DataLayer;
using MediatR;
using MediatRBlogAPI.Application.Base;
using MediatRBlogAPI.Application.Features.Posts.Commands;
using Microsoft.Extensions.Hosting;

namespace MediatRBlogAPI.Application.Features.Posts.Handlers;

public class CreatePostCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreatePostCommand, ResponseDto<string>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ResponseDto<string>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        if(await _unitOfWork.BlogRepository.ExistsAsync(x=>x.Name == request.Name))
            return new ResponseDto<string>()
            {
                data = null,
                message = "مقاله با این نام وجود دارد" ,
                is_success = false,
                response_code = 400
            };

        var slug = request.Slug ?? SlugHelper.GenerateSlug(request.Name);
        if (await _unitOfWork.BlogRepository.ExistsAsync(x => x.Slug == slug))
            return new ResponseDto<string>()
            {
                data = slug,
                message = "مقاله با این نامک وجود دارد",
                is_success = false,
                response_code = 400
            };

        var post = new Blog
        {
            Name = request.Name,
            BlogCategoryId = request.BlogCategoryId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            BlogText = request.BlogText,
            ImageAlt = request.ImageAlt,
            ShortDescription = request.ShortDescription,
            KeyWords = request.KeyWords,
            ShowBlog = request.ShowBlog,
            MetaDescription = request.MetaDescription,
            EstimatedReadTime = request.EstimatedReadTime,
            Slug = slug,
        };
		if (request.Image != null)
		{
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
			post.Image = imagePath;
		}
        else
        {
            return new ResponseDto<string>() 
            {
                data = null,
                is_success = false,
                message = "لطفا تصویر شاخص را آپلود کنید",
                response_code = 400
			};
        }

        await _unitOfWork.BlogRepository.AddAsync(post);
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
