using GMutagenEngine.Infrastructure.Disposables.Sync.BaseClases;

namespace GMutagenEngine.Infrastructure.Disposables.Sync.Extensions
{
    public static class CompositeDisposableExtensions
    {
        public static CompositeDisposable AsCompositeDisposable(this IEnumerable<IDisposable> disposables) 
        {
            return new CompositeDisposable(disposables);
        }
        
        public static T Add<T>(this T disposable, CompositeDisposable composite) where T : IDisposable
        {
            composite.Add(disposable);
            return disposable;
        }
    
        public static void AddRange(this CompositeDisposable composite, params IDisposable[] disposables)
        {
            if (composite == null)
                throw new ArgumentNullException(nameof(composite));
        
            foreach (var d in disposables)
                composite.AddRange(d);
        }
    
        public static CompositeDisposable AddTo(this IDisposable disposable, CompositeDisposable composite)
        {
            composite.Add(disposable);
            return composite;
        }
    
        public static CompositeDisposable AddToIfNotNull(this IDisposable? disposable, CompositeDisposable composite)
        {
            if (disposable != null)
                composite.Add(disposable);
        
            return composite;
        }
    }
}