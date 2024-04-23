using FarsBlog.Domain.DTOs.ViewModels.Article.Category;
using FarsBlog.Domain.Shared;
using System.Runtime.CompilerServices;

namespace FarsBlog.Application.Services.Interfaces.Article;

public interface IArticleCategoryService
{
    #region Common

    Task<Result<ArticleCategoryDetailsViewModel>> GetArticleCategoryByIdAsync(int categoryId);
    Task<Result<AdminSideCreateArticleCategoryViewModel>> GetArticleCategoryByIdForAdminUpdate(int categoryId);
    Task<Result<bool>> ValidateArticleCategorySlugAsync(string slug,int? articleCategoryId = null);
    Task<Result<bool>> ValidateArticleCategoryTitleAsync(string title,int? articleCategoryId = null);

    #endregion

    #region Admin

    Task<Result<FilterArticleCategoryViewModel>> FilterArticleCategoriesAsync(FilterArticleCategoryViewModel filter);
    Task<Result> CreateArticleCategoryAsync(AdminSideCreateArticleCategoryViewModel model);
    Task<Result> UpdateArticleCategoryAsync(AdminSideCreateArticleCategoryViewModel model);
    Task<Result> DeleteArticleCategoryAsync(int categoryId);

    #endregion
}
