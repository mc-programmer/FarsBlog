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

    public static ArticleCategory MapFrom(this ArticleCategory model , AdminSideUpsertArticleCategoryViewModel articleCategory)
    {
        model.Id = articleCategory.Id.HasValue ? articleCategory.Id.Value : 0;
        model.ImageAlt = articleCategory.ImageAlt;
        model.ImageName = articleCategory.ImageName;
        model.ShortDescription = articleCategory.ShortDescription;
        model.Slug = articleCategory.Slug;
        model.Title = articleCategory.Title;
        model.Description = articleCategory.Description;

        return model;
    }
}