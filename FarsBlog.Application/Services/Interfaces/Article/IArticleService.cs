using FarsBlog.Domain.DTOs.ViewModels.Article.Article;
using FarsBlog.Domain.Shared;

namespace FarsBlog.Application.Services.Interfaces.Article;

public interface IArticleService
{
    #region Common

    #endregion

    #region AdminPanel

    Task<FilterArticleViewModel> FilterAsync(FilterArticleViewModel filterArticleViewModel);
    Task<AdminSideArticleDetailsViewModel> GetByIdForAdminAsync(int id);
    Task<Result> CreateAsync(AdminSideUpsertArticleViewModel model);
    Task<Result> UpdateAsync(AdminSideUpsertArticleViewModel model);
    Task<Result> DeleteAsync(int id);
    Task<Result> RecoverAsync(int id);

    #endregion
}
