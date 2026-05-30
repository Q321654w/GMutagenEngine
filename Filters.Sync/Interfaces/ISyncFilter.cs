using GMutagenEngine.Handlers.Funcs.Interfaces;
using GMutagenEngine.MetaData.Runtime.Marks;

namespace Filters.Sync.Interfaces;

public interface ISyncFilter<in T> : ISyncFuncHandler<T, bool>, ISyncFilterMark {
    
}
public interface ISyncFilterMark : ISelfMark<ISyncFilterMark> {
}
