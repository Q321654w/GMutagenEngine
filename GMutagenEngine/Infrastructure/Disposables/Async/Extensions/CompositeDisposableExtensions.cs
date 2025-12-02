using GMutagenEngine.Infrastructure.Disposables.Async.BaseClases;

namespace GMutagenEngine.Infrastructure.Disposables.Async.Extensions
{
    public static class CompositeDisposableExtensions
    {
        public static CompositeDisposable AsCompositeDisposable(this IEnumerable<IAsyncDisposable> disposables) 
        {
            return new CompositeDisposable(disposables);
        }
        
        public static T Add<T>(this T disposable, CompositeDisposable composite) where T : IAsyncDisposable
        {
            composite.Add(disposable);
            return disposable;
        }
    
        public static void AddRange(this CompositeDisposable composite, params IAsyncDisposable[] disposables)
        {
            if (composite == null)
                throw new ArgumentNullException(nameof(composite));
        
            foreach (var d in disposables)
                composite.AddRange(d);
        }
    
        public static CompositeDisposable AddTo(this IAsyncDisposable disposable, CompositeDisposable composite)
        {
            composite.Add(disposable);
            return composite;
        }
    
        public static CompositeDisposable AddToIfNotNull(this IAsyncDisposable? disposable, CompositeDisposable composite)
        {
            if (disposable != null)
                composite.Add(disposable);
        
            return composite;
        }
    }
}