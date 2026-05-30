namespace GMutagenEngine.Naming.Validators;

public class RelativeUrlValidator
{
    public bool IsValid(string data)
        => IsValidStatic(data);

    public static bool IsValidStatic(string data)
    {
        if (string.IsNullOrWhiteSpace(data))
            return false;

        return Uri.TryCreate(data, UriKind.Relative, out _);
    }
}
