using System.ComponentModel.DataAnnotations.Schema;

namespace FarsBlog.Domain.Models.Article;

public class ArticleCategoryMapping
{
    #region Properties

    public int CategoryId { get; set; }
    public int ArticleId { get; set; }

    #endregion

    #region Relations

    [ForeignKey(nameof(ArticleId))]
    public Article? Article { get; set; }

    [ForeignKey(nameof(CategoryId))]
    public ArticleCategory? ArticleCategory { get; set; }

    #endregion
}
