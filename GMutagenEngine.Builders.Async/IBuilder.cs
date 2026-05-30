using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Builders.Async;

public interface IBuilder<TSelf, TId> : IParametrizedBuilder<TSelf, TId>, IBuilderMark where TSelf : Builder<TSelf, TId>
    where TId : notnull {
    public Task<TOut> Execute<TOut>(TId id, CancellationToken cancellationToken)
        where TOut : Builder<TSelf, TId>, new();

    Task<TData?> Build<TData>(TId id, CancellationToken cancellationToken);
}
public interface IBuilderMark : ISelfMark<IBuilderMark> {
}
