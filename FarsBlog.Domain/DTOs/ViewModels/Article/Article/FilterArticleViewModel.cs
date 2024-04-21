using FarsBlog.Domain.DTOs.ViewModels.Common;

namespace FarsBlog.Domain.DTOs.ViewModels.Article.Article;

public class FilterArticleViewModel : BasePaging<AdminSideArticleDetailsViewModel>
{
    public string? Title { get; set; }
}