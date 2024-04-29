using FarsBlog.Domain.Interfaces.Article;
using FarsBlog.Infra.Data.Context;
using FarsBlog.Infra.Data.Repositories.Common;

namespace FarsBlog.Infra.Data.Repositories.Article;

public class ArticleRepository : EfRepository<Domain.Models.Article.Article,int> , IArticleRepository
{
    public ArticleRepository(FarsBlogDbContext context) : base(context) { }
}