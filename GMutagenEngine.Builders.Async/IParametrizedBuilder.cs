using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Builders.Async;

public interface IParametrizedBuilder<TSelf, TId> : IParametrizedBuilderMark where TSelf : Builder<TSelf, TId>
    where TId : notnull {
    public Task<TOut> Execute<TData, TOut>(TData data, TId id, CancellationToken cancellationToken)
        where TOut : Builder<TSelf, TId>, new();
}
public interface IParametrizedBuilderMark : ISelfMark<IParametrizedBuilderMark> {
}
