using GMutagenEngine.Infrastructure.Storing.Registries.Sync.Indexed;
using NCalc;

namespace GMutagenEngine.Infrastructure.Formulas
{
    public class AsyncFunctionIndexedSyncRegistry<TId> : InMemoryIndexedSyncRegistry<TId, AsyncExpressionFunction>, 
        IAsyncFunctionIndexedSyncRegistry<TId> where TId : notnull
    {
    }
}