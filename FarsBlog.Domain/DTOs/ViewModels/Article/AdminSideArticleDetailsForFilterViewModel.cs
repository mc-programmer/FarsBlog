namespace FarsBlog.Domain.DTOs.ViewModels.Article;

public class AdminSideArticleDetailsForFilterViewModel
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public bool IsPublished { get; set; }
    public bool IsDelete { get; set; }
    public DateTime CreateDateOnUtc { get; set; }
}