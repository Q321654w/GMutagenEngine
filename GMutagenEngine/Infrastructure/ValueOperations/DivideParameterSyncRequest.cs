using GMutagenEngine.Concept.Sync.Entities.Interfaces;
using GMutagenEngine.Infrastructure.Commands;
using GMutagenEngine.Infrastructure.Identification.Interfaces;

namespace GMutagenEngine.Infrastructure.ValueOperations
{
    public abstract record DivideParameterSyncRequest<T>(
        IEntity Sender,
        IEntity Target,
        IId ParameterId,
        T Divisor
    ) : SyncRequest<bool>(Sender, Target)
    {
        public IId ParameterId { get; } = ParameterId;
        public T Divisor { get; } = Divisor;
    }
}