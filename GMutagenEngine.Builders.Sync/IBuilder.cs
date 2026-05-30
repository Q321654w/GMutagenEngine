using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Builders.Sync;

public interface IBuilder<TSelf, TId> : IParametrizedBuilder<TSelf, TId>, IBuilderMark where TSelf : Builder<TSelf, TId>
    where TId : notnull {
    public TOut Execute<TOut>(TId id)
        where TOut : Builder<TSelf, TId>, new();

    TData? Build<TData>(TId id);
}
public interface IBuilderMark : ISelfMark<IBuilderMark> {
}
