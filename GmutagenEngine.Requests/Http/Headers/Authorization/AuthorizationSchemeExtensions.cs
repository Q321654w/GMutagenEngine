namespace GmutagenEngine.Requests.Http.Headers.Authorization
{
    public static class AuthorizationSchemeExtensions
    {
        public static string AsString(this AuthorizationScheme type)
            => type switch
            {
                AuthorizationScheme.Bearer => "Bearer",
                AuthorizationScheme.Basic => "Basic",

                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
    }
}