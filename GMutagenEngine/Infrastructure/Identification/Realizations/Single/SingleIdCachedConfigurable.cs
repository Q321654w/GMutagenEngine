using GMutagenEngine.Infrastructure.Identification.Constants;
using GMutagenEngine.Infrastructure.Identification.Interfaces;
using GMutagenEngine.Infrastructure.Identification.Utils;

namespace GMutagenEngine.Infrastructure.Identification.Realizations.Single;

public class SingleIdCachedConfigurable<T> : ISingleId<T>, IConfigurableId,
    IEquatable<SingleIdCachedConfigurable<T>>, IIdOptimized
{
    private readonly int _hashCode;
    public T? Value { get; }
    object? ISingleId.Value => Value;
    public IdEqualityBehavior EqualityBehavior { get; }

    public SingleIdCachedConfigurable(T? value, IdEqualityBehavior behavior = IdEqualityBehavior.Strict)
    {
        Value = value;
        EqualityBehavior = behavior;
        _hashCode = IdHashCodeHelper.ComputeSingleHash(value);
    }

    public override int GetHashCode() => _hashCode;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is SingleIdCachedConfigurable<T> other) return Equals(other);

#if ENABLE_SINGLE_COMPOSITE_EQUALITY || ENABLE_CROSS_TYPE_EQUALITY
        return IdEqualityHelper.TryCrossTypeEquality(this, obj);
#else
        return false;
#endif
    }

    public bool Equals(SingleIdCachedConfigurable<T>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (_hashCode != other._hashCode) return false;
        return IdEqualityHelper.AreValuesEqual(Value, other.Value);
    }


    public static implicit operator SingleIdCachedConfigurable<T>(T date)
        => new(date);
}