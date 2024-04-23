namespace FarsBlog.Domain.DTOs.ViewModels.Article.Category;

public class ArticleCategoryDetailsViewModel
{
    public int? Id { get; set; }
    public string? Title { get; set; }
    public string? Slug { get; set; }
    public string? CoverName { get; set; }
    public bool IsDelete { get; set; }
}