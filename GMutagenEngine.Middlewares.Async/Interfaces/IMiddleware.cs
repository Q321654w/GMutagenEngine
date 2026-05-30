using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Middlewares.Async.Interfaces;


public interface IMiddleware : IMiddlewareMark {
    Task Invoke(Func<CancellationToken, Task> next, CancellationToken cancellationToken = default);
}

public interface IInMiddleware : IInMiddlewareMark {
}

public interface IOutMiddleware : IOutMiddlewareMark {
}

public interface IInOutMiddleware : IInOutMiddlewareMark {
}