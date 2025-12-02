using GMutagenEngine.Infrastructure.Storing.Registries.Sync.Indexed;
using NCalc;

namespace GMutagenEngine.Infrastructure.Formulas
{
    public interface ISyncFunctionIndexedSyncRegistry<TId> : IIndexedSyncRegistry<TId, ExpressionFunction> where TId : notnull
    {
    }
}