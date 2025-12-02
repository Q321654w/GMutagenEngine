namespace GMutagenEngine.Infrastructure.Logging
{
    public static class LoggerPresets
    {
        public static ILogger<T> CreateConsoleLogger<T>()
        {
            return new Logger<T>();
        }
    }
}