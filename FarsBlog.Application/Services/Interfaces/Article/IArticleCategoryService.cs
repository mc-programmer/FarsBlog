using FarsBlog.Domain.DTOs.ViewModels.Article.Category;
using FarsBlog.Domain.Shared;

namespace FarsBlog.Application.Services.Interfaces.Article;

public interface IArticleCategoryService
{
    #region Common

    Task<Result<ArticleCategoryDetailsViewModel>> GetArticleCategoryByIdAsync(int categoryId);

    #endregion

    #region Admin

    Task<Result<FilterArticleCategoryViewModel>> FilterArticleCategoriesAsync(FilterArticleCategoryViewModel filter);
    Task<Result> CreateArticleCategoryAsync(AdminSideUpsertArticleCategoryViewModel model);
    Task<Result> UpdateArticleCategoryAsync(AdminSideUpsertArticleCategoryViewModel model);
    Task<Result> DeleteArticleCategoryAsync(int categoryId);
    Task<Result> RecoverArticleCategoryAsync(int categoryId);

    #endregion
}
