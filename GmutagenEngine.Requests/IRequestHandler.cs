using GMutagenEngine.MetaData.Runtime.Marks;

namespace GmutagenEngine.Requests;

public interface IRequestHandler<in TIn, TOut> : IDisposable, IRequestHandlerMark where TOut : IResponse {
    Task<TOut> Send(TIn data);
}

public interface IRequestHandlerMark : ISelfMark<IRequestHandlerMark>
{
}