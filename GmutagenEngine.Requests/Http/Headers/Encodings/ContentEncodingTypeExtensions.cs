namespace GmutagenEngine.Requests.Http.Headers.Encodings
{
    public static class ContentEncodingTypeExtensions
    {
        public static string AsString(this ContentEncodingType encodingType) => encodingType switch
        {
            ContentEncodingType.Gzip => "gzip",
            ContentEncodingType.Deflate => "deflate",
            ContentEncodingType.Br => "br",
            ContentEncodingType.Compress => "compress",
            ContentEncodingType.Identity => "identity",
            ContentEncodingType.Any => "*",
            _ => throw new ArgumentOutOfRangeException(nameof(encodingType), encodingType, null)
        };
    }
}