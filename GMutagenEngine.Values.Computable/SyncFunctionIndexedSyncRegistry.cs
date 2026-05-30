using GMutagenEngine.Storing.Registries.Sync.Indexed;
using NCalc;

namespace GMutagenEngine.Values.Computable;

public class SyncFunctionIndexedSyncRegistry<TId> : InMemoryIndexedSyncRegistry<TId, ExpressionFunction>,
    ISyncFunctionIndexedSyncRegistry<TId> where TId : notnull
{
    
}