using FarsBlog.Domain.DTOs.ViewModels.Article;
using System.Linq.Expressions;

namespace FarsBlog.Application.Mappers.Article;

public static class ArticleMapper
{
    public static Expression<Func<Domain.Models.Article.Article, AdminSideArticleDetailsForFilterViewModel>> MapArticleDetailsViewModel => (Domain.Models.Article.Article article) => new()
    {
        Id = article.Id,
        Title = article.Title,
        IsDelete = article.IsDelete,
        IsPublished = article.IsPublished,
        CreateDateOnUtc = article.CreateDateOnUtc
    };
}
