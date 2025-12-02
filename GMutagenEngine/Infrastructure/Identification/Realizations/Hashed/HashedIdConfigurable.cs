using GMutagenEngine.Infrastructure.Identification.Constants;
using GMutagenEngine.Infrastructure.Identification.Interfaces;
using GMutagenEngine.Infrastructure.Identification.Utils;

namespace GMutagenEngine.Infrastructure.Identification.Realizations.Hashed;

public class HashedIdConfigurable : ISingleId<byte[]>, IConfigurableId,
    IEquatable<HashedIdConfigurable>
{
    public byte[]? Value { get; }
    object? ISingleId.Value => Value;
    public IdEqualityBehavior EqualityBehavior { get; }

    public HashedIdConfigurable(byte[]? value,
        IdEqualityBehavior behavior = IdEqualityBehavior.Strict)
    {
        Value = value;
        EqualityBehavior = behavior;
    }

    public override int GetHashCode() => IdHashCodeHelper.ComputeSingleHash(Value);

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is HashedIdConfigurable other) return Equals(other);

#if ENABLE_SINGLE_COMPOSITE_EQUALITY || ENABLE_CROSS_TYPE_EQUALITY
        return IdEqualityHelper.TryCrossTypeEquality(this, obj);
#else
            return false;
#endif
    }

    public bool Equals(HashedIdConfigurable? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

        if (Value is null && other.Value is null) return true;
        if (Value is null || other.Value is null) return false;

        return Value.SequenceEqual(other.Value);
    }
}