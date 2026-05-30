using GMutagenEngine.MetaData.Runtime.Marks;

namespace GmutagenEngine.Requests.Http;

public interface IHttpRequest : IHttpRequestMark {
    string Url { get; }
    HttpMethod Method { get; }

    Dictionary<string, string> Headers { get; }
    Dictionary<string, string> Cookies { get; }

    string? Body { get; }
}
public interface IHttpRequestMark : ISelfMark<IHttpRequestMark> {
}
