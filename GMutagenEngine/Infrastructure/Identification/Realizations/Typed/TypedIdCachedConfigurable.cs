using GMutagenEngine.Infrastructure.Identification.Constants;
using GMutagenEngine.Infrastructure.Identification.Interfaces;
using GMutagenEngine.Infrastructure.Identification.Realizations.Single;
using GMutagenEngine.Infrastructure.Identification.Utils;

namespace GMutagenEngine.Infrastructure.Identification.Realizations.Typed;

public class TypedIdCachedConfigurable<T> : IOrderedCompositeId, IConfigurableId,
    IEquatable<TypedIdCachedConfigurable<T>>, IIdOptimized
{
    private readonly int _hashCode;
    public IReadOnlyList<IId> Components { get; }
    public T? Value { get; }
    public Type Type { get; }
    public IdEqualityBehavior EqualityBehavior { get; }

    public TypedIdCachedConfigurable(T? value, Type type,
        IdEqualityBehavior behavior = IdEqualityBehavior.Strict)
    {
        Type = type ?? throw new ArgumentNullException(nameof(type));
        Value = value;
        EqualityBehavior = behavior;
        Components = new IId[] { new SingleIdCached<T>(value), new SingleIdCached<Type>(type) };
        _hashCode = IdHashCodeHelper.ComputeOrderedHash(Components);
    }

    public override int GetHashCode() => _hashCode;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is TypedIdCachedConfigurable<T> other) return Equals(other);

#if ENABLE_SINGLE_COMPOSITE_EQUALITY || ENABLE_ORDERED_UNORDERED_EQUALITY || ENABLE_CROSS_TYPE_EQUALITY
        return IdEqualityHelper.TryCrossTypeEquality(this, obj);
#else
            return false;
#endif
    }

    public bool Equals(TypedIdCachedConfigurable<T>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (_hashCode != other._hashCode) return false;
        return IdEqualityHelper.AreOrderedComponentsEqual(Components, other.Components);
    }
}