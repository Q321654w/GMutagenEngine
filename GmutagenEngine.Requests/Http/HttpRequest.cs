using System.Text;
using GmutagenEngine.Requests.Http.Headers.Cookies;

namespace GmutagenEngine.Requests.Http;

public class HttpRequest : IHttpRequest
{
    public string Url { get; set; } = string.Empty;
    public HttpMethod Method { get; set; } = HttpMethod.Get;

    public Dictionary<string, string> Headers { get; set; } = new();
    public Dictionary<string, string> Cookies { get; set; } = new();

    public string? Body { get; set; }

    public HttpRequest()
    {
    }

    public HttpRequest(string url, HttpMethod method = HttpMethod.Get)
    {
        Url = url;
        Method = method;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{Method} {Url}");

        foreach (var h in Headers)
            sb.AppendLine($"{h.Key}: {h.Value}");

        if (Cookies.Count > 0)
            sb.AppendLine($"Cookie: {CookiesUtility.GetCookieHeader(Cookies)}");

        if (!string.IsNullOrEmpty(Body))
            sb.AppendLine($"\nBody:\n{Body}");

        return sb.ToString();
    }
}