using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Builders.Sync;

public interface IParametrizedBuilder<TSelf, TId> : IParametrizedBuilderMark where TSelf : Builder<TSelf, TId>
    where TId : notnull {
    public TOut Execute<TData, TOut>(TData data, TId id)
        where TOut : Builder<TSelf, TId>, new();
}
public interface IParametrizedBuilderMark : ISelfMark<IParametrizedBuilderMark> {
}
