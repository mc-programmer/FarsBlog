using FarsBlog.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace FarsBlog.Domain.DTOs.ViewModels.Article;

public class AdminSideCreateArticleViewModel
{
    [Display(Name ="عنوان")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(250,ErrorMessage =ErrorMessages.MaxLengthError)]
    public string? Title { get; set; }

    [Display(Name = "عنوان در Url")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(300, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string? Slug { get; set; }
   
    [Display(Name = "توضیحات کوتاه")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(800, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string? ShortDescription { get; set; }

    [Display(Name = "متن")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    public string? Text { get; set; }

    public bool IsPublished { get; set; }
}
