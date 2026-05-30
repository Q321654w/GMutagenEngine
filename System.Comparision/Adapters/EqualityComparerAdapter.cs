using System.Diagnostics.CodeAnalysis;
using System.Equality;

namespace System.Adapters;

public class EqualityComparerAdapter(IEqualityComparer comparer, IHashComputer hashComputer)
    : Collections.IEqualityComparer
{
    private readonly IEqualityComparer _comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));

    private readonly IHashComputer
        _hashComputer = hashComputer ?? throw new ArgumentNullException(nameof(hashComputer));

    public new bool Equals(object? x, object? y)
    {
        return _comparer.Equals(x, y);
    }

    public int GetHashCode([DisallowNull] object obj)
    {
        if (obj == null) throw new ArgumentNullException(nameof(obj));
        return _hashComputer.Compute(obj);
    }
}

public class EqualityComparerAdapter<T>(
    Equality.IEqualityComparer<T> comparer,
    IHashComputer<T> hashComputer)
    : Collections.Generic.IEqualityComparer<T>
{
    private readonly Equality.IEqualityComparer<T> _comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));

    private readonly IHashComputer<T> _hashComputer =
        hashComputer ?? throw new ArgumentNullException(nameof(hashComputer));

    public bool Equals(T? x, T? y)
    {
        return _comparer.Equals(x, y);
    }

    public int GetHashCode([DisallowNull] T obj)
    {
        if (obj == null) throw new ArgumentNullException(nameof(obj));
        return _hashComputer.Compute(obj);
    }
}

public class EqualityComparerAdapter<T1, T2>(
    Equality.IEqualityComparer<T1, T2> comparer,
    IHashComputer<T1> hashComputer1,
    IHashComputer<T2> hashComputer2)
    : Equality.IEqualityComparer<T1, T2>
{
    private readonly Equality.IEqualityComparer<T1, T2>
        _comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));

    private readonly IHashComputer<T1> _hashComputer1 =
        hashComputer1 ?? throw new ArgumentNullException(nameof(hashComputer1));

    private readonly IHashComputer<T2> _hashComputer2 =
        hashComputer2 ?? throw new ArgumentNullException(nameof(hashComputer2));

    public bool Equals(T1? x, T2? y)
    {
        return _comparer.Equals(x, y);
    }

    public int GetHashCode([DisallowNull] T1 obj)
    {
        if (obj == null) throw new ArgumentNullException(nameof(obj));
        return _hashComputer1.Compute(obj);
    }

    public int GetHashCode([DisallowNull] T2 obj)
    {
        if (obj == null) throw new ArgumentNullException(nameof(obj));
        return _hashComputer2.Compute(obj);
    }
}