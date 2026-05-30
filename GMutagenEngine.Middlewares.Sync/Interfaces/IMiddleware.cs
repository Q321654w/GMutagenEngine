using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Middlewares.Sync.Interfaces;

public interface IInMiddleware : IInMiddlewareMark {
    
}

public interface IMiddleware : IMiddlewareMark {
    void Invoke(Action next);
}

public interface IOutMiddleware : IOutMiddlewareMark {
    
}

public interface IInOutMiddleware : IInOutMiddlewareMark {
}
