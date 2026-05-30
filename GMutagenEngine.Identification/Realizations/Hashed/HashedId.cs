using GMutagenEngine.Identification.Interfaces;
using GMutagenEngine.Identification.Utils;

namespace GMutagenEngine.Identification.Realizations.Hashed;

public class HashedId : ISingleId<byte[]>, IEquatable<HashedId>
{
    public byte[]? Value { get; }
    object? ISingleId.Value => Value;

    public HashedId(byte[]? value) => Value = value;

    public override int GetHashCode() => IdHashCodeHelper.ComputeSingleHash(Value);

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is HashedId other) return Equals(other);

#if ENABLE_SINGLE_COMPOSITE_EQUALITY || ENABLE_CROSS_TYPE_EQUALITY
        return IdEqualityHelper.TryCrossTypeEquality(this, obj);
#else
            return false;
#endif
    }

    public bool Equals(HashedId? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

        if (Value is null && other.Value is null) return true;
        if (Value is null || other.Value is null) return false;

        return Value.SequenceEqual(other.Value);
    }
}