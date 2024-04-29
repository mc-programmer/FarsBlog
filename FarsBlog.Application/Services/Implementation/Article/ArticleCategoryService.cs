using FarsBlog.Application.Extentions;
using FarsBlog.Application.Mappers.Article;
using FarsBlog.Application.Services.Interfaces.Article;
using FarsBlog.Application.Statics;
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
    public async Task<Result<AdminSideUpdateArticleCategoryViewModel>> GetArticleCategoryByIdForAdminUpdate(int categoryId)
    {
        if (categoryId <= 0) return Result.Failure<AdminSideUpdateArticleCategoryViewModel>(ErrorMessages.NullValue);

        var articleCategory = await _articleCategoryRepository.GetByIdAsync(categoryId);
        if (articleCategory is null || articleCategory.IsDelete) return Result.Failure<AdminSideUpdateArticleCategoryViewModel>(ErrorMessages.NotFoundErorr);

        return new AdminSideUpdateArticleCategoryViewModel().MapFrom(articleCategory);
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
    public async Task<Result> CreateArticleCategoryAsync(AdminSideCreateArticleCategoryViewModel model)
    {
        #region Validate Model

        if (model is null) return Result.Failure(ErrorMessages.NullValue);

        #endregion

        if (model.CoverImage is not null)
        {
            model.CoverImageName = Guid.NewGuid() + Path.GetExtension(model.CoverImage.FileName);

            if (SiteTools.ArticleCategory is null) return Result.Failure(ErrorMessages.OperationFailedError);

            var result = model.CoverImage.AddImageToServer(model.CoverImageName, SiteTools.ArticleCategory, 400, 280, SiteTools.ArticleCategoryThumb);

            if (result.IsFailure) return Result.Failure("خطا در افزودن تصویر");
        }

        #region Validate Slug and Title

        var isModelValid = await _articleCategoryRepository.IsValidAsync(nameof(model.Title), model.Title!);
        if (!isModelValid) return Result.Failure(ErrorMessages.TitleExistError);

        isModelValid = await _articleCategoryRepository.IsValidAsync(nameof(model.Slug), model.Slug!);
        if (!isModelValid) return Result.Failure(ErrorMessages.SlugExistError);

        #endregion

        #region Map And Insert

        var articleCategory = new ArticleCategory().MapFrom(model);

        await _articleCategoryRepository.InsertAsync(articleCategory);
        await _articleCategoryRepository.SaveAsync();

        #endregion

        return Result.Success(SuccessMessages.SuccessfullyDone);
    }
    public async Task<Result> UpdateArticleCategoryAsync(AdminSideUpdateArticleCategoryViewModel model)
    {
        #region Validate Model

        if (model is null || !model.Id.HasValue) return Result.Failure(ErrorMessages.NullValue);

        #endregion

        if (model.CoverImage is not null)
        {
            string updatedImageName = Guid.NewGuid() + Path.GetExtension(model.CoverImage.FileName);

            if (SiteTools.ArticleCategory is null) return Result.Failure(ErrorMessages.OperationFailedError);

            var result = model.CoverImage.AddImageToServer(updatedImageName, SiteTools.ArticleCategory,
                400, 280, SiteTools.ArticleCategoryThumb, model.CoverName);

            if (result.IsFailure) return Result.Failure("خطا در ,ویرایش تصویر");

            model.CoverName = updatedImageName;
        }

        #region Validate Id From DB

        var objectFromDatabase = await _articleCategoryRepository.GetByIdAsync(model.Id.Value);
        if (objectFromDatabase is null) return Result.Failure(ErrorMessages.NotFoundErorr);

        #endregion

        #region Validate Title And Slug

        var isModelValid = await _articleCategoryRepository.IsValidAsync(nameof(model.Title), model.Title!, objectFromDatabase.Id);
        if (!isModelValid) return Result.Failure(ErrorMessages.TitleExistError);

        isModelValid = await _articleCategoryRepository.IsValidAsync(nameof(model.Slug), model.Slug!, objectFromDatabase.Id);
        if (!isModelValid) return Result.Failure(ErrorMessages.SlugExistError);

        #endregion

        #region Map And Update

        objectFromDatabase = objectFromDatabase.MapFrom(model);

        _articleCategoryRepository.Update(objectFromDatabase);
        await _articleCategoryRepository.SaveAsync();

        #endregion

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
    public async Task<Result> RecoverArticleCategoryAsync(int id)
    {
        var objectToRecover = await _articleCategoryRepository.GetByIdAsync(id);
        if (objectToRecover is null) return Result.Failure(ErrorMessages.NotFoundErorr);

        objectToRecover.IsDelete = false;

        _articleCategoryRepository.Update(objectToRecover);
        await _articleCategoryRepository.SaveAsync();

        return Result.Success(SuccessMessages.SuccessfullyDone);
    }
    #endregion
}