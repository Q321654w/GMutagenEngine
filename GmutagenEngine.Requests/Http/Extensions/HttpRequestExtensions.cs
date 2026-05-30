using System.Globalization;
using System.Text;
using System.Web;
using GmutagenEngine.Requests.Http.Constants;
using GmutagenEngine.Requests.Http.Headers;
using GmutagenEngine.Requests.Http.Headers.Authorization;
using GmutagenEngine.Requests.Http.Headers.CacheControls;
using GmutagenEngine.Requests.Http.Headers.Cookies;
using GmutagenEngine.Requests.Http.Headers.Encodings;
using GmutagenEngine.Requests.Http.Headers.MediaTypes;
using Newtonsoft.Json;

namespace GmutagenEngine.Requests.Http.Extensions;

public static class HttpRequestExtensions
{
    public static HttpRequest WithUrl(this HttpRequest httpRequest, string url)
    {
        httpRequest.Url = url;
        return httpRequest;
    }

    public static HttpRequest WithMethod(this HttpRequest httpRequest, HttpMethod method)
    {
        httpRequest.Method = method;
        return httpRequest;
    }

    public static HttpRequest WithHeader(this HttpRequest httpRequest, string name, string value)
    {
        httpRequest.Headers[name] = value;
        return httpRequest;
    }

    public static HttpRequest WithCookie(this HttpRequest httpRequest, string name, string value)
    {
        httpRequest.Cookies[name] = value;
        return httpRequest;
    }

    public static HttpRequest WithBody(this HttpRequest httpRequest, string body, MediaType? contentType = null)
    {
        httpRequest.Body = body;
        return contentType != null
            ? httpRequest.WithContentType(contentType.Value)
            : httpRequest;
    }

    public static HttpRequest WithQueryParam(this HttpRequest httpRequest, string name, string value)
    {
        var separator = httpRequest.Url.Contains(HttpFormatSymbols.QUESTION)
            ? HttpFormatSymbols.AMPERSAND
            : HttpFormatSymbols.QUESTION;

        httpRequest.Url += string.Format(HttpFormats.QUERY_PARAM_FORMAT,
            separator,
            Uri.EscapeDataString(name),
            Uri.EscapeDataString(value));
        return httpRequest;
    }

    public static HttpRequest WithHeader(this HttpRequest httpRequest, HttpHeaderType headerType, string value)
        => httpRequest.WithHeader(headerType.AsString(), value);

    public static HttpRequest WithHeaderFormat(this HttpRequest httpRequest, HttpHeaderType headerType, string format,
        params object[] args)
        => httpRequest.WithHeader(headerType, string.Format(format, args));

    public static HttpRequest WithAccept(this HttpRequest httpRequest, MediaType mediaType)
        => httpRequest.WithHeader(HttpHeaderType.Accept, mediaType.AsString());

    public static HttpRequest WithContentType(this HttpRequest httpRequest, string value)
        => httpRequest.WithHeader(HttpHeaderType.ContentType, value);

    public static HttpRequest WithContentType(this HttpRequest httpRequest, MediaType mediaType)
        => httpRequest.WithHeader(HttpHeaderType.ContentType, mediaType.AsString());

    public static HttpRequest WithJsonContentType(this HttpRequest httpRequest)
        => httpRequest.WithContentType(MediaType.Json);

    public static HttpRequest WithAuthorization(this HttpRequest httpRequest, AuthorizationScheme scheme,
        string credentials)
        => httpRequest.WithHeaderFormat(HttpHeaderType.Authorization, HttpFormats.BEARER_TOKEN_FORMAT,
            scheme.AsString(),
            credentials);

    public static HttpRequest WithBearerToken(this HttpRequest httpRequest, string token)
        => httpRequest.WithAuthorization(AuthorizationScheme.Bearer, token);

    public static HttpRequest WithBasicAuth(this HttpRequest httpRequest, string username, string password)
    {
        var credentials =
            Convert.ToBase64String(
                Encoding.UTF8.GetBytes(string.Format(HttpFormats.BASIC_AUTH_FORMAT, username, password)));
        return httpRequest.WithAuthorization(AuthorizationScheme.Basic, credentials);
    }

    public static HttpRequest WithUserAgent(this HttpRequest httpRequest, string name,
        string version = HttpDefaults.DEFAULT_USER_AGENT_VERSION)
        => httpRequest.WithHeaderFormat(HttpHeaderType.UserAgent, HttpFormats.USER_AGENT_FORMAT, name, version);

    public static HttpRequest WithDefaultUserAgent(this HttpRequest httpRequest)
        => httpRequest.WithUserAgent(HttpDefaults.DEFAULT_USER_AGENT_NAME);

    public static HttpRequest WithDefaultHeaders(this HttpRequest httpRequest)
        => httpRequest
            .WithAccept(MediaType.Json)
            .WithDefaultUserAgent();

    public static HttpRequest WithCookie(this HttpRequest httpRequest, CookieType cookieType, string value)
        => httpRequest.WithCookie(cookieType.AsString(), value);

    public static HttpRequest WithCookies(this HttpRequest httpRequest,
        IEnumerable<KeyValuePair<string, string>> cookies)
    {
        foreach (var cookie in cookies)
            httpRequest.WithCookie(cookie.Key, cookie.Value);

        return httpRequest;
    }

    public static HttpRequest WithCookieHeader(this HttpRequest httpRequest)
    {
        var cookies = CookiesUtility.GetCookieHeader(httpRequest.Cookies);
        return !string.IsNullOrEmpty(cookies)
            ? httpRequest.WithHeader(HttpHeaderType.Cookie, cookies)
            : httpRequest;
    }


    public static HttpRequest WithJsonBody(this HttpRequest httpRequest, object data,
        JsonSerializerSettings? settings = null)
    {
        var json = JsonConvert.SerializeObject(data, settings);
        return httpRequest.WithBody(json, MediaType.Json);
    }

    public static HttpRequest WithFormBody(this HttpRequest httpRequest,
        IEnumerable<KeyValuePair<string, string>> formData)
    {
        var body = string.Join(HttpFormatSymbols.AMPERSAND, formData.Select(kv =>
            string.Format(HttpFormats.FORM_DATA_FORMAT,
                Uri.EscapeDataString(kv.Key),
                Uri.EscapeDataString(kv.Value))));

        return httpRequest.WithBody(body, MediaType.FormUrlEncoded);
    }


    public static HttpRequest WithQueryParams(this HttpRequest httpRequest,
        IEnumerable<KeyValuePair<string, string>> queryParams)
    {
        foreach (var param in queryParams)
            httpRequest.WithQueryParam(param.Key, param.Value);
        return httpRequest;
    }

    public static HttpRequest WithJsonPayload(this HttpRequest httpRequest, object data,
        JsonSerializerSettings? settings = null)
        => httpRequest.WithJsonBody(data, settings).WithDefaultHeaders();

    public static HttpRequest WithFormPayload(this HttpRequest httpRequest,
        IEnumerable<KeyValuePair<string, string>> formData)
        => httpRequest.WithFormBody(formData).WithDefaultHeaders();


    public static HttpRequest WithHeaderIf(this HttpRequest httpRequest, bool condition, string name, string value)
        => condition ? httpRequest.WithHeader(name, value) : httpRequest;

    public static HttpRequest WithHeaderIf(this HttpRequest httpRequest, bool condition, HttpHeaderType headerType,
        string value)
        => condition ? httpRequest.WithHeader(headerType, value) : httpRequest;

    public static HttpRequest WithHeaderIf(this HttpRequest httpRequest, Func<bool> condition, string name,
        string value)
        => condition() ? httpRequest.WithHeader(name, value) : httpRequest;

    public static HttpRequest WithHeaderIf(this HttpRequest httpRequest, Func<bool> condition,
        HttpHeaderType headerType,
        string value)
        => condition() ? httpRequest.WithHeader(headerType, value) : httpRequest;

    public static HttpRequest RemoveHeader(this HttpRequest httpRequest, string name)
    {
        httpRequest.Headers.Remove(name);
        return httpRequest;
    }

    public static HttpRequest RemoveHeader(this HttpRequest httpRequest, HttpHeaderType headerType)
        => httpRequest.RemoveHeader(headerType.AsString());

    public static HttpRequest WithHeaders(this HttpRequest httpRequest,
        IEnumerable<KeyValuePair<string, string>> headers)
    {
        foreach (var header in headers)
            httpRequest.WithHeader(header.Key, header.Value);
        return httpRequest;
    }

    public static HttpRequest WithHeaders(this HttpRequest httpRequest,
        IEnumerable<KeyValuePair<HttpHeaderType, string>> headers)
    {
        foreach (var header in headers)
            httpRequest.WithHeader(header.Key, header.Value);
        return httpRequest;
    }

    public static HttpRequest WithCacheControl(this HttpRequest httpRequest, string value)
        => httpRequest.WithHeader(HttpHeaderType.CacheControl, value);

    public static HttpRequest WithCacheControl(this HttpRequest httpRequest, CacheControlType type)
        => httpRequest.WithHeader(HttpHeaderType.CacheControl, type.AsString());

    public static HttpRequest WithNoCache(this HttpRequest httpRequest)
        => httpRequest.WithCacheControl(CacheControlType.NoCache);

    public static HttpRequest WithNoStore(this HttpRequest httpRequest)
        => httpRequest.WithCacheControl(CacheControlType.NoStore);

    public static HttpRequest WithAcceptLanguage(this HttpRequest httpRequest, string language)
        => httpRequest.WithHeader(HttpHeaderType.AcceptLanguage, language);

    public static HttpRequest WithAcceptEncoding(this HttpRequest httpRequest, string encoding)
        => httpRequest.WithHeader(HttpHeaderType.AcceptEncoding, encoding);

    public static HttpRequest WithAcceptEncoding(this HttpRequest httpRequest, ContentEncodingType encodingType)
        => httpRequest.WithAcceptEncoding(encodingType.AsString());

    public static HttpRequest WithAcceptEncoding(this HttpRequest httpRequest,
        params ContentEncodingType[] encodings)
    {
        var encodingString = string.Join(HttpFormatSymbols.COMMA + HttpFormatSymbols.SPACE,
            encodings.Select(e => e.AsString()));
        return httpRequest.WithAcceptEncoding(encodingString);
    }

    public static HttpRequest WithAcceptEncoding(this HttpRequest httpRequest,
        IEnumerable<ContentEncodingType> encodings)
        => httpRequest.WithAcceptEncoding(encodings.ToArray());

    public static HttpRequest WithGzipAcceptEncoding(this HttpRequest httpRequest)
        => httpRequest.WithAcceptEncoding(ContentEncodingType.Gzip);

    public static HttpRequest WithDeflateAcceptEncoding(this HttpRequest httpRequest)
        => httpRequest.WithAcceptEncoding(ContentEncodingType.Deflate);

    public static HttpRequest WithBrotliAcceptEncoding(this HttpRequest httpRequest)
        => httpRequest.WithAcceptEncoding(ContentEncodingType.Br);

    public static HttpRequest WithCompressAcceptEncoding(this HttpRequest httpRequest)
        => httpRequest.WithAcceptEncoding(ContentEncodingType.Compress);

    public static HttpRequest WithCommonCompressions(this HttpRequest httpRequest)
        => httpRequest.WithAcceptEncoding(ContentEncodingType.Gzip, ContentEncodingType.Deflate,
            ContentEncodingType.Br);

    public static HttpRequest WithAllCompressions(this HttpRequest httpRequest)
        => httpRequest.WithAcceptEncoding(ContentEncodingType.Gzip, ContentEncodingType.Deflate,
            ContentEncodingType.Br, ContentEncodingType.Compress);

    public static HttpRequest WithContentEncoding(this HttpRequest httpRequest, string encoding)
        => httpRequest.WithHeader(HttpHeaderType.ContentEncoding, encoding);

    public static HttpRequest WithContentEncoding(this HttpRequest httpRequest, ContentEncodingType encodingType)
        => httpRequest.WithContentEncoding(encodingType.AsString());

    public static HttpRequest WithGzipContentEncoding(this HttpRequest httpRequest)
        => httpRequest.WithContentEncoding(ContentEncodingType.Gzip);

    public static HttpRequest WithDeflateContentEncoding(this HttpRequest httpRequest)
        => httpRequest.WithContentEncoding(ContentEncodingType.Deflate);


    public static HttpRequest WithAcceptEncodingQuality(this HttpRequest httpRequest,
        params (ContentEncodingType encoding, double quality)[] encodingsWithQuality)
    {
        var encodingString = string.Join(HttpFormatSymbols.COMMA + HttpFormatSymbols.SPACE,
            encodingsWithQuality.Select(x =>
                $"{x.encoding.AsString()};q={x.quality.ToString("0.0", CultureInfo.InvariantCulture)}"));

        return httpRequest.WithAcceptEncoding(encodingString);
    }

    public static bool SupportsEncoding(this HttpRequest httpRequest, ContentEncodingType encodingType)
    {
        if (httpRequest.Headers.TryGetValue(HttpHeaderType.AcceptEncoding.AsString(), out var acceptEncoding))
        {
            return acceptEncoding.Split(HttpFormatSymbols.COMMA_CHAR).Any(x =>
                x.Trim().StartsWith(encodingType.AsString(), StringComparison.OrdinalIgnoreCase));
        }

        return false;
    }

    public static HttpRequest WithReferer(this HttpRequest httpRequest, string referer)
        => httpRequest.WithHeader(HttpHeaderType.Referer, referer);

    public static HttpRequest WithOrigin(this HttpRequest httpRequest, string origin)
        => httpRequest.WithHeader(HttpHeaderType.Origin, origin);

    public static HttpRequest WithApiKey(this HttpRequest httpRequest, string apiKey)
        => httpRequest.WithHeader(HttpHeaderType.XApiKey, apiKey);

    public static HttpRequest WithApiKey(this HttpRequest httpRequest, string apiKey, string scheme)
        => httpRequest.WithHeader(HttpHeaderType.XApiKey, string.Format(HttpFormats.BASIC_AUTH_FORMAT, apiKey, scheme));

    public static HttpRequest WithOAuth(this HttpRequest httpRequest, string token)
        => httpRequest.WithBearerToken(token);

    public static HttpRequest WithCustomAuth(this HttpRequest httpRequest, string scheme, string token)
        => httpRequest.WithHeader(HttpHeaderType.Authorization,
            string.Format(HttpFormats.BASIC_AUTH_FORMAT, scheme, token));

    public static HttpRequest WithCookieIf(this HttpRequest httpRequest, bool condition, string name, string value)
        => condition ? httpRequest.WithCookie(name, value) : httpRequest;

    public static HttpRequest WithCookieIf(this HttpRequest httpRequest, bool condition, CookieType cookieType,
        string value)
        => condition ? httpRequest.WithCookie(cookieType, value) : httpRequest;

    public static HttpRequest RemoveCookie(this HttpRequest httpRequest, string name)
    {
        httpRequest.Cookies.Remove(name);
        return httpRequest;
    }

    public static HttpRequest RemoveCookie(this HttpRequest httpRequest, CookieType cookieType)
        => httpRequest.RemoveCookie(cookieType.AsString());

    public static HttpRequest WithSessionCookie(this HttpRequest httpRequest, string sessionId)
        => httpRequest.WithCookie(CookieType.Session, sessionId);

    public static HttpRequest WithCsrfToken(this HttpRequest httpRequest, string token)
        => httpRequest.WithCookie(CookieType.CsrfToken, token);


    public static HttpRequest WithBodyIf(this HttpRequest httpRequest, bool condition, string body,
        MediaType? contentType = null)
        => condition ? httpRequest.WithBody(body, contentType) : httpRequest;

    public static HttpRequest WithBodyIf(this HttpRequest httpRequest, Func<bool> condition, string body,
        MediaType? contentType = null)
        => condition() ? httpRequest.WithBody(body, contentType) : httpRequest;

    public static HttpRequest WithXmlBody(this HttpRequest httpRequest, string xml)
        => httpRequest.WithBody(xml, MediaType.Xml);

    public static HttpRequest WithTextBody(this HttpRequest httpRequest, string text)
        => httpRequest.WithBody(text, MediaType.PlainText);

    public static HttpRequest WithHtmlBody(this HttpRequest httpRequest, string html)
        => httpRequest.WithBody(html, MediaType.Html);

    public static HttpRequest WithQueryParamIf(this HttpRequest httpRequest, bool condition, string name,
        string value)
        => condition ? httpRequest.WithQueryParam(name, value) : httpRequest;

    public static HttpRequest WithQueryParamIf(this HttpRequest httpRequest, Func<bool> condition, string name,
        string value)
        => condition() ? httpRequest.WithQueryParam(name, value) : httpRequest;

    public static HttpRequest RemoveQueryParam(this HttpRequest httpRequest, string name)
    {
        var uri = new Uri(httpRequest.Url);
        var query = HttpUtility.ParseQueryString(uri.Query);
        query.Remove(name);

        var builder = new UriBuilder(uri)
        {
            Query = query.ToString()
        };

        httpRequest.Url = builder.ToString();
        return httpRequest;
    }


    public static HttpRequest AsGet(this HttpRequest httpRequest)
        => httpRequest.WithMethod(HttpMethod.Get);

    public static HttpRequest AsPost(this HttpRequest httpRequest)
        => httpRequest.WithMethod(HttpMethod.Post);

    public static HttpRequest AsPut(this HttpRequest httpRequest)
        => httpRequest.WithMethod(HttpMethod.Put);

    public static HttpRequest AsDelete(this HttpRequest httpRequest)
        => httpRequest.WithMethod(HttpMethod.Delete);

    public static HttpRequest AsPatch(this HttpRequest httpRequest)
        => httpRequest.WithMethod(HttpMethod.Patch);

    public static HttpRequest ForJsonApi(this HttpRequest httpRequest)
        => httpRequest
            .WithJsonContentType()
            .WithAccept(MediaType.Json)
            .WithHeader(HttpHeaderType.Accept, "application/json, text/plain, */*");

    public static HttpRequest ForXmlApi(this HttpRequest httpRequest)
        => httpRequest
            .WithContentType(MediaType.Xml)
            .WithAccept(MediaType.Xml);

    public static HttpRequest WithCompression(this HttpRequest httpRequest)
        => httpRequest
            .WithAcceptEncoding(string.Join(HttpFormatSymbols.COMMA + HttpFormatSymbols.SPACE,
                ContentEncodingType.Gzip.AsString(),
                ContentEncodingType.Deflate.AsString(),
                ContentEncodingType.Br.AsString()));

    public static HttpRequest WithCorsHeaders(this HttpRequest httpRequest, string origin = HttpFormatSymbols.STAR)
        => httpRequest
            .WithOrigin(origin)
            .WithHeader(HttpHeaderType.AccessControlRequestHeaders, HttpHeaderType.ContentType.AsString())
            .WithHeader(HttpHeaderType.AccessControlRequestMethod, httpRequest.Method.AsString());

    public static HttpRequest WithRequestId(this HttpRequest httpRequest, string requestId = null)
        => httpRequest.WithHeader(HttpHeaderType.RequestId, requestId ?? Guid.NewGuid().ToString());

    public static HttpRequest WithCorrelationId(this HttpRequest httpRequest, string correlationId)
        => httpRequest.WithHeader(HttpHeaderType.CorrelationId, correlationId);

    public static HttpRequest WithTraceParent(this HttpRequest httpRequest, string traceId = null)
        => httpRequest.WithHeader(HttpHeaderType.TraceParent,
            traceId ?? $"00-{Guid.NewGuid():N}-{Guid.NewGuid():N.Substring(0, 16)}-01");


    public static HttpRequest Clone(this HttpRequest httpRequest)
    {
        return new HttpRequest
        {
            Url = httpRequest.Url,
            Method = httpRequest.Method,
            Headers = new Dictionary<string, string>(httpRequest.Headers),
            Cookies = new Dictionary<string, string>(httpRequest.Cookies),
            Body = httpRequest.Body,
        };
    }

    public static HttpRequest Reset(this HttpRequest httpRequest)
    {
        httpRequest.Headers.Clear();
        httpRequest.Cookies.Clear();
        httpRequest.Body = null;

        return httpRequest;
    }

    public static HttpRequest Validate(this HttpRequest httpRequest)
    {
        if (string.IsNullOrEmpty(httpRequest.Url))
            throw new ArgumentException("URL cannot be null or empty");

        if (httpRequest.Method == null)
            throw new ArgumentException("HTTP method cannot be null");

        return httpRequest;
    }
}