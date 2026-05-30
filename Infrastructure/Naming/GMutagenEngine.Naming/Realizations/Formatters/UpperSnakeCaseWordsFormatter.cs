using GMutagenEngine.Naming.Interfaces;

namespace GMutagenEngine.Naming.Realizations.Formatters;

public class UpperSnakeCaseWordsFormatter : IWordsFormatter
{
    public IEnumerable<string> Handle(IEnumerable<string> words)
        => Format(words);

    public static IEnumerable<string> Format(IEnumerable<string> words)
    {
        return words.Select(UpperWordFormatter.Format);
    }
}
