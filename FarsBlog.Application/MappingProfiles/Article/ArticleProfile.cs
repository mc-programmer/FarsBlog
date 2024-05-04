using AutoMapper;
using FarsBlog.Domain.DTOs.ViewModels.Article;

namespace FarsBlog.Application.MappingProfiles.Article;

public class ArticleProfile : Profile
{
    public ArticleProfile()
    {
        CreateMap<AdminSideArticleDetailsForFilterViewModel, Domain.Models.Article.Article>()
            .ReverseMap();

        CreateMap<AdminSideCreateArticleViewModel, Domain.Models.Article.Article>()
            .ReverseMap();

        CreateMap<Domain.Models.Article.Article, AdminSideUpdateArticleViewModel > ();
        
        //CreateMap<AdminSideUpdateArticleViewModel, Domain.Models.Article.Article> ()
        //    .ForMember(dest =>dest.Id,option => option.Ignore());
    }
}