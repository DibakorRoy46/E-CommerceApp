
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Common.Logging.Extensions;

public static class LoggingServiceExtensions
{
    public static IHostBuilder UseSharedSerilog(this IHostBuilder builder)
    {
        builder.UseSerilog(LoggingConfiguration.ConfigureLogger);
        return builder;
    }
}