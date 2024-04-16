using FarsBlog.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace FarsBlog.Domain.DTOs.ViewModels.Article.Category;

public class AdminSideUpsertArticleCategoryViewModel
{
    public int? Id { get; set; }

    [Display(Name ="عنوان")]
    [Required(ErrorMessage =ErrorMessages.RequiredError)]
    [MaxLength(60,ErrorMessage = ErrorMessages.MaxLengthError)]
    public required string Title { get; set; }

    [Display(Name ="عنوان در URL")]
    [Required(ErrorMessage =ErrorMessages.RequiredError)]
    [MaxLength(60,ErrorMessage = ErrorMessages.MaxLengthError)]
    public required string Slug { get; set; }

    [Display(Name ="توضیحات کوتاه")]
    [MaxLength(100, ErrorMessage =ErrorMessages.MaxLengthError)]
    public string? ShortDescription { get; set; }

    [Display(Name = "توضیحات")]
    [MaxLength(500,ErrorMessage = ErrorMessages.MaxLengthError)]
    public string? Description { get; set; }

    [Display(Name ="عنوان عکس در URL")]
    [MaxLength(50,ErrorMessage = ErrorMessages.MaxLengthError)]
    public string? ImageAlt { get; set; }
}
