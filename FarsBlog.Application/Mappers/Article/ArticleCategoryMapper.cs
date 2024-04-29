using FarsBlog.Domain.DTOs.ViewModels.Article.Category;
using FarsBlog.Domain.Models.Article;
using System.Linq.Expressions;

namespace FarsBlog.Application.Mappers.Article;

public static class ArticleCategoryMapper
{
    public static Expression<Func<ArticleCategory, ArticleCategoryDetailsViewModel>> MapArticleCategoryDetailsViewModel => (ArticleCategory articleCategory) => new()
    {
        Id = articleCategory.Id,
        Title = articleCategory.Title, 
        IsDelete = articleCategory.IsDelete
    };
    public static ArticleCategory MapFrom(this ArticleCategory model, AdminSideCreateArticleCategoryViewModel articleCategory)
    {
        model.Title = articleCategory.Title?.Trim();
        model.Slug = articleCategory.Slug?.Trim();
        model.CoverName = articleCategory.CoverImageName;

        return model;
    }

    public static ArticleCategory MapFrom(this ArticleCategory model, AdminSideUpdateArticleCategoryViewModel articleCategory)
    {
        if (articleCategory.Id.HasValue) model.Id = articleCategory.Id.Value;
        model.Title = articleCategory.Title?.Trim();
        model.Slug = articleCategory.Slug?.Trim();
        model.CoverName = articleCategory.CoverName;

        return model;
    }
    public static AdminSideUpdateArticleCategoryViewModel MapFrom(this AdminSideUpdateArticleCategoryViewModel articleCategory, ArticleCategory model) => new()
    {
        Id = model.Id,
        Title = model.Title?.Trim(),
        Slug = model.Slug?.Trim(),
        CoverName = model.CoverName
    };
    public static ArticleCategoryDetailsViewModel MapFrom(this ArticleCategoryDetailsViewModel articleCategory, ArticleCategory model) => new()
    {
        Id = articleCategory.Id,
        Title = articleCategory.Title?.Trim(),
        Slug = articleCategory.Slug?.Trim(),
        CoverName = articleCategory.CoverName
    };
}