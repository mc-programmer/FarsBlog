using Serilog;

namespace FarsBlog.Web.Configurations;

public static class LogsConfiger
{
    public static void ConfigeSerilog(IHostBuilder hostBuilder)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("logs/serilog-logs.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        hostBuilder.UseSerilog();
    }
}
