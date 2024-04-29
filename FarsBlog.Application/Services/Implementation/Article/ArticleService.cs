using FarsBlog.Application.Mappers.Article;
using FarsBlog.Application.Services.Interfaces.Article;
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

    #endregion

    #region Constructor

    public ArticleService(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    #endregion

    public async Task<FilterArticleViewModel> AdminFilterAsync(FilterArticleViewModel filter)
    {
        var filterCondition = Filter.GenerateConditions<Domain.Models.Article.Article>();

        await _articleRepository.FilterAsync(filter, filterCondition, ArticleMapper.MapArticleDetailsViewModel,
            orderByDesc:a => a.CreateDateOnUtc);

        return filter;
    }

    public Task<Result> CreateAsync()
    {
        throw new NotImplementedException();
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