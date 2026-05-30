using GMutagenEngine.Handlers.Funcs.Interfaces;
using GMutagenEngine.Mediators.Api.General.Requests;
using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Mediators.Api.Sync.Requests;

public interface ISyncRequestHandler<in TRequest, out TResponse> : ISyncFuncHandler<TRequest, TResponse>, ISyncRequestHandlerMark where TRequest : IRequest<TResponse> {
}
public interface ISyncRequestHandlerMark : ISelfMark<ISyncRequestHandlerMark> {
}
