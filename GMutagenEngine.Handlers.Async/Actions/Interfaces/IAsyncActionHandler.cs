using GMutagenEngine.Handlers.Async.Actions.Marks;
using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Handlers.Async.Actions.Interfaces;

public interface IAsyncActionHandler : IAsyncActionHandlerMark {
    Task Handle(CancellationToken cancellationToken = default);
}

public interface IAsyncActionHandler<in TIn> : IAsyncActionHandlerIn, IAsyncActionHandlerInMark, IAsyncActionHandlerMark {
    Task Handle(TIn data, CancellationToken cancellationToken = default);
}
public interface IAsyncActionHandlerMark : ISelfMark<IAsyncActionHandlerMark> {
}
