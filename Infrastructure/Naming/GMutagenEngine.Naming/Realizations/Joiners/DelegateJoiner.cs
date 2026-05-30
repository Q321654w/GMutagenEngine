using GMutagenEngine.Naming.Interfaces;

namespace GMutagenEngine.Naming.Realizations.Joiners;

public class DelegateJoiner(Func<IEnumerable<string>, string> joinFunction) : IWordsJoiner
{
    public string Handle(IEnumerable<string> words)
        => JoinStatic(words, joinFunction);

    public static string JoinStatic(IEnumerable<string> words, Func<IEnumerable<string>, string> joinFunction)
    {
        return joinFunction(words);
    }
}
