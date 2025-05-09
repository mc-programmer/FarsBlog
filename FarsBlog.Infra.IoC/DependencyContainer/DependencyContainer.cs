﻿using FarsBlog.Application.MappingProfiles.Article;
using FarsBlog.Application.Services.Implementation.Article;
using FarsBlog.Application.Services.Interfaces.Article;
using FarsBlog.Domain.Interfaces.Article;
using FarsBlog.Infra.Data.Context;
using FarsBlog.Infra.Data.Repositories.Article;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FarsBlog.Infra.IoC.DependencyContainer;

public static class DependencyContainer
{
    public static void RegisterDependencies(this IServiceCollection services, string connectionString)
    {
        #region Database

        services.AddDbContext<FarsBlogDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        #endregion

        #region Auto Mapper

        services.AddAutoMapper(typeof(ArticleProfile));

        #endregion

        #region Repositories

        #region Article

        services.AddScoped<IArticleCategoryRepository, ArticleCategoryRepository>();
        services.AddScoped<IArticleRepository, ArticleRepository>();

        #endregion

        #endregion

        #region Services

        #region Article

        services.AddScoped<IArticleCategoryService, ArticleCategoryService>();
        services.AddScoped<IArticleService, ArticleService>();

        #endregion

        #endregion
    }
}