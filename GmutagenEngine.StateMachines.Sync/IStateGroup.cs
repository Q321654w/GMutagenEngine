/*using GMutagenEngine.Infrastructure.GMutagenEngine.Identification.Identifiable.Interfaces;

namespace GMutagenEngine.Infrastructure.StateMachines.Sync;

public interface IStateGroup<out TId> : IAdvancedIdentifiable<TId>, IStateGroupMark {
    IState<TId>[] States { get; }
}*/
namespace GmutagenEngine.StateMachines.Sync;

public interface IStateGroupMark : GMutagenEngine.MetaData.Runtime.Marks.ISelfMark<IStateGroupMark> {
}