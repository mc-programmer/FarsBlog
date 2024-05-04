using FarsBlog.Domain.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace FarsBlog.Domain.Models.Article;

public class Article : BaseEntity
{
    #region Properties

    [MaxLength(250)]
    public string? Title { get; set; }

    [MaxLength(300)]
    public string? Slug { get; set; }

    [MaxLength(800)]
    public string? ShortDescription { get; set; }
    public string? ImageName { get; set; }
    public string? ImageAlt{ get; set; }
    public string? Text { get; set; }
    public bool IsPublished { get; set; }

    #endregion

    #region Relations

    public ICollection<ArticleCategoryMapping>? Categories { get; set; }

    #endregion
}