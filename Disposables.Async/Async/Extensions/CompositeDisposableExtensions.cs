using Disposables.Async.BaseClasses;

namespace Disposables.Async.Extensions;


public static class CompositeDisposableExtensions
{
    public static CompositeDisposable AsCompositeDisposable(this IEnumerable<IAsyncDisposable> disposables)
    {
        return new CompositeDisposable(disposables);
    }

    public static void AddRange(this CompositeDisposable composite, params IAsyncDisposable[] disposables)
    {
        foreach (var d in disposables)
            composite.Add(d);
    }
    
    public static void AddRange(this CompositeDisposable composite, IEnumerable<IAsyncDisposable> disposables)
    {
        foreach (var d in disposables)
            composite.Add(d);
    }

    public static CompositeDisposable AddTo(this IAsyncDisposable disposable, CompositeDisposable composite)
    {
        composite.Add(disposable);
        return composite;
    }
}