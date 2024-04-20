using Serilog;

namespace FarsBlog.Web.Configurations;

public static class LogsConfiger
{
    public static void ConfigSerilog(WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();

        Serilog.Debugging.SelfLog.Enable(msg=> Console.WriteLine(msg));

        builder.Host.UseSerilog();
    }
}
