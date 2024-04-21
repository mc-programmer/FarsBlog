using FarsBlog.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace FarsBlog.Domain.DTOs.ViewModels.Article.Article;

public class AdminSideArticleDetailsViewModel
{
    public int? Id { get; set; }
    public string? Title { get; set; }
    public string? Slug { get; set; }
}
