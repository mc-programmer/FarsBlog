using Serilog;

namespace FarsBlog.Web.Configurations;

public static class LogsConfiger
{
    public static void ConfigeSerilog(WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();

        builder.Host.UseSerilog();
    }
}
