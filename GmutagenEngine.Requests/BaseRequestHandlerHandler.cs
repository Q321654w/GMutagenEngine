using GmutagenEngine.Requests.Http;

namespace GmutagenEngine.Requests
{
    public abstract class BaseRequestHandlerHandler<TIn, TOut>(IHttpClient client) : IRequestHandler<TIn, TOut>
        where TOut : IResponse
    {
        protected readonly IHttpClient Client = client;

        public async Task<TOut> Send(TIn data)
        {
            var request = await PrepareRequestAsync(data);
            var response = await Client.SendAsync(request);
            return await HandleResponseAsync(response);
        }

        protected abstract Task<IHttpRequest> PrepareRequestAsync(TIn data);

        protected abstract Task<TOut> HandleResponseAsync(IHttpResponse response);

        public virtual void Dispose()
        {
        }
    }
}