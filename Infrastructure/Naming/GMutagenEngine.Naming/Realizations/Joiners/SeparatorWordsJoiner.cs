using Constants.Literals;
using GMutagenEngine.Naming.Interfaces;

namespace GMutagenEngine.Naming.Realizations.Joiners;

public class SeparatorWordsJoiner(string separator = Literals.Whitespace.SPACE_STRING) : IWordsJoiner
{
    public string Handle(IEnumerable<string> words)
        => JoinStatic(words, separator);

    public static string JoinStatic(IEnumerable<string> words, string separator = Literals.Whitespace.SPACE_STRING)
    {
        return string.Join(separator, words);
    }
}
