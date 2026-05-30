/*using GMutagenEngine.Infrastructure.GMutagenEngine.Identification.Identifiable.Interfaces;

namespace GMutagenEngine.Infrastructure.StateMachines.Sync;

public interface IMultiStateMachine<TStateId, TTopicId> : IAdvancedIdentifiable<TStateId>, IMultiStateMachineMark {
    IControlledStateMachine<TStateId, TTopicId>[] StateMachines { get; }
    IMultiStateMachineTransition<TStateId, TTopicId>[] Transitions { get; }

    void Start();
    void Transit(IMultiStateMachineTransition<TStateId, TTopicId> transition);
    void Stop();
}*/
namespace GmutagenEngine.StateMachines.Sync;

public interface IMultiStateMachineMark : GMutagenEngine.MetaData.Runtime.Marks.ISelfMark<IMultiStateMachineMark> {
}