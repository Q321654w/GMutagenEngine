using GMutagenEngine.MetaData.Runtime.Marks;

namespace GmutagenEngine.Requests.Http;

public interface IHttpResponse : IHttpResponseMark {
    int StatusCode { get; }
    string? Body { get; }
    Dictionary<string, string> Headers { get; }
}

public interface IHttpResponseMark : ISelfMark<IHttpResponseMark>
{
}