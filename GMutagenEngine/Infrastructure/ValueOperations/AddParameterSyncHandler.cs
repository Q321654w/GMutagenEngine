using GMutagenEngine.Concept.Sync.Entities.Extensions;
using GMutagenEngine.Concept.Sync.Values.Arithmetic;
using GMutagenEngine.Infrastructure.Handlers.Sync.Funcs;

namespace GMutagenEngine.Infrastructure.ValueOperations
{
    public class AddParameterSyncHandler<T> : ISyncFuncHandler<AddParameterSyncRequest<T>, bool>
    {
        public bool Handle(AddParameterSyncRequest<T> data)
        {
            var current = data.Target.ValueStorage().Get(data.ParameterId);
            if (current is not IAdd<T> typedValue)
                return false;

            typedValue.Add(data.Delta);
            return true;
        }
    }
}