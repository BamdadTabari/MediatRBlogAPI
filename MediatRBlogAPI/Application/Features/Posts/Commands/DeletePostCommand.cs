using MediatR;
using MediatRBlogAPI.Application.Base;
using System.ComponentModel.DataAnnotations;

namespace MediatRBlogAPI.Application.Features.Posts.Commands;

public class DeletePostCommand : IRequest<ResponseDto<string>>
{
    [Display(Name = "آیدی مقاله")]
    [Required(ErrorMessage = "لطفا مقدار {0} را وارد کنید")]
    public long Id { get; set; }
}
