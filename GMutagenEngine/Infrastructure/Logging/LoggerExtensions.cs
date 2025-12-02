namespace GMutagenEngine.Infrastructure.Logging
{
    public static class LoggerExtensions
    {
        public static void LogDebug(this ILogger logger, string data)
            => logger.Log(data, (int)LogLevel.Debug);
        public static void LogInfo(this ILogger logger, string data)
            => logger.Log(data, (int)LogLevel.Info);  
        
        public static void LogNotice(this ILogger logger, string data)
            => logger.Log(data, (int)LogLevel.Notice);
    
        public static void LogWarning(this ILogger logger, string data)
            => logger.Log(data, (int)LogLevel.Warning);   
        
        public static void LogError(this ILogger logger, string data)
            => logger.Log(data, (int)LogLevel.Error);
        
        public static void LogCritical(this ILogger logger, string data)
            => logger.Log(data, (int)LogLevel.Critical);   
    }
}