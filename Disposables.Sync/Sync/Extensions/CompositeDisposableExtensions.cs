using Disposables.Sync.BaseClasses;

namespace Disposables.Sync.Extensions;

public static class CompositeDisposableExtensions
{
    public static CompositeDisposable AsCompositeDisposable(this IEnumerable<IDisposable> disposables)
    {
        return new CompositeDisposable(disposables);
    }

    public static void AddRange(this CompositeDisposable composite, params IDisposable[] disposables)
    {
        foreach (var d in disposables)
            composite.Add(d);
    }
    
    public static void AddRange(this CompositeDisposable composite, IEnumerable<IDisposable> disposables)
    {
        foreach (var d in disposables)
            composite.Add(d);
    }

    public static CompositeDisposable AddTo(this IDisposable disposable, CompositeDisposable composite)
    {
        composite.Add(disposable);
        return composite;
    }
}