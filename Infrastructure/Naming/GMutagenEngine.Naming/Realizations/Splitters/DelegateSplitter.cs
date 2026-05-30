using GMutagenEngine.Naming.Interfaces;

namespace GMutagenEngine.Naming.Realizations.Splitters;

public class DelegateSplitter(Func<string, string[]> splitFunction) : IWordsSplitter
{
    public string[] Handle(string data)
        => SplitStatic(data, splitFunction);

    public static string[] SplitStatic(string data, Func<string, string[]> splitFunction)
    {
        if (string.IsNullOrEmpty(data))
            return Array.Empty<string>();

        return splitFunction(data);
    }
}
