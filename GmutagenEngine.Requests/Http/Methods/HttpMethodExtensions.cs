namespace GmutagenEngine.Requests.Http.Methods
{
    public static class HttpMethodExtensions
    {
        public static string AsString(this HttpMethod method) => method switch
        {
            HttpMethod.Get => "GET",
            HttpMethod.Post => "POST",
            HttpMethod.Put => "PUT",
            HttpMethod.Delete => "DELETE",
            HttpMethod.Patch => "PATCH",
            HttpMethod.Head => "HEAD",
            HttpMethod.Options => "OPTIONS",

            _ => throw new ArgumentOutOfRangeException(nameof(method), method, null)
        };
    }
}