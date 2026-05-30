using GMutagenEngine.Identification.Interfaces;
using GMutagenEngine.Identification.Utils;

namespace GMutagenEngine.Identification.Realizations.Single;

public class SingleId<T> : ISingleId<T>, IEquatable<SingleId<T>>
{
    public T? Value { get; set; }
    object? ISingleId.Value => Value;

    public SingleId(T? value) => Value = value;

    public override int GetHashCode() => IdHashCodeHelper.ComputeSingleHash(Value);

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is SingleId<T> other) return Equals(other);

#if ENABLE_SINGLE_COMPOSITE_EQUALITY || ENABLE_CROSS_TYPE_EQUALITY
        return IdEqualityHelper.TryCrossTypeEquality(this, obj);
#else
        return false;
#endif
    }

    public bool Equals(SingleId<T>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return IdEqualityHelper.AreValuesEqual(Value, other.Value);
    }

    public static implicit operator SingleId<T>(T date)
        => new(date);
}