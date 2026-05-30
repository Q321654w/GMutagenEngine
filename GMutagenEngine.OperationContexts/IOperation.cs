namespace GMutagenEngine.OperationContexts;

public interface IOperationContext<T>
{
    void Push(T operationId);
    T Pop();
}

public interface IOperation<T>
{
    bool TryGetId(out T operationId);
}
