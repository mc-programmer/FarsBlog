using Microsoft.EntityFrameworkCore;

namespace FarsBlog.Infra.Data.Context;

public class FarsBlogDbContext:DbContext
{
    public FarsBlogDbContext(DbContextOptions<FarsBlogDbContext> options) : base(options) { }
}