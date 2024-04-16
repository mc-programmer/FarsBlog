using FarsBlog.Domain.Interfaces.Article;
using FarsBlog.Domain.Models.Article;
using FarsBlog.Infra.Data.Context;
using FarsBlog.Infra.Data.Repositories.Common;

namespace FarsBlog.Infra.Data.Repositories.Article;

public class ArticleCategoryRepository:EfRepository<ArticleCategory,int>, IArticleCategoryRepository
{
    public ArticleCategoryRepository(FarsBlogDbContext context) : base(context) { }
}