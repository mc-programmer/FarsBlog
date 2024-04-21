using FarsBlog.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace FarsBlog.Domain.DTOs.ViewModels.Article.Article;

public class AdminSideUpsertArticleViewModel
{
    public int? Id { get; set; }

    [Display(Name = "عنوان")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(200, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string? Title { get; set; }

    [Display(Name = "عنوان در URL")]
    [Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(200, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string? Slug { get; set; }
}