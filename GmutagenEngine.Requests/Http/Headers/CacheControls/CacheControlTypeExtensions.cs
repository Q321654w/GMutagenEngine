namespace GmutagenEngine.Requests.Http.Headers.CacheControls
{
    public static class CacheControlTypeExtensions
    {
        public static string AsString(this CacheControlType type)
            => type switch
            {
                CacheControlType.NoCache => "no-cache",
                CacheControlType.NoStore => "no-store",

                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
    }
}