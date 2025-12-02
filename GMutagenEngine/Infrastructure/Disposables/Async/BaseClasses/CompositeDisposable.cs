namespace GMutagenEngine.Infrastructure.Disposables.Async.BaseClases
{
    public class CompositeDisposable : IAsyncDisposable
    {
        private readonly List<IAsyncDisposable> _disposables = new();
        private bool _disposed = false;

        public bool IsDisposed => _disposed;

        public CompositeDisposable()
        {
        }

        public CompositeDisposable(params IAsyncDisposable[] disposables)
        {
            _disposables.AddRange(disposables);
        }

        public CompositeDisposable(IEnumerable<IAsyncDisposable> disposables)
        {
            _disposables.AddRange(disposables);
        }

        public async Task Add(IAsyncDisposable disposable)
        {
            if (_disposed)
                await disposable.DisposeAsync();
            else
                _disposables.Add(disposable);
        }
    

        public bool Remove(IAsyncDisposable disposable)
        {
            return _disposables.Remove(disposable);
        }

        public async Task Clear()
        {
            foreach (var d in _disposables.ToArray())
                await d.DisposeAsync();

            _disposables.Clear();
        }

        public async ValueTask DisposeAsync()
        {
            if (_disposed)
                return;

            _disposed = true;
            await Clear();
            GC.SuppressFinalize(this);
        }

        public override string ToString() =>
            $"{nameof(CompositeDisposable)}(Count={_disposables.Count}, IsDisposed={_disposed})";
    }
}