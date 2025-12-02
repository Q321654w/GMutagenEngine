using GMutagenEngine.Concept.Sync.Entities.Extensions;
using GMutagenEngine.Concept.Sync.Values.Arithmetic;
using GMutagenEngine.Infrastructure.Handlers.Sync.Funcs;

namespace GMutagenEngine.Infrastructure.ValueOperations
{
    public class DivideParameterSyncHandler<T> : ISyncFuncHandler<DivideParameterSyncRequest<T>, bool>
    {
        public bool Handle(DivideParameterSyncRequest<T> data)
        {
            var current = data.Target.ValueStorage().Get(data.ParameterId);
            if (current is not IDivide<T> typedValue)
                return false;

            typedValue.Divide(data.Divisor);
            return true;
        }
    }
}