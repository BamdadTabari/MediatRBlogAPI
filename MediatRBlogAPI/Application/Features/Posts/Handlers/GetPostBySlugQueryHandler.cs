using DataLayer;
using MediatR;
using MediatRBlogAPI.Application.Base;
using MediatRBlogAPI.Application.Features.Posts.Commands;
using MediatRBlogAPI.Application.Features.Posts.Query;
using MediatRBlogAPI.Application.Features.Posts.Response;
using MediatRBlogAPI.Application.Mappers;

namespace MediatRBlogAPI.Application.Features.Posts.Handlers;

public class GetPostBySlugQueryHandler : IRequestHandler<GetPostBySlugQuery, ResponseDto<PostResponse>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetPostBySlugQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<ResponseDto<PostResponse>> Handle(GetPostBySlugQuery request, CancellationToken cancellationToken)
	{

		var data = await _unitOfWork.BlogRepository.Get(request.Slug);
		
		if (data == null)
			return new ResponseDto<PostResponse>()
			{
				data = null,
				is_success = false,
				message = "مقاله پیدا نشد",
				response_code = 404,
			};

		return new ResponseDto<PostResponse>()
		{
			data = data.Map(),
			is_success = true,
			message = "عملیات موفقیت آمیز بود",
			response_code = 201
		};
	}
}
