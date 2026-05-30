namespace GMutagenEngine.Naming.Validators;

public class TitleCaseWordsValidator
{
    public bool IsValid(IEnumerable<string> words)
        => IsValidStatic(words);

    public static bool IsValidStatic(IEnumerable<string> words)
    {
        return words.All(UpperFirstWordValidator.IsValidStatic);
    }
}
