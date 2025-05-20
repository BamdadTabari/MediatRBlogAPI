using DataLayer;
using MediatR;
using MediatRBlogAPI.Application.Features.Posts.Commands;
using Microsoft.Extensions.Hosting;

namespace MediatRBlogAPI.Application.Features.Posts.Handlers;

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, long>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreatePostCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<long> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
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
        };

        await _unitOfWork.BlogRepository.AddAsync(post);
        await _unitOfWork.CommitAsync(cancellationToken);
        return post.Id;
    }
}
