using AutoMapper;
using FarsBlog.Application.Extentions;
using FarsBlog.Application.Services.Interfaces.Article;
using FarsBlog.Application.Statics;
using FarsBlog.Domain.DTOs.ViewModels.Article;
using FarsBlog.Domain.DTOs.ViewModels.Article.Category;
using FarsBlog.Domain.DTOs.ViewModels.Common.Filter;
using FarsBlog.Domain.Interfaces.Article;
using FarsBlog.Domain.Models.Article;
using FarsBlog.Domain.Shared;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection.Metadata.Ecma335;

namespace FarsBlog.Application.Services.Implementation.Article;

public class ArticleService : IArticleService
{
    #region Fields

    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;

    #endregion

    #region Constructor

    public ArticleService(IArticleRepository articleRepository,
        IMapper mapper)
    {
        _articleRepository = articleRepository;
        _mapper = mapper;
    }

    #endregion

    #region Common

    public async Task<Result<AdminSideUpdateArticleViewModel>> GetArticleByIdForEditByAdminAsync(int id)
    {
        if (id <= 0) return Result.Failure<AdminSideUpdateArticleViewModel>(ErrorMessages.NotFoundErorr);

        var articleFromDatabase = await _articleRepository.GetByIdAsync(id);
        if (articleFromDatabase is null) return Result.Failure<AdminSideUpdateArticleViewModel>(ErrorMessages.NotFoundErorr);

        return _mapper.Map<AdminSideUpdateArticleViewModel>(articleFromDatabase);
    }

    #endregion

    #region Admin

    public async Task<FilterArticleViewModel> AdminFilterAsync(FilterArticleViewModel filter)
    {
        var filterCondition = Filter.GenerateConditions<Domain.Models.Article.Article>();
        await _articleRepository.FilterAsync(filter, filterCondition,
          mapping: article => _mapper.Map<AdminSideArticleDetailsForFilterViewModel>(article),
          orderByDesc: a => a.CreateDateOnUtc);

        return filter;
    }
    public async Task<Result> UpdateAsync(AdminSideUpdateArticleViewModel model)
    {
        #region Validate Model

        if (model is null) return Result.Failure(ErrorMessages.NullValue);

        if (model.Id is null) return Result.Failure(ErrorMessages.NullValue);

        #endregion

        var articleFromDatabase = await _articleRepository.GetByIdAsync(model.Id.Value);
        if (articleFromDatabase is null) return Result.Failure(ErrorMessages.NotFoundErorr);

        #region Validate Title And Slug

        bool isValid = await _articleRepository.IsValidAsync(nameof(Domain.Models.Article.Article.Title),model.Title,articleFromDatabase.Id) ;
        if (isValid is false) return Result.Failure(ErrorMessages.TitleExistError);

        isValid = await _articleRepository.IsValidAsync(nameof(Domain.Models.Article.Article.Slug), model.Slug,articleFromDatabase.Id);
        if (isValid is false) return Result.Failure(ErrorMessages.SlugExistError);

        #endregion

        #region Update Image if is changed

        if (model.ImageFile is not null)
        {
            string imageName = Guid.NewGuid() + Path.GetExtension(model.ImageFile.FileName);
            if (SiteTools.ArticleImagePath is null) return Result.Failure(ErrorMessages.OperationFailedError);
                        
            var result = model.ImageFile.AddImageToServer(imageName, SiteTools.ArticleImagePath, 400, 280, SiteTools.ArticleImageThumbPath, model.ImageName ?? "");
            if (result.IsFailure) return Result.Failure(result.Message ?? ErrorMessages.NullValue);

            articleFromDatabase.ImageName = imageName;
        }

        #endregion

        articleFromDatabase.Title = model.Title;
        articleFromDatabase.Text = model.Text;
        articleFromDatabase.ShortDescription = model.ShortDescription;
        articleFromDatabase.ImageAlt = model.ImageAlt;

        _articleRepository.Update(articleFromDatabase);
        await _articleRepository.SaveAsync();

        return Result.Success();
    }
    public async Task<Result> CreateAsync(AdminSideCreateArticleViewModel model)
    {
        #region Validate Model

        if (model is null) return Result.Failure(ErrorMessages.NullValue);

        #endregion

        #region Upload Image

        if (model.ImageFile is not null)
        {
            model.ImageName = Guid.NewGuid() + Path.GetExtension(model.ImageFile.FileName);

            if (SiteTools.ArticleImagePath is null) return Result.Failure(ErrorMessages.OperationFailedError);

            var result = model.ImageFile.AddImageToServer(model.ImageName, SiteTools.ArticleImagePath, 400, 280, SiteTools.ArticleImageThumbPath);

            if (result.IsFailure) return Result.Failure(result.Message ?? ErrorMessages.NullValue);
        }

        #endregion

        #region Validate Slug and Title

        bool isValid = await _articleRepository.IsValidAsync(nameof(Domain.Models.Article.Article.Title), model.Title.Trim());
        if (isValid is false) return Result.Failure(ErrorMessages.TitleExistError);

        isValid = await _articleRepository.IsValidAsync(nameof(Domain.Models.Article.Article.Slug), model.Slug.Trim());
        if (isValid is false) return Result.Failure(ErrorMessages.SlugExistError);

        #endregion

        #region Map and insert to db

        var article = _mapper.Map<Domain.Models.Article.Article>(model);

        await _articleRepository.InsertAsync(article);
        await _articleRepository.SaveAsync();

        #endregion

        return Result.Success();
    }
    public async Task<Result<bool>> DeleteAsync(int id)
    {
        if (id <= 0) return false;

        var articleToDelete = await _articleRepository.GetByIdAsync(id);
        if (articleToDelete is null) return false;

        articleToDelete.IsDelete = true;

        _articleRepository.Update(articleToDelete);
        await _articleRepository.SaveAsync();

        return true;
    }
    public Task<Result<bool>> RecoverAsync(int id)
    {
        throw new NotImplementedException();
    }

    #endregion
}