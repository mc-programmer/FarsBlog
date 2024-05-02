using AutoMapper;
using FarsBlog.Application.Extentions;
using FarsBlog.Application.Mappers.Article;
using FarsBlog.Application.Services.Interfaces.Article;
using FarsBlog.Application.Statics;
using FarsBlog.Domain.DTOs.ViewModels.Article;
using FarsBlog.Domain.DTOs.ViewModels.Common.Filter;
using FarsBlog.Domain.Interfaces.Article;
using FarsBlog.Domain.Models.Article;
using FarsBlog.Domain.Shared;

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

    public async Task<FilterArticleViewModel> AdminFilterAsync(FilterArticleViewModel filter)
    {
        var filterCondition = Filter.GenerateConditions<Domain.Models.Article.Article>();

        await _articleRepository.FilterAsync(filter, filterCondition, ArticleMapper.MapArticleDetailsViewModel,
            orderByDesc: a => a.CreateDateOnUtc);

        return filter;
    }

    public async Task<Result> CreateAsync(AdminSideCreateArticleViewModel model)
    {
        #region Validate Model

        if (model is null) return Result.Failure(ErrorMessages.NullValue);

        #endregion

        #region Upload Image

        if (model.ImageFile is not null)
        {
            model.ImageFileName = Guid.NewGuid() + Path.GetExtension(model.ImageFile.FileName);

            if (SiteTools.ArticleImagePath is null) return Result.Failure(ErrorMessages.OperationFailedError);

            var result = model.ImageFile.AddImageToServer(model.ImageFileName, SiteTools.ArticleImagePath, 400, 280, SiteTools.ArticleImageThumbPath);

            if (result.IsFailure) return Result.Failure(result.Message ?? ErrorMessages.NullValue);
        }

        #endregion

        #region Validate Slug and Title

        bool isValid = await _articleRepository.IsValidAsync(nameof(Domain.Models.Article.Article.Title), model.Title);
        if (isValid is false) return Result.Failure(ErrorMessages.TitleExistError);

        isValid = await _articleRepository.IsValidAsync(nameof(Domain.Models.Article.Article.Slug), model.Slug);
        if (isValid is false) return Result.Failure(ErrorMessages.SlugExistError);

        #endregion

        #region Map and insert to db

        var article = _mapper.Map<Domain.Models.Article.Article>(model);

        await _articleRepository.InsertAsync(article);
        await _articleRepository.SaveAsync();

        #endregion

        return Result.Success();
    }

    public Task<Result<bool>> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> RecoverAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Result> UpdateAsync()
    {
        throw new NotImplementedException();
    }
}