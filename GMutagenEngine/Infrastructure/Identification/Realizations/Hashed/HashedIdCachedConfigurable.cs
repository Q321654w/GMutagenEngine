using GMutagenEngine.Infrastructure.Identification.Constants;
using GMutagenEngine.Infrastructure.Identification.Interfaces;
using GMutagenEngine.Infrastructure.Identification.Utils;

namespace GMutagenEngine.Infrastructure.Identification.Realizations.Hashed;

public class HashedIdCachedConfigurable : ISingleId<byte[]>, IConfigurableId,
    IEquatable<HashedIdCachedConfigurable>, IIdOptimized
{
    private readonly int _hashCode;
    public byte[]? Value { get; }
    object? ISingleId.Value => Value;
    public IdEqualityBehavior EqualityBehavior { get; }

    public HashedIdCachedConfigurable(byte[]? value,
        IdEqualityBehavior behavior = IdEqualityBehavior.Strict)
    {
        Value = value;
        EqualityBehavior = behavior;
        _hashCode = IdHashCodeHelper.ComputeSingleHash(value);
    }

    public override int GetHashCode() => _hashCode;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is HashedIdCachedConfigurable other) return Equals(other);

#if ENABLE_SINGLE_COMPOSITE_EQUALITY || ENABLE_CROSS_TYPE_EQUALITY
        return IdEqualityHelper.TryCrossTypeEquality(this, obj);
#else
            return false;
#endif
    }

    public bool Equals(HashedIdCachedConfigurable? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (_hashCode != other._hashCode) return false;

        if (Value is null && other.Value is null) return true;
        if (Value is null || other.Value is null) return false;

        return Value.SequenceEqual(other.Value);
    }
}