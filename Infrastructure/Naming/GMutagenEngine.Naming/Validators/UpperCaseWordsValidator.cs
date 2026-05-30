namespace GMutagenEngine.Naming.Validators;

public class UpperCaseWordsValidator
{
    public bool IsValid(IEnumerable<string> words)
        => IsValidStatic(words);

    public static bool IsValidStatic(IEnumerable<string> words)
    {
        return words.All(UpperCaseWordValidator.IsValidStatic);
    }
}
