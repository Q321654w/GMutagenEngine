using GMutagenEngine.Naming.Interfaces;

namespace GMutagenEngine.Naming.Realizations.Formatters;

public class CamelCaseWordsFormatter : IWordsFormatter
{
    public IEnumerable<string> Handle(IEnumerable<string> words)
        => Format(words);

    public static IEnumerable<string> Format(IEnumerable<string> words)
    {
        using var enumerator = words.GetEnumerator();
        if (!enumerator.MoveNext())
            return Array.Empty<string>();

        var estimatedCount = words is ICollection<string> collection ? collection.Count : 4;
        var result = new List<string>(Math.Max(estimatedCount, 1))
        {
            LowerFirstWordFormatter.Format(enumerator.Current)
        };

        while (enumerator.MoveNext())
        {
            var word = enumerator.Current;
            result.Add(UpperFirstWordFormatter.Format(word));
        }

        return result;
    }
}
