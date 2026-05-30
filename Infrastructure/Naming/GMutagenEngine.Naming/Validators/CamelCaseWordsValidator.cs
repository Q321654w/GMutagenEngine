namespace GMutagenEngine.Naming.Validators;

public class CamelCaseWordsValidator
{
    public bool IsValid(IEnumerable<string> words)
        => IsValidStatic(words);

    public static bool IsValidStatic(IEnumerable<string> words)
    {
        var wordArray = words.ToArray();
        if (!wordArray.Any())
            return false;

        if (!LowerFirstWordValidator.IsValidStatic(wordArray.First()))
            return false;

        return wordArray.Skip(1).All(UpperFirstWordValidator.IsValidStatic);
    }
}
