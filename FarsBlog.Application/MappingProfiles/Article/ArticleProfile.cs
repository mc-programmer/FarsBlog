using AutoMapper;
using FarsBlog.Domain.DTOs.ViewModels.Article;

namespace FarsBlog.Application.MappingProfiles.Article;

public class ArticleProfile : Profile
{
    public ArticleProfile()
    {
        CreateMap<Domain.Models.Article.Article, AdminSideCreateArticleViewModel>();
        CreateMap<AdminSideCreateArticleViewModel, Domain.Models.Article.Article>();
    }
}