using FarsBlog.Application.Services.Interfaces.Article;
using FarsBlog.Domain.DTOs.ViewModels.Article.Category;
using FarsBlog.Domain.Interfaces.Article;
using FarsBlog.Domain.Shared;

namespace FarsBlog.Application.Services.Implementation.Article;

public class ArticleCategoryService : IArticleCategoryService
{
    #region Fields

    private readonly IArticleCategoryRepository _articleCategoryRepository;

    #endregion

    #region Constructor

    public ArticleCategoryService(IArticleCategoryRepository articleCategoryRepository)
    {
        _articleCategoryRepository = articleCategoryRepository;
    }

    #endregion

    #region Common

    public Task<Result<ArticleCategoryDetailsViewModel>> GetArticleCategoryByIdAsync(int categoryId)
    {
        throw new NotImplementedException();
    }


    #endregion

    #region ClientSide

    #endregion

    #region AdminSide

    public async Task<Result<FilterArticleCategoryViewModel>> FilterArticleCategoriesAsync(FilterArticleCategoryViewModel filter)
    {
        if (filter is null) return Result.Failure<FilterArticleCategoryViewModel>(ErrorMessages.NullValue);

        throw new NotImplementedException();
    }

    public Task<Result> CreateArticleCategoryAsync(AdminSideUpsertArticleCategoryViewModel model)
    {
        throw new NotImplementedException();
    }

    public Task<Result> UpdateArticleCategoryAsync(AdminSideUpsertArticleCategoryViewModel model)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteArticleCategoryAsync(int categoryId)
    {
        throw new NotImplementedException();
    }

    public Task<Result> RecoverArticleCategoryAsync(int categoryId)
    {
        throw new NotImplementedException();
    }

    #endregion
}
