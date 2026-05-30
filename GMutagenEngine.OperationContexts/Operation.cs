namespace GMutagenEngine.OperationContexts;

public class OperationContext<T> : IOperationContext<T>, IOperation<T>
{
    private readonly AsyncLocal<T> _current = new();

    public bool TryGetId(out T operationId)
    {
        var currentOperationId = _current.Value;
        if (currentOperationId is null)
        {
            operationId = default;
            return false;
        }

        operationId = currentOperationId;
        return true;
    }

    public IDisposable Push(T operationId)
    {
        var previous = _current.Value;
        _current.Value = operationId;
        return new PopHandle<T>(this, previous);
    }
}


public class PopHandle<T>(OperationContext<T> owner, T previous) : IDisposable
{
    private int _disposed;

    public void Dispose()
    {
        if (Interlocked.Exchange(ref _disposed, 1) != 0)
            return;

        owner._current.Value = previous;
    }
}