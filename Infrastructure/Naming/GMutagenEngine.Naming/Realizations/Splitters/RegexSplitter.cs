using System.Text.RegularExpressions;
using GMutagenEngine.Naming.Interfaces;

namespace GMutagenEngine.Naming.Realizations.Splitters;

public class RegexSplitter(Regex regex) : IWordsSplitter
{
    public RegexSplitter(string pattern = @"\W+") : this(new Regex(pattern, RegexOptions.Compiled))
    {
    }

    public string[] Handle(string data)
        => SplitStatic(data, regex);

    public static string[] SplitStatic(string data, Regex regex)
    {
        if (string.IsNullOrEmpty(data))
            return Array.Empty<string>();

        var splitValues = regex.Split(data);

        var nonEmptyCount = 0;
        for (var i = 0; i < splitValues.Length; i++)
        {
            if (!string.IsNullOrEmpty(splitValues[i]))
                nonEmptyCount++;
        }

        if (nonEmptyCount == 0)
            return Array.Empty<string>();

        if (nonEmptyCount == splitValues.Length)
            return splitValues;

        var result = new string[nonEmptyCount];
        var resultIndex = 0;

        for (var i = 0; i < splitValues.Length; i++)
        {
            var splitValue = splitValues[i];
            if (string.IsNullOrEmpty(splitValue))
                continue;

            result[resultIndex] = splitValue;
            resultIndex++;
        }

        return result;
    }
}
