using GMutagenEngine.Identification.Interfaces;
using GMutagenEngine.Identification.Utils;

namespace GMutagenEngine.Identification.Realizations.Single;

public class SingleIdCached<T> : ISingleId<T>, IEquatable<SingleIdCached<T>>, IIdOptimized
{
    private readonly int _hashCode;
    public T? Value { get; }
    object? ISingleId.Value => Value;

    public SingleIdCached(T? value)
    {
        Value = value;
        _hashCode = IdHashCodeHelper.ComputeSingleHash(value);
    }

    public override int GetHashCode() => _hashCode;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is SingleIdCached<T> other) return Equals(other);

#if ENABLE_SINGLE_COMPOSITE_EQUALITY || ENABLE_CROSS_TYPE_EQUALITY
        return IdEqualityHelper.TryCrossTypeEquality(this, obj);
#else
        return false;
#endif
    }

    public bool Equals(SingleIdCached<T>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (_hashCode != other._hashCode) return false;
        return IdEqualityHelper.AreValuesEqual(Value, other.Value);
    }

    public static implicit operator SingleIdCached<T>(T date)
        => new(date);
}