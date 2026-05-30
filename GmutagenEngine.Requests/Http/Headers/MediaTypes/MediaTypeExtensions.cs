namespace GmutagenEngine.Requests.Http.Headers.MediaTypes
{
    public static class MediaTypeExtensions
    {
        public static string AsString(this MediaType type) => type switch
        {
            MediaType.Json => "application/json",
            MediaType.Xml => "application/xml",
            MediaType.PlainText => "text/plain",
            MediaType.Html => "text/html",
            MediaType.FormUrlEncoded => "application/x-www-form-urlencoded",
            MediaType.MultipartFormData => "multipart/form-data",
            MediaType.JavaScript => "application/javascript",
            MediaType.Css => "text/css",
            MediaType.Csv => "text/csv",
            MediaType.Pdf => "application/pdf",
            MediaType.Jpeg => "image/jpeg",
            MediaType.Png => "image/png",
            MediaType.Gif => "image/gif",
            MediaType.Svg => "image/svg+xml",
            MediaType.Webp => "image/webp",
            MediaType.OctetStream => "application/octet-stream",

            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}