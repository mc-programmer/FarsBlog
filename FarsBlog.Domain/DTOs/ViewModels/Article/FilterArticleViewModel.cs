using FarsBlog.Domain.DTOs.ViewModels.Common;
using FarsBlog.Domain.Enums.Common;
using FarsBlog.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace FarsBlog.Domain.DTOs.ViewModels.Article;

public class FilterArticleViewModel:BasePaging<AdminSideArticleDetailsForFilterViewModel>
{
    [Display(Name ="عنوان")]
    [MaxLength(60,ErrorMessage = ErrorMessages.MaxLengthError)]
    public string? Search { get; set; }

    [Display(Name ="وضعیت دسترسی")]
    public DeletedStatus DeletedStatus{ get; set; }
}
