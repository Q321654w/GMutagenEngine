using GMutagenEngine.Naming.Interfaces;

namespace GMutagenEngine.Naming.Realizations.Transformers;

public class CompositeStringTransformer(IWordsSplitter splitter, IWordsJoiner joiner) : IStringFormatter
{
    public string Handle(string data)
        => HandleStatic(data, splitter, joiner);

    public static string HandleStatic(string data, IWordsSplitter splitter, IWordsJoiner joiner)
    {
        var words = splitter.Handle(data) ?? Array.Empty<string>();
        return joiner.Handle(words) ?? string.Empty;
    }
}