using GMutagenEngine.Naming.Interfaces;

namespace GMutagenEngine.Naming.Realizations.Formatters;

public sealed class UpperWordFormatter : IWordFormatter
{
    public string Handle(string word)
        => Format(word);

    public static string Format(string word)
    {
        return word?.ToUpper() ?? string.Empty;
    }
}
