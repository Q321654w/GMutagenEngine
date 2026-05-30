namespace GMutagenEngine.Naming.Validators;

public static class UpperFirstWordValidator
{
    public static bool IsValidStatic(string word)
    {
        if (string.IsNullOrWhiteSpace(word))
            return false;

        return char.IsUpper(word[0]);
    }
}
