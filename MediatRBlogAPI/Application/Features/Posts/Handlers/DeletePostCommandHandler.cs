using DataLayer;
using MediatR;
using MediatRBlogAPI.Application.Base;
using MediatRBlogAPI.Application.Features.Posts.Commands;

namespace MediatRBlogAPI.Application.Features.Posts.Handlers;

public class DeletePostCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeletePostCommand, ResponseDto<string>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ResponseDto<string>> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.BlogRepository.Get(request.Id);
        if (entity == null)
            return new ResponseDto<string>()
            {
                data = null,
                message = "مقاله پیدا نشد",
                is_success = false,
                response_code = 404
            };

        _unitOfWork.BlogRepository.Remove(entity);
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
