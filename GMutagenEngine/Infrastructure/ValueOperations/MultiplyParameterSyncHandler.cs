using GMutagenEngine.Concept.Sync.Entities.Extensions;
using GMutagenEngine.Concept.Sync.Values.Arithmetic;
using GMutagenEngine.Infrastructure.Handlers.Sync.Funcs;

namespace GMutagenEngine.Infrastructure.ValueOperations
{
    public class MultiplyParameterSyncHandler<T> : ISyncFuncHandler<MultiplyParameterSyncRequest<T>, bool>
    {
        public bool Handle(MultiplyParameterSyncRequest<T> data)
        {
            var current = data.Target.ValueStorage().Get(data.ParameterId);
            if (current is not IMultiply<T> typedValue)
                return false;

            typedValue.Multiply(data.Factor);
            return true;
        }
    }
}