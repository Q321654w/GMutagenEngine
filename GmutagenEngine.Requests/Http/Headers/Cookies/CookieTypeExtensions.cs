namespace GmutagenEngine.Requests.Http.Headers.Cookies
{
    public static class CookieTypeExtensions
    {
        public static string AsString(this CookieType type) => type switch
        {
            CookieType.Session => "session_id",
            CookieType.CsrfToken => "XSRF-TOKEN",
            CookieType.AuthToken => "auth_token",
            CookieType.RefreshToken => "refresh_token",
            CookieType.UserPreferences => "prefs",
            CookieType.Locale => "locale",
            CookieType.Analytics => "_ga",

            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}