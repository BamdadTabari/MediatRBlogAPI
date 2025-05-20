using System.ComponentModel.DataAnnotations;

namespace DimoLand;

public class BaseViewModel
{
    [Display(Name = "آیدی")]
    public int Id { get; set; }
    [Display(Name = "نامک (اختیاری)")]
    public string? Slug { get; set; }
    [Display(Name = "زمان ایجاد")]
    public DateTime CreatedAt { get; set; }
    [Display(Name = "زمان ویرایش")]
    public DateTime UpdatedAt { get; set; }
}
