using MediatR;
using MediatRBlogAPI.Application.Base;
using MediatRBlogAPI.Application.Features.Categories.Response;

namespace MediatRBlogAPI.Application.Features.Categories.Query;

public class GetAllCategoryQuery : IRequest<ResponseDto<List<PostCategoryResponse>>>
{
}
