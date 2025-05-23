using MediatR;
using MediatRBlogAPI.Application.Base;
using MediatRBlogAPI.Application.Features.Categories.Response;
using System.ComponentModel.DataAnnotations;

namespace MediatRBlogAPI.Application.Features.Categories.Query;

public class GetCategoryBySlugQuery : IRequest<ResponseDto<PostCategoryResponse>>
{
    [Display(Name = "نامک")]
    [Required(ErrorMessage = "لطفا مقدار {0} را وارد کنید")]
    public string Slug { get; set; }
}
