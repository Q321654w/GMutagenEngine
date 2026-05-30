namespace GMutagenEngine.Naming.Validators;

public static class UpperCaseWordValidator
{
    public static bool IsValidStatic(string word)
    {
        if (string.IsNullOrWhiteSpace(word))
            return false;

        return string.Equals(word, word.ToUpper(), StringComparison.Ordinal);
    }
}
