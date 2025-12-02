using GMutagenEngine.Infrastructure.Storing.Registries.Sync.Indexed;
using NCalc;

namespace GMutagenEngine.Infrastructure.Formulas
{
    public class SyncFunctionIndexedSyncRegistry<TId> : InMemoryIndexedSyncRegistry<TId, ExpressionFunction>,
        ISyncFunctionIndexedSyncRegistry<TId> where TId : notnull
    {
    
    }
}