namespace GMutagenEngine.OperationContexts;

public class OperationContext<T> : IOperationContext<T>, IOperation<T>
{
    private readonly AsyncLocal<T> _current = new();
    private readonly Stack<T> _chain = new();

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

    public T Pop()
    {
        return _chain.Pop();
    }

    public void Push(T operationId)
    {
        if (_current.Value != null)
            _chain.Push(_current.Value);

        _current.Value = operationId;
    }
}