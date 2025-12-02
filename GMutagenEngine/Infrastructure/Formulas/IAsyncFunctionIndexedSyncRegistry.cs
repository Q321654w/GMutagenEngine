using GMutagenEngine.Infrastructure.Storing.Registries.Sync.Indexed;
using NCalc;

namespace GMutagenEngine.Infrastructure.Formulas
{
    public interface IAsyncFunctionIndexedSyncRegistry<TId> : IIndexedSyncRegistry<TId, AsyncExpressionFunction> where TId : notnull
    {
    }
}