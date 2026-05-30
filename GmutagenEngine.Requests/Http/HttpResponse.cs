using System.Text;

namespace GmutagenEngine.Requests.Http
{
    public class HttpResponse : IHttpResponse
    {
        public int StatusCode { get; }
        public string? Body { get; }
        public Dictionary<string, string> Headers { get; }

        public HttpResponse(long statusCode, string body, Dictionary<string, string> headers)
        {
            StatusCode = (int)statusCode;
            Body = body;
            Headers = headers;
        }
    
        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var h in Headers)
                sb.AppendLine($"{h.Key}: {h.Value}");

            if (!string.IsNullOrEmpty(Body))
                sb.AppendLine($"\nBody:\n{Body}");

            return sb.ToString();
        }
    }
}