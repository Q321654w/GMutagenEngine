namespace GMutagenEngine.Infrastructure.Disposables.Sync.BaseClases
{
    public class CompositeDisposable : IDisposable
    {
        private readonly List<IDisposable> _disposables = new();
        private bool _disposed = false;

        public bool IsDisposed => _disposed;

        public CompositeDisposable()
        {
        }

        public CompositeDisposable(params IDisposable[] disposables)
        {
            _disposables.AddRange(disposables);
        }

        public CompositeDisposable(IEnumerable<IDisposable> disposables)
        {
            _disposables.AddRange(disposables);
        }

        public void Add(IDisposable disposable)
        {
            if (_disposed)
                disposable.Dispose();
            else
                _disposables.Add(disposable);
        }
    

        public bool Remove(IDisposable disposable)
        {
            return _disposables.Remove(disposable);
        }

        public void Clear()
        {
            foreach (var d in _disposables.ToArray())
                d.Dispose();

            _disposables.Clear();
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            _disposed = true;
            Clear();
            GC.SuppressFinalize(this);
        }

        public override string ToString() =>
            $"{nameof(CompositeDisposable)}(Count={_disposables.Count}, IsDisposed={_disposed})";
    }
}