using MediatR;
using MediatRBlogAPI.Application.Base;
using System.ComponentModel.DataAnnotations;

namespace MediatRBlogAPI.Application.Features.Categories.Commands;

public class DeleteCategoryCommand : IRequest<ResponseDto<string>>
{
    [Display(Name = "آیدی دسته بندی")]
    [Required(ErrorMessage = "لطفا مقدار {0} را وارد کنید")]
    public long Id { get; set; }
}
