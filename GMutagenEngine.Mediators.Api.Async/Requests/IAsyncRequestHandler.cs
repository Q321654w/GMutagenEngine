using GMutagenEngine.Handlers.Async.Funcs.Interfaces;
using GMutagenEngine.Mediators.Api.General.Requests;
using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Mediators.Api.Async.Requests;

public interface IAsyncRequestHandler<in TRequest, TResponse> : IAsyncFuncHandler<TRequest, TResponse>, IAsyncRequestHandlerMark where TRequest : IRequest<TResponse> {
}
public interface IAsyncRequestHandlerMark : ISelfMark<IAsyncRequestHandlerMark> {
}