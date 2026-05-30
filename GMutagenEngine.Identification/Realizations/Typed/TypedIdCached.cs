using GMutagenEngine.Identification.Interfaces;
using GMutagenEngine.Identification.Realizations.Single;
using GMutagenEngine.Identification.Utils;

namespace GMutagenEngine.Identification.Realizations.Typed;

public class TypedIdCached<T> : IOrderedCompositeId, IEquatable<TypedIdCached<T>>, IIdOptimized
{
    private readonly int _hashCode;
    public IReadOnlyList<IId> Components { get; }
    public T? Value { get; }
    public Type Type { get; }

    public TypedIdCached(T? value, Type type)
    {
        Type = type ?? throw new ArgumentNullException(nameof(type));
        Value = value;
        Components = new IId[] { new SingleIdCached<T>(value), new SingleIdCached<Type>(type) };
        _hashCode = IdHashCodeHelper.ComputeOrderedHash(Components);
    }

    public override int GetHashCode() => _hashCode;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is TypedIdCached<T> other) return Equals(other);

#if ENABLE_SINGLE_COMPOSITE_EQUALITY || ENABLE_ORDERED_UNORDERED_EQUALITY || ENABLE_CROSS_TYPE_EQUALITY
        return IdEqualityHelper.TryCrossTypeEquality(this, obj);
#else
            return false;
#endif
    }

    public bool Equals(TypedIdCached<T>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (_hashCode != other._hashCode) return false;
        return IdEqualityHelper.AreOrderedComponentsEqual(Components, other.Components);
    }
}