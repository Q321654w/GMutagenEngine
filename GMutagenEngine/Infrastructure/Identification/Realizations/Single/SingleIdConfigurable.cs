using GMutagenEngine.Infrastructure.Identification.Constants;
using GMutagenEngine.Infrastructure.Identification.Interfaces;
using GMutagenEngine.Infrastructure.Identification.Utils;

namespace GMutagenEngine.Infrastructure.Identification.Realizations.Single;

public class SingleIdConfigurable<T> : ISingleId<T>, IConfigurableId, IEquatable<SingleIdConfigurable<T>>
{
    public T? Value { get; }
    object? ISingleId.Value => Value;
    public IdEqualityBehavior EqualityBehavior { get; }

    public SingleIdConfigurable(T? value, IdEqualityBehavior behavior = IdEqualityBehavior.Strict)
    {
        Value = value;
        EqualityBehavior = behavior;
    }

    public override int GetHashCode() => IdHashCodeHelper.ComputeSingleHash(Value);

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is SingleIdConfigurable<T> other) return Equals(other);

#if ENABLE_SINGLE_COMPOSITE_EQUALITY || ENABLE_CROSS_TYPE_EQUALITY
        return IdEqualityHelper.TryCrossTypeEquality(this, obj);
#else
        return false;
#endif
    }

    public bool Equals(SingleIdConfigurable<T>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return IdEqualityHelper.AreValuesEqual(Value, other.Value);
    }

    public static implicit operator SingleIdConfigurable<T>(T date)
        => new(date);
}