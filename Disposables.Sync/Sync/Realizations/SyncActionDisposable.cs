namespace Disposables.Sync.Realizations;

public class SyncActionDisposable(Action action) : IDisposable
{
    private bool _disposed;

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        action();
        GC.SuppressFinalize(this);
    }
}