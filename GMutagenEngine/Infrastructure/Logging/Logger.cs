namespace GMutagenEngine.Infrastructure.Logging
{
    public class Logger<T> : ILogger<T>
    {
        public void Log(string data, int logLevel = 0)
        {
            var actualData = GetData(data);
            var color = GetColor((LogLevel)logLevel);

            var prevColor = Console.ForegroundColor;

            Console.ForegroundColor = color;
            Console.WriteLine(actualData);

            Console.ForegroundColor = prevColor;
        }

        private string GetData(string data)
        {
            return $"[{typeof(T).Name}][{DateTime.Now:HH:mm:ss}] {data}";
        }

        private static ConsoleColor GetColor(LogLevel level) =>
            level switch
            {
                LogLevel.Debug => ConsoleColor.DarkGray,
                LogLevel.Info => ConsoleColor.White,
                LogLevel.Notice => ConsoleColor.Cyan,
                LogLevel.Warning => ConsoleColor.Yellow,
                LogLevel.Error => ConsoleColor.Red,
                LogLevel.Critical => ConsoleColor.Magenta,
                _ => ConsoleColor.Gray
            };
    }
}