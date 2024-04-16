using FarsBlog.Domain.DTOs.ViewModels.Article.Category;
using FarsBlog.Domain.Models.Article;
using System.Linq.Expressions;

namespace FarsBlog.Application.Mappers.Article;

public static class ArticleCategoryMapper
{
    public static Expression<Func<ArticleCategory, ArticleCategoryDetailsViewModel>> MapArticleCategoryDetailsViewModel => (ArticleCategory articleCategory) => new()
    {
        Title = articleCategory.Title,
        Description = articleCategory.Description,
        ParentId = articleCategory.ParentId,
        ImageAlt = articleCategory.ImageAlt,
        ImageName = articleCategory.ImageName,
        ShortDescription = articleCategory.ShortDescription,
        IsDelete = articleCategory.IsDelete
    };

    public static ArticleCategory MapFrom(this ArticleCategory model, AdminSideUpsertArticleCategoryViewModel articleCategory)
    {
        if (articleCategory.Id.HasValue) model.Id = articleCategory.Id.Value;
        model.Title = articleCategory.Title;
        model.Slug = articleCategory.Slug;
        model.ImageAlt = articleCategory.ImageAlt;
        model.ImageName = articleCategory.ImageName;
        model.ShortDescription = articleCategory.ShortDescription;
        model.Description = articleCategory.Description;

        return model;
    }

    public static AdminSideUpsertArticleCategoryViewModel MapFrom(this AdminSideUpsertArticleCategoryViewModel articleCategory, ArticleCategory model) => new()
    {
        Id = articleCategory.Id,
        Title = articleCategory.Title,
        Slug = articleCategory.Slug,
        Description = articleCategory.Description,
        ImageAlt = articleCategory.ImageAlt,
        ImageName = articleCategory.ImageName,
        ShortDescription = articleCategory.ShortDescription,
    };

    public static ArticleCategoryDetailsViewModel MapFrom(this ArticleCategoryDetailsViewModel articleCategory, ArticleCategory model) => new()
    {
        Id = articleCategory.Id,
        Title = articleCategory.Title,
        Slug = articleCategory.Slug,
        ParentId = articleCategory.ParentId,
        Description = articleCategory.Description,
        ImageAlt = articleCategory.ImageAlt,
        ImageName = articleCategory.ImageName,
        ShortDescription = articleCategory.ShortDescription,
    };
}