using FarsBlog.Domain.DTOs.ViewModels.Common;
using System.ComponentModel.DataAnnotations;

namespace FarsBlog.Domain.DTOs.ViewModels.Article.Category;

public class FilterArticleCategoryViewModel:BasePaging<ArticleCategoryDetailsViewModel>
{
    [Display(Name ="عنوان")]
    public string? Title { get; set; }

    [Display(Name = "حذف شده؟")]
    public bool? IsDelete { get; set; }
}
