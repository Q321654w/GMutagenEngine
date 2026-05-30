using GMutagenEngine.MetaData.Runtime.Marks;

namespace GmutagenEngine.Requests.Http;

public interface IHttpClient : IHttpClientMark {
    Task<IHttpResponse> SendAsync(IHttpRequest request);
}

public interface IHttpClientMark : ISelfMark<IHttpClientMark>
{
}