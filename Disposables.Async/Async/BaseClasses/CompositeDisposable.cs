namespace Disposables.Async.BaseClasses;

public class CompositeDisposable : IAsyncDisposable
{
    private readonly List<IAsyncDisposable> _disposables = new();

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

    public void Add(IAsyncDisposable disposable)
    {
        _disposables.Add(disposable);
    }
    
    public bool Remove(IAsyncDisposable disposable)
    {
        return _disposables.Remove(disposable);
    }
    
    public async ValueTask DisposeAsync()
    {
        foreach (var d in _disposables.ToArray())
            await d.DisposeAsync();
        
        GC.SuppressFinalize(this);
    }

    public override string ToString() =>
        $"{nameof(CompositeDisposable)}(Count={_disposables.Count})";
}