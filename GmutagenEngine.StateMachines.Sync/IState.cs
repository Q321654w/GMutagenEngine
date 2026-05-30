
/*
using GMutagenEngine.Infrastructure.GMutagenEngine.Identification.Identifiable.Interfaces;

namespace GMutagenEngine.Infrastructure.StateMachines.Sync;

public interface IState<out TId> : IAdvancedIdentifiable<TId>, IStateMark {
    public IStateBehaviour Behaviour { get; }
}*/
namespace GmutagenEngine.StateMachines.Sync;

public interface IStateMark : GMutagenEngine.MetaData.Runtime.Marks.ISelfMark<IStateMark> {
}