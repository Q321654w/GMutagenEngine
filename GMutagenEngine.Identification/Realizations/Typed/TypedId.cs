using GMutagenEngine.Identification.Interfaces;
using GMutagenEngine.Identification.Realizations.Single;
using GMutagenEngine.Identification.Utils;

namespace GMutagenEngine.Identification.Realizations.Typed;

public class TypedId<T> : IOrderedCompositeId, IEquatable<TypedId<T>>
{
    public IReadOnlyList<IId> Components { get; }
    public T? Value { get; }
    public Type Type { get; }

    public TypedId(T? value, Type type)
    {
        Type = type ?? throw new ArgumentNullException(nameof(type));
        Value = value;
        Components = new IId[] { new SingleId<T>(value), new SingleId<Type>(type) };
    }

    public override int GetHashCode() => IdHashCodeHelper.ComputeOrderedHash(Components);

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is TypedId<T> other) return Equals(other);

#if ENABLE_SINGLE_COMPOSITE_EQUALITY || ENABLE_ORDERED_UNORDERED_EQUALITY || ENABLE_CROSS_TYPE_EQUALITY
        return IdEqualityHelper.TryCrossTypeEquality(this, obj);
#else
            return false;
#endif
    }

    public bool Equals(TypedId<T>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return IdEqualityHelper.AreOrderedComponentsEqual(Components, other.Components);
    }
}