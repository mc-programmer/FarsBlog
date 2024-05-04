using AutoMapper;
using FarsBlog.Domain.DTOs.ViewModels.Article.Category;
using FarsBlog.Domain.Models.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarsBlog.Application.MappingProfiles.Article;

public class ArticleCategoryProfile:Profile
{
    public ArticleCategoryProfile()
    {
        CreateMap<AdminSideCreateArticleCategoryViewModel, ArticleCategory>()
            .ReverseMap();

        CreateMap<ArticleCategoryDetailsViewModel,ArticleCategory>()
            .ReverseMap();
    }
}
