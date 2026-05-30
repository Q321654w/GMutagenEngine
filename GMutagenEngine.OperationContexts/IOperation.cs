namespace GMutagenEngine.OperationContexts;

public interface IOperationContext<in T>
{
    IDisposable Push(T operationId);
}

public interface IOperation<T>
{
    bool TryGetId(out T operationId);
}
