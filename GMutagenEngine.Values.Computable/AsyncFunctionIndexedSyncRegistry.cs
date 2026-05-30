using GMutagenEngine.Storing.Registries.Sync.Indexed;
using NCalc;

namespace GMutagenEngine.Values.Computable;

public class AsyncFunctionIndexedSyncRegistry<TId> : InMemoryIndexedSyncRegistry<TId, AsyncExpressionFunction>, 
    IAsyncFunctionIndexedSyncRegistry<TId> where TId : notnull
{
}