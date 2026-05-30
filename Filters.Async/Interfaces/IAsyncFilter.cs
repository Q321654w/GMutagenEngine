using GMutagenEngine.Handlers.Async.Funcs.Interfaces;
using GMutagenEngine.MetaData.Runtime.Marks;

namespace Filters.Async.Interfaces;

public interface IAsyncFilter<in T> : IAsyncFuncHandler<T, bool>, IAsyncFilterMark {
    
}
public interface IAsyncFilterMark : ISelfMark<IAsyncFilterMark> {
}
