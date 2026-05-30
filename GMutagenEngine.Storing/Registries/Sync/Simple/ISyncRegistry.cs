using GMutagenEngine.MetaData.Runtime.Marks;

namespace GMutagenEngine.Storing.Registries.Sync.Simple;

public interface ISyncRegistry<T> : ISyncRegistryMark
{
    T GetBy(Func<T, bool> predicate);
    bool TryGetBy(Func<T, bool> predicate, out T value);
    IEnumerable<T> GetAllBy(Func<T, bool> predicate);
    IEnumerable<T> GetAll();
    bool ExistsBy(Func<T, bool> predicate);
}


public interface IQuerySet<T> : IEnumerable<T>, IQuerySetMark {
    bool Any(Func<T, bool> predicate);
    bool All(Func<T, bool> predicate);
    
    T GetBy(Func<T, bool> predicate);
    bool TryGetBy(Func<T, bool> predicate, out T value);
    IEnumerable<T> GetAllBy(Func<T, bool> predicate);
    IEnumerable<T> GetAll();
    
    
    bool Contains(T value);
    bool IsNotContains(T value);
    void UnionWith(T value);
    IEnumerable<T> Union(T value);
    void IntersectWith(T value);
    IEnumerable<T> Intersect(T value);
    void ExceptWith(T value);
    IEnumerable<T> Except(T value);
    void ExtendWith(T value);
    IEnumerable<T> Extend(T value);
    void SymmetricExceptWith(T value);
    IEnumerable<T> SymmetricExcept(T value);
    bool IsStrictSubsetOf(IEnumerable<T> other);
    bool IsSubsetOf(IEnumerable<T> other);
    bool IsStrictSupersetOf(IEnumerable<T> other);
    bool IsSupersetOf(IEnumerable<T> other);
    bool HasIntersection(IEnumerable<T> other);
    bool HasNotIntersection(IEnumerable<T> other);
    bool IsInfinite();
    bool IsNotInfinite();
    bool IsCountable();
    bool IsNotCountable();
    int Count();
}

public interface IIndexedQuerySet<TKey, T> : IQuerySet<T>, IIndexedQuerySetMark {
    bool ContainsKey(TKey key);
    T GetByKey(TKey key);
    bool TryGetByKey(TKey key, out T value);
}

public interface ISyncRegistryMark : ISelfMark<ISyncRegistryMark> {
}

public interface IQuerySetMark : ISelfMark<IQuerySetMark> {
}

public interface IIndexedQuerySetMark : ISelfMark<IIndexedQuerySetMark> {
}