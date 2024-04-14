using FarsBlog.Infra.Data.Context;
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
        
        #endregion

        #region User

        #endregion

        #endregion
    }
}