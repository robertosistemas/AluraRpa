using Serilog;

namespace AluraRpa.Infra.Configurations
{
    public static class ConfigureLog
    {
        public static void InitializeLog()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
    }
}
