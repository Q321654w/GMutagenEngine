using GMutagenEngine.Handlers.Actions.Interfaces;

namespace GMutagenEngine.Logging.Handlers;

public sealed class ConsoleLogHandler : ISyncActionHandler<string>
{
    public void Handle(string data)
    {
        Console.WriteLine(data);
    }
}
