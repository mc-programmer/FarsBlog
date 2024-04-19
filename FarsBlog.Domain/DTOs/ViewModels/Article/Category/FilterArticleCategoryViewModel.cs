using FarsBlog.Domain.DTOs.ViewModels.Common;
using FarsBlog.Domain.Enums.Common;
using System.ComponentModel.DataAnnotations;

namespace FarsBlog.Domain.DTOs.ViewModels.Article.Category;

public class FilterArticleCategoryViewModel:BasePaging<ArticleCategoryDetailsViewModel>
{
    [Display(Name ="عنوان")]
    public string? Title { get; set; }

    [Display(Name = "وضعیت")]
    public DeletedStatus? DeletedStatus { get; set; }
}