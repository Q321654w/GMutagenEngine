using GMutagenEngine.MetaData.Runtime.Marks;
using GMutagenEngine.Storing.Registries.Sync.Indexed;
using NCalc;

namespace GMutagenEngine.Values.Computable;

public interface IAsyncFunctionIndexedSyncRegistry<TId> : IIndexedSyncRegistry<TId, AsyncExpressionFunction>, IAsyncFunctionIndexedSyncRegistryMark where TId : notnull {
}
public interface IAsyncFunctionIndexedSyncRegistryMark : ISelfMark<IAsyncFunctionIndexedSyncRegistryMark> {
}
