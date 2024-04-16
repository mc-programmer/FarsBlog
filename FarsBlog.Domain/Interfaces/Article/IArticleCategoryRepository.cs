using FarsBlog.Domain.Models.Article;

namespace FarsBlog.Domain.Interfaces.Article
{
    public interface IArticleCategoryRepository : IRepository<ArticleCategory, int>
    {
    }
}