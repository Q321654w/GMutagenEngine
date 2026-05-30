namespace Disposables.Async.Realizations;

public class AsyncActionDisposable(Func<Task> func) : IAsyncDisposable
{
    private bool _disposed;

    public ValueTask DisposeAsync()
    {
        if (_disposed)
            return ValueTask.CompletedTask;
        
        _disposed = true;
        GC.SuppressFinalize(this);
        return new ValueTask(func());
    }
}