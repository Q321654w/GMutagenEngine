namespace GMutagenEngine.Infrastructure.Equality;

public interface IEqualityComparer : IEqualityComparer<object>
{
}

public interface IEqualityComparer<in T> : IEqualityComparer<T, T>
{
}

public interface IEqualityComparer<in T1, in T2>
{
    bool Equals(T1? obj1, T2? obj2);
}
