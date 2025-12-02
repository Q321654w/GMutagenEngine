using GMutagenEngine.Infrastructure.Identification.Constants;
using GMutagenEngine.Infrastructure.Identification.Interfaces;
using GMutagenEngine.Infrastructure.Identification.Utils;

namespace GMutagenEngine.Infrastructure.Identification.Realizations.Ordered;

public class OrderedMultipleIdConfigurable : IOrderedCompositeId, IConfigurableId,
    IEquatable<OrderedMultipleIdConfigurable>
{
    public IReadOnlyList<IId> Components { get; }
    public IdEqualityBehavior EqualityBehavior { get; }

    public OrderedMultipleIdConfigurable(IReadOnlyList<IId> components,
        IdEqualityBehavior behavior = IdEqualityBehavior.Strict)
    {
        Components = components ?? throw new ArgumentNullException(nameof(components));
        EqualityBehavior = behavior;
    }

    public OrderedMultipleIdConfigurable(IdEqualityBehavior behavior, params IId[] components)
        : this((IReadOnlyList<IId>)components, behavior)
    {
    }

    public override int GetHashCode() => IdHashCodeHelper.ComputeOrderedHash(Components);

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is OrderedMultipleIdConfigurable other) return Equals(other);

#if ENABLE_SINGLE_COMPOSITE_EQUALITY || ENABLE_ORDERED_UNORDERED_EQUALITY || ENABLE_CROSS_TYPE_EQUALITY
        return IdEqualityHelper.TryCrossTypeEquality(this, obj);
#else
        return false;
#endif
    }

    public bool Equals(OrderedMultipleIdConfigurable? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return IdEqualityHelper.AreOrderedComponentsEqual(Components, other.Components);
    }
}