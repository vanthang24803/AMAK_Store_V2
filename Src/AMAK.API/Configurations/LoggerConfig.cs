using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;

namespace AMAK.API.Configurations {
    public static class LoggerConfig {
        public static void AddLoggerConfig(this IHostBuilder builder) {

            builder.UseSerilog((_, config) => {
                config.MinimumLevel.Information()
                      .MinimumLevel.Override("System", LogEventLevel.Warning)
                      .WriteTo.Console()
                      .WriteTo.File(
                          path: Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs/log-.txt"),
                          rollingInterval: RollingInterval.Day,
                          retainedFileCountLimit: 7,
                          shared: true,
                          formatter: new JsonFormatter(),
                          restrictedToMinimumLevel: LogEventLevel.Warning)
                      .MinimumLevel.Override("Microsoft", LogEventLevel.Information);
            });
        }
    }
}
