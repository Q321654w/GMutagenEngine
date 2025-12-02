using GMutagenEngine.Infrastructure.Identification.Constants;
using GMutagenEngine.Infrastructure.Identification.Interfaces;
using GMutagenEngine.Infrastructure.Identification.Utils;

namespace GMutagenEngine.Infrastructure.Identification.Realizations.Ordered;

public class OrderedMultipleIdCachedConfigurable : IOrderedCompositeId, IConfigurableId,
    IEquatable<OrderedMultipleIdCachedConfigurable>, IIdOptimized
{
    private readonly int _hashCode;
    public IReadOnlyList<IId> Components { get; }
    public IdEqualityBehavior EqualityBehavior { get; }

    public OrderedMultipleIdCachedConfigurable(IReadOnlyList<IId> components,
        IdEqualityBehavior behavior = IdEqualityBehavior.Strict)
    {
        Components = components ?? throw new ArgumentNullException(nameof(components));
        EqualityBehavior = behavior;
        _hashCode = IdHashCodeHelper.ComputeOrderedHash(components);
    }

    public OrderedMultipleIdCachedConfigurable(IdEqualityBehavior behavior, params IId[] components)
        : this((IReadOnlyList<IId>)components, behavior)
    {
    }

    public override int GetHashCode() => _hashCode;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is OrderedMultipleIdCachedConfigurable other) return Equals(other);

#if ENABLE_SINGLE_COMPOSITE_EQUALITY || ENABLE_ORDERED_UNORDERED_EQUALITY || ENABLE_CROSS_TYPE_EQUALITY
        return IdEqualityHelper.TryCrossTypeEquality(this, obj);
#else
        return false;
#endif
    }

    public bool Equals(OrderedMultipleIdCachedConfigurable? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (_hashCode != other._hashCode) return false;
        return IdEqualityHelper.AreOrderedComponentsEqual(Components, other.Components);
    }
}