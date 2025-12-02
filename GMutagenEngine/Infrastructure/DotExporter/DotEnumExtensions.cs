namespace GMutagenEngine.Infrastructure.DotExporter
{
    public static class DotEnumExtensions
    {
        public static string ToAttributeKey(this Enum value) => value.ToString().ToLower();
    }
}