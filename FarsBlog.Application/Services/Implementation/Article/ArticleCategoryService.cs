using FarsBlog.Application.Mappers.Article;
using FarsBlog.Application.Services.Interfaces.Article;
using FarsBlog.Domain.DTOs.ViewModels.Article.Category;
using FarsBlog.Domain.DTOs.ViewModels.Common.Filter;
using FarsBlog.Domain.Enums.Common;
using FarsBlog.Domain.Interfaces.Article;
using FarsBlog.Domain.Models.Article;
using FarsBlog.Domain.Shared;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Result<ArticleCategoryDetailsViewModel>> GetArticleCategoryByIdAsync(int categoryId)
    {
        if (categoryId <= 0) return Result.Failure<ArticleCategoryDetailsViewModel>(ErrorMessages.NullValue);

        var articleCategory = await _articleCategoryRepository.GetByIdAsync(categoryId);
        if (articleCategory is null) return Result.Failure<ArticleCategoryDetailsViewModel>(ErrorMessages.NotFoundErorr);

        return new ArticleCategoryDetailsViewModel().MapFrom(articleCategory);
    }
    public async Task<Result<AdminSideUpsertArticleCategoryViewModel>> GetArticleCategoryByIdForAdminUpdate(int categoryId)
    {
        if (categoryId <= 0) return Result.Failure<AdminSideUpsertArticleCategoryViewModel>(ErrorMessages.NullValue);

        var articleCategory = await _articleCategoryRepository.GetByIdAsync(categoryId);
        if (articleCategory is null) return Result.Failure<AdminSideUpsertArticleCategoryViewModel>(ErrorMessages.NotFoundErorr);

        return new AdminSideUpsertArticleCategoryViewModel().MapFrom(articleCategory);
    }
    public async Task<Result<bool>> ValidateArticleCategorySlugAsync(string slug, int? articleCategoryId = null)
    {
        var articleCategoryWithSameSlug = await _articleCategoryRepository
            .GetAllAsync(filter: a => !a.IsDelete && a.Slug == slug.Trim() && a.Id != articleCategoryId);

        if (articleCategoryWithSameSlug is not null && articleCategoryWithSameSlug.Any()) return false;

        return true;
    }
    public async Task<Result<bool>> ValidateArticleCategoryTitleAsync(string title, int? articleCategoryId = null)
    {
        var articleCategoryWithSameTitle = await _articleCategoryRepository
            .GetAllAsync(filter: a => !a.IsDelete && a.Title == title.Trim() && a.Id != articleCategoryId);

        if (articleCategoryWithSameTitle is not null && articleCategoryWithSameTitle.Any()) return false;

        return true;
    }

    #endregion

    #region ClientSide

    #endregion

    #region AdminSide

    public async Task<Result<FilterArticleCategoryViewModel>> FilterArticleCategoriesAsync(FilterArticleCategoryViewModel filter)
    {
        var filterConditions = Filter.GenerateConditions<ArticleCategory>();

        if (!string.IsNullOrEmpty(filter.Title))
            filterConditions.Add(articleCategory => EF.Functions.Like(articleCategory.Title, $"%{filter.Title.Trim()}%"));

        switch (filter.DeletedStatus)
        {
            case DeletedStatus.All:
                break;
            case DeletedStatus.Deleted:
                filterConditions.Add(articleCategory => articleCategory.IsDelete);
                break;
            case DeletedStatus.NotDeleted:
            default:
                filterConditions.Add(articleCategory => !articleCategory.IsDelete);
                break;
        }

        await _articleCategoryRepository.FilterAsync(filter, filterConditions, ArticleCategoryMapper.MapArticleCategoryDetailsViewModel);

        return filter;
    }
    public async Task<Result> CreateArticleCategoryAsync(AdminSideUpsertArticleCategoryViewModel model)
    {
        if (model is null) return Result.Failure(ErrorMessages.NullValue);

        var articleCategory = new ArticleCategory().MapFrom(model);

        var isModelValid = await ValidateArticleCategoryTitleAsync(model.Title ?? "");
        if (!isModelValid.Value) return Result.Failure(ErrorMessages.TitleExistError);

        isModelValid = await ValidateArticleCategorySlugAsync(model.Slug ?? "");
        if (!isModelValid.Value) return Result.Failure(ErrorMessages.SlugExistError);

        await _articleCategoryRepository.InsertAsync(articleCategory);
        await _articleCategoryRepository.SaveAsync();

        return Result.Success(SuccessMessages.SuccessfullyDone);
    }
    public async Task<Result> UpdateArticleCategoryAsync(AdminSideUpsertArticleCategoryViewModel model)
    {
        if (model is null || !model.Id.HasValue) return Result.Failure(ErrorMessages.NullValue);

        var articleCategoryFromDatabase = await _articleCategoryRepository.GetByIdAsync(model.Id.Value);
        if (articleCategoryFromDatabase is null) return Result.Failure(ErrorMessages.NotFoundErorr);

        var isModelValid = await ValidateArticleCategoryTitleAsync(model.Title ?? "", model.Id);
        if (!isModelValid.Value) return Result.Failure(ErrorMessages.TitleExistError);

        isModelValid = await ValidateArticleCategorySlugAsync(model.Slug ?? "", model.Id);
        if (!isModelValid.Value) return Result.Failure(ErrorMessages.SlugExistError);

        articleCategoryFromDatabase = articleCategoryFromDatabase.MapFrom(model);

        _articleCategoryRepository.Update(articleCategoryFromDatabase);
        await _articleCategoryRepository.SaveAsync();

        return Result.Success(SuccessMessages.SuccessfullyDone);
    }
    public async Task<Result> DeleteArticleCategoryAsync(int categoryId)
    {
        if (categoryId <= 0) return Result.Failure(ErrorMessages.NullValue);

        var articleCategoryToBeDeleted = await _articleCategoryRepository.GetByIdAsync(categoryId);
        if (articleCategoryToBeDeleted is null) return Result.Failure(ErrorMessages.NotFoundErorr);

        articleCategoryToBeDeleted.IsDelete = true;

        _articleCategoryRepository.Update(articleCategoryToBeDeleted);
        await _articleCategoryRepository.SaveAsync();

        return Result.Success(SuccessMessages.SuccessfullyDone);
    }
    public async Task<Result> RecoverArticleCategoryAsync(int categoryId)
    {
        if (categoryId <= 0) return Result.Failure(ErrorMessages.NullValue);

        var articleCategoryToBeRecovered = await _articleCategoryRepository.GetByIdAsync(categoryId);
        if (articleCategoryToBeRecovered is null) return Result.Failure(ErrorMessages.NotFoundErorr);

        var isValid = await ValidateArticleCategoryTitleAsync(articleCategoryToBeRecovered.Title ?? "", categoryId);
        if (!isValid.Value) return Result.Failure(ErrorMessages.TitleExistError);

        isValid = await ValidateArticleCategorySlugAsync(articleCategoryToBeRecovered.Slug ?? "", categoryId);
        if (!isValid.Value) return Result.Failure(ErrorMessages.SlugExistError);

        articleCategoryToBeRecovered.IsDelete = false;

        _articleCategoryRepository.Update(articleCategoryToBeRecovered);
        await _articleCategoryRepository.SaveAsync();

        return Result.Success(SuccessMessages.SuccessfullyDone);
    }

    #endregion
}