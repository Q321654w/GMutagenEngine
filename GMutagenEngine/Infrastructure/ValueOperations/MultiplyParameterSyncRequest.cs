using GMutagenEngine.Concept.Sync.Entities.Interfaces;
using GMutagenEngine.Infrastructure.Commands;
using GMutagenEngine.Infrastructure.Identification.Interfaces;

namespace GMutagenEngine.Infrastructure.ValueOperations
{
    public abstract record MultiplyParameterSyncRequest<T>(
        IEntity Sender,
        IEntity Target,
        IId ParameterId,
        T Factor
    ) : SyncRequest<bool>(Sender, Target)
    {
        public IId ParameterId { get; } = ParameterId;
        public T Factor { get; } = Factor;
    }
}