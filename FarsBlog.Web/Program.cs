using FarsBlog.Application.Statics;
using FarsBlog.Infra.IoC.DependencyContainer;
using FarsBlog.Web.Configurations;

var builder = WebApplication.CreateBuilder(args);

#region Services

LogsConfiger.ConfigSerilog(builder);

builder.Services.AddControllersWithViews();

DependencyContainer.RegisterDependencies(
   services: builder.Services,
   connectionString: builder.Configuration.GetConnectionString("CodeJooyanTVConnectionString")!);

builder.Configuration.GetSection("SiteTools").Get<SiteTools>();

#endregion

#region Pipline

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
  name: "areas",
  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

#endregion