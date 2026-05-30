using GMutagenEngine.Identification.Interfaces;
using GMutagenEngine.Identification.Utils;

namespace GMutagenEngine.Identification.Realizations.Hashed;

public class HashedIdCached : ISingleId<byte[]>, IEquatable<HashedIdCached>, IIdOptimized
{
    private readonly int _hashCode;
    public byte[]? Value { get; }
    object? ISingleId.Value => Value;

    public HashedIdCached(byte[]? value)
    {
        Value = value;
        _hashCode = IdHashCodeHelper.ComputeSingleHash(value);
    }

    public override int GetHashCode() => _hashCode;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is HashedIdCached other) return Equals(other);

#if ENABLE_SINGLE_COMPOSITE_EQUALITY || ENABLE_CROSS_TYPE_EQUALITY
        return IdEqualityHelper.TryCrossTypeEquality(this, obj);
#else
            return false;
#endif
    }

    public bool Equals(HashedIdCached? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (_hashCode != other._hashCode) return false;

        if (Value is null && other.Value is null) return true;
        if (Value is null || other.Value is null) return false;

        return Value.SequenceEqual(other.Value);
    }
}