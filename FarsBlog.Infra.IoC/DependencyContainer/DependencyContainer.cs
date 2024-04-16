using FarsBlog.Application.Services.Implementation.Article;
using FarsBlog.Application.Services.Interfaces.Article;
using FarsBlog.Domain.Interfaces.Article;
using FarsBlog.Domain.Models.Article;
using FarsBlog.Infra.Data.Context;
using FarsBlog.Infra.Data.Repositories.Article;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FarsBlog.Infra.IoC.DependencyContainer;

public class DependencyContainer
{
    public static void RegisterDependencies(IServiceCollection services, string connectionString)
    {
        #region Database

        services.AddDbContext<FarsBlogDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        #endregion

        #region Repositories

        #region Article

        services.AddScoped<IArticleCategoryRepository, ArticleCategoryRepository>();

        #endregion

        #endregion

        #region Services

        #region Article

        services.AddScoped<IArticleCategoryService, ArticleCategoryService>();

        #endregion

        #endregion
    }
}