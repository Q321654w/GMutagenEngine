namespace GMutagenEngine.Naming.Validators;

public static class LowerFirstWordValidator
{
    public static bool IsValidStatic(string word)
    {
        if (string.IsNullOrWhiteSpace(word))
            return false;

        return char.IsLower(word[0]);
    }
}
