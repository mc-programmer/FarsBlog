using FarsBlog.Application.Mappers.Article;
using FarsBlog.Application.Services.Interfaces.Article;
using FarsBlog.Domain.DTOs.ViewModels.Article.Category;
using FarsBlog.Domain.DTOs.ViewModels.Common.Filter;
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

        var filterConditions = Filter.GenerateConditions<ArticleCategory>();

        if (!string.IsNullOrEmpty(filter.Title))
            filterConditions.Add(articleCategory => EF.Functions.Like(articleCategory.Title,$"%{filter.Title.Trim()}%"));

        await _articleCategoryRepository.FilterAsync(filter,filterConditions,ArticleCategoryMapper.MapArticleCategoryDetailsViewModel);

        return filter;
    }

    public async Task<Result> CreateArticleCategoryAsync(AdminSideUpsertArticleCategoryViewModel model)
    {
        if (model is null) return Result.Failure(ErrorMessages.NullValue);

        var articleCategory = new ArticleCategory().MapFrom(model);

        await _articleCategoryRepository.InsertAsync(articleCategory);
        await _articleCategoryRepository.SaveAsync();

        return Result.Success(SuccessMessages.SuccessfullyDone);
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
