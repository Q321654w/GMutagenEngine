namespace Disposables.Sync.BaseClasses;

public class CompositeDisposable : IDisposable
{
    private readonly List<IDisposable> _disposables = new();

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
        _disposables.Add(disposable);
    }
    
    public bool Remove(IDisposable disposable)
    {
        return _disposables.Remove(disposable);
    }
    
    public void Dispose()
    {
        foreach (var d in _disposables.ToArray())
            d.Dispose();
        
        _disposables.Clear();
        GC.SuppressFinalize(this);
    }

    public override string ToString() =>
        $"{nameof(CompositeDisposable)}(Count={_disposables.Count})";
}