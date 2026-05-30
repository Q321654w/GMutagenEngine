using GMutagenEngine.MetaData.Runtime.Marks;
using GMutagenEngine.Storing.Registries.Sync.Indexed;
using NCalc;

namespace GMutagenEngine.Values.Computable;

public interface ISyncFunctionIndexedSyncRegistry<TId> : IIndexedSyncRegistry<TId, ExpressionFunction>, ISyncFunctionIndexedSyncRegistryMark where TId : notnull {
}
public interface ISyncFunctionIndexedSyncRegistryMark : ISelfMark<ISyncFunctionIndexedSyncRegistryMark> {
}
