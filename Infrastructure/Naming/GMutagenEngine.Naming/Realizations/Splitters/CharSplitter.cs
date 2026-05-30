using Constants.Literals;
using GMutagenEngine.Naming.Interfaces;

namespace GMutagenEngine.Naming.Realizations.Splitters;

public class CharSplitter(StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries, params char[]? separators)
    : IWordsSplitter
{
    private readonly char[] _separators = separators ?? Delimiters.CHARS;

    public string[] Handle(string data)
        => SplitStatic(data, options, _separators);

    public static string[] SplitStatic(string data,
        StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries, params char[]? separators)
    {
        if (string.IsNullOrEmpty(data))
            return Array.Empty<string>();

        separators ??= Delimiters.CHARS;
        return data.Split(separators, options);
    }
}
