using FarsBlog.Domain.Models.Article;
using Microsoft.EntityFrameworkCore;

namespace FarsBlog.Infra.Data.Context;

public class FarsBlogDbContext : DbContext
{
    public FarsBlogDbContext(DbContextOptions<FarsBlogDbContext> options) : base(options) { }

    #region Article

    public DbSet<Article> Articles { get; set; }
    public DbSet<ArticleCategoryMapping> ArticleCategoryMappings { get; set; }
    public DbSet<ArticleCategory> ArticleCategories { get; set; }

    #endregion

    #region FluentApi

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region MappingTables

        modelBuilder.Entity<ArticleCategoryMapping>()
            .HasKey(a => new { a.ArticleId, a.CategoryId });

        #endregion

        base.OnModelCreating(modelBuilder);
    }

    #endregion
}