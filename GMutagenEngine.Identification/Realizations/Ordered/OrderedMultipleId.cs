using GMutagenEngine.Identification.Interfaces;
using GMutagenEngine.Identification.Utils;

namespace GMutagenEngine.Identification.Realizations.Ordered;

public class OrderedMultipleId : IOrderedCompositeId, IEquatable<OrderedMultipleId>
{
    public IReadOnlyList<IId?> Components { get; }

    public OrderedMultipleId(IReadOnlyList<IId?> components)
    {
        Components = components ?? throw new ArgumentNullException(nameof(components));
    }

    public OrderedMultipleId(params IId?[] components) : this((IReadOnlyList<IId?>)components)
    {
    }

    public override int GetHashCode() => IdHashCodeHelper.ComputeOrderedHash(Components);

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is OrderedMultipleId other) return Equals(other);

#if ENABLE_SINGLE_COMPOSITE_EQUALITY || ENABLE_ORDERED_UNORDERED_EQUALITY || ENABLE_CROSS_TYPE_EQUALITY
        return IdEqualityHelper.TryCrossTypeEquality(this, obj);
#else
        return false;
#endif
    }

    public bool Equals(OrderedMultipleId? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return IdEqualityHelper.AreOrderedComponentsEqual(Components, other.Components);
    }
}