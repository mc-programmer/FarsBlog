namespace FarsBlog.Domain.DTOs.ViewModels.Article.Category;

public class ArticleCategoryDetailsViewModel
{
    public string? Title { get; set; }
    public stirng? ShortDescription { get; set; }
    public string? Description { get; set; }
    public int? ParentId { get; set; }
    public string? ImageName { get; set; }
    public string? ImageAlt { get; set; }
    public bool IsActive { get; set; }
    public bool IsDelete { get; set; }
}
