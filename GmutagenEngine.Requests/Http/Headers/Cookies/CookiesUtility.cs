using System.Text;
using GmutagenEngine.Requests.Http.Constants;

namespace GmutagenEngine.Requests.Http.Headers.Cookies
{
    public static class CookiesUtility
    {
        public static string GetCookieHeader(Dictionary<string, string> cookies)
        {
            if (cookies.Count == 0)
                return string.Empty;

            var sb = new StringBuilder();
            foreach (var pair in cookies)
            {
                sb.Append(pair.Key).Append(HttpFormatSymbols.EQUALS).Append(pair.Value)
                    .Append(HttpFormatSymbols.SEMICOLON + HttpFormatSymbols.SPACE);
            }

            return sb.ToString().TrimEnd(HttpFormatSymbols.SPACE_CHAR, HttpFormatSymbols.SEMICOLON_CHAR);
        }
    }
}