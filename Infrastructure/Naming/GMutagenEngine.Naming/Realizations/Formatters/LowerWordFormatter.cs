using GMutagenEngine.Naming.Interfaces;

namespace GMutagenEngine.Naming.Realizations.Formatters;

public sealed class LowerWordFormatter : IWordFormatter
{
    public string Handle(string word)
        => Format(word);

    public static string Format(string word)
    {
        return word?.ToLower() ?? string.Empty;
    }
}
