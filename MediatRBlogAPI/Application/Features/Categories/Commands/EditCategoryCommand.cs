using MediatR;
using MediatRBlogAPI.Application.Base;
using System.ComponentModel.DataAnnotations;

namespace MediatRBlogAPI.Application.Features.Categories.Commands;

public class EditCategoryCommand : IRequest<ResponseDto<string>>
{
    [Display(Name = "آیدی دسته بندی")]
    [Required(ErrorMessage = "لطفا مقدار {0} را وارد کنید")]
    public long Id { get; set; }
    [Display(Name = "نام")]
    [Required(ErrorMessage = "لطفا مقدار {0} را وارد کنید")]
    public string Name { get; set; }
    [Display(Name = "توضیحات")]
    public string? Description { get; set; }
    [Display(Name = "نامک")]
    public string? Slug { get; set; }
}
