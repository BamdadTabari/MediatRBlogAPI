using MediatR;
using MediatRBlogAPI.Application.Base;
using System.ComponentModel.DataAnnotations;

namespace MediatRBlogAPI.Application.Features.Posts.Commands;

public class CreatePostCommand : IRequest<ResponseDto<string>>
{
	[Display(Name = "نام")]
	[Required(ErrorMessage = "لطفا مقدار {0} را وارد کنید")]
	public string Name { get; set; }
	[Display(Name = "توضیحات کوتاه")]
	[Required(ErrorMessage = "لطفا مقدار {0} را وارد کنید")]
	public string ShortDescription { get; set; }
	[Display(Name = "تصویر شاخص")]
	public IFormFile? Image { get; set; }
	[Display(Name = "نامک")]
	[Required(ErrorMessage = "لطفا مقدار {0} را وارد کنید")]
	public string ImageAlt { get; set; }
	[Display(Name = "متن مقاله")]
	[Required(ErrorMessage = "لطفا مقدار {0} را وارد کنید")]
	public string BlogText { get; set; }
	[Display(Name = "آیا نمایش داده شود؟")]
	[Required(ErrorMessage = "لطفا مقدار {0} را وارد کنید")]
	public bool ShowBlog { get; set; }
	[Display(Name = "کلمات کلیدی")]
	[Required(ErrorMessage = "لطفا مقدار {0} را وارد کنید")]
	public string KeyWords { get; set; }
	[Display(Name = "Meta Description")]
	[Required(ErrorMessage = "لطفا مقدار {0} را وارد کنید")]
	public string MetaDescription { get; set; }
	[Display(Name = "زمان تقریبی مطالعه")]
	[Required(ErrorMessage = "لطفا مقدار {0} را وارد کنید")]
	public int EstimatedReadTime { get; set; }

	[Display(Name = "دسته بندی مقاله")]
	[Required(ErrorMessage = "لطفا مقدار {0} را وارد کنید")]
	public int BlogCategoryId { get; set; }

    public string? Slug { get; set; }
}
