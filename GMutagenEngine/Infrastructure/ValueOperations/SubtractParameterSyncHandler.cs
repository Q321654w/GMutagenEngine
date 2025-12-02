using GMutagenEngine.Concept.Sync.Entities.Extensions;
using GMutagenEngine.Concept.Sync.Values.Arithmetic;
using GMutagenEngine.Infrastructure.Handlers.Sync.Funcs;

namespace GMutagenEngine.Infrastructure.ValueOperations
{
    public class SubtractParameterSyncHandler<T> : ISyncFuncHandler<SubtractParameterSyncRequest<T>, bool>
    {
        public bool Handle(SubtractParameterSyncRequest<T> data)
        {
            var current = data.Target.ValueStorage().Get(data.ParameterId);
            if (current is not ISubtract<T> typedValue)
                return false;

            typedValue.Subtract(data.Delta);
            return true;
        }
    }
}