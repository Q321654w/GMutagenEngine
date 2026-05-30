using GMutagenEngine.Naming.Interfaces;

namespace GMutagenEngine.Naming.Realizations.Formatters;

public sealed class UpperFirstWordFormatter : IWordFormatter
{
    public string Handle(string word)
        => Format(word);

    public static string Format(string word)
    {
        if (string.IsNullOrEmpty(word))
            return string.Empty;

        return word.Length > 1
            ? char.ToUpper(word[0]) + word.Substring(1).ToLower()
            : word.ToUpper();
    }
}
