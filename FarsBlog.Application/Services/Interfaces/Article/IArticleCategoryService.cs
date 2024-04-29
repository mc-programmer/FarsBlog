using FarsBlog.Domain.DTOs.ViewModels.Article.Category;
using FarsBlog.Domain.Shared;
using System.Runtime.CompilerServices;

namespace FarsBlog.Application.Services.Interfaces.Article;

public interface IArticleCategoryService
{
    #region Common

    Task<Result<ArticleCategoryDetailsViewModel>> GetArticleCategoryByIdAsync(int categoryId);
    Task<Result<AdminSideUpdateArticleCategoryViewModel>> GetArticleCategoryByIdForAdminUpdate(int categoryId);

    #endregion

    #region Admin

    Task<Result<FilterArticleCategoryViewModel>> FilterArticleCategoriesAsync(FilterArticleCategoryViewModel filter);
    Task<Result> CreateArticleCategoryAsync(AdminSideCreateArticleCategoryViewModel model);
    Task<Result> UpdateArticleCategoryAsync(AdminSideUpdateArticleCategoryViewModel model);
    Task<Result> DeleteArticleCategoryAsync(int categoryId);
    Task<Result> RecoverArticleCategoryAsync(int id);

    #endregion
}
