using FarsBlog.Domain.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace FarsBlog.Domain.Models.Article;

public class ArticleCategory : BaseEntity<int>
{
    #region Properties

    [MaxLength(60)]
    public string? Title { get; set; }

    [MaxLength(60)]
    public string? Slug { get; set; }

    [MaxLength(50)]
    public string? CoverName { get; set; }

    #endregion

    #region Relations

    public virtual ArticleCategory? Parent { get;set; }

    #endregion
}
