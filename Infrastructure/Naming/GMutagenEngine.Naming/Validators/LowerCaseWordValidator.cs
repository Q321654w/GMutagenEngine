namespace GMutagenEngine.Naming.Validators;

public static class LowerCaseWordValidator
{
    public static bool IsValidStatic(string word)
    {
        if (string.IsNullOrWhiteSpace(word))
            return false;

        return string.Equals(word, word.ToLower(), StringComparison.Ordinal);
    }
}
