using GMutagenEngine.Handlers.Actions.Intarfeces;
using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Handlers.Actions.Interfaces;

public interface ISyncActionHandler : ISyncActionHandlerMark {
    void Handle();
}

public interface ISyncActionHandler<in TIn> : ISyncActionHandlerIn, ISyncActionHandlerMark {
    void Handle(TIn data);
}
public interface ISyncActionHandlerMark : ISelfMark<ISyncActionHandlerMark> {
}
