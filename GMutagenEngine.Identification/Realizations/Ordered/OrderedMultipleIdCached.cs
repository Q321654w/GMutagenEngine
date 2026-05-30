using GMutagenEngine.Identification.Interfaces;
using GMutagenEngine.Identification.Utils;

namespace GMutagenEngine.Identification.Realizations.Ordered;

public class OrderedMultipleIdCached : IOrderedCompositeId, IEquatable<OrderedMultipleIdCached>, IIdOptimized
{
    private readonly int _hashCode;
    public IReadOnlyList<IId> Components { get; }

    public OrderedMultipleIdCached(IReadOnlyList<IId> components)
    {
        Components = components ?? throw new ArgumentNullException(nameof(components));
        _hashCode = IdHashCodeHelper.ComputeOrderedHash(components);
    }

    public OrderedMultipleIdCached(params IId[] components) : this((IReadOnlyList<IId>)components)
    {
    }

    public override int GetHashCode() => _hashCode;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is OrderedMultipleIdCached other) return Equals(other);

#if ENABLE_SINGLE_COMPOSITE_EQUALITY || ENABLE_ORDERED_UNORDERED_EQUALITY || ENABLE_CROSS_TYPE_EQUALITY
        return IdEqualityHelper.TryCrossTypeEquality(this, obj);
#else
        return false;
#endif
    }

    public bool Equals(OrderedMultipleIdCached? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (_hashCode != other._hashCode) return false;
        return IdEqualityHelper.AreOrderedComponentsEqual(Components, other.Components);
    }
}