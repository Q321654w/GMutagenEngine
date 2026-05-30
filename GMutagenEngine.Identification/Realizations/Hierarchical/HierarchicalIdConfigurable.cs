using GMutagenEngine.Identification.Constants;
using GMutagenEngine.Identification.Interfaces;
using GMutagenEngine.Identification.Realizations.Single;
using GMutagenEngine.Identification.Utils;

namespace GMutagenEngine.Identification.Realizations.Hierarchical;

public class HierarchicalIdConfigurable<T> : IOrderedCompositeId, IConfigurableId,
    IEquatable<HierarchicalIdConfigurable<T>>
{
    public IReadOnlyList<IId> Components { get; }
    public T[] Parts { get; }
    public IdEqualityBehavior EqualityBehavior { get; }

    public HierarchicalIdConfigurable(T[] parts,
        IdEqualityBehavior behavior = IdEqualityBehavior.Strict)
    {
        Parts = parts ?? throw new ArgumentNullException(nameof(parts));
        EqualityBehavior = behavior;
        Components = parts.Select(p => (IId)new SingleId<T>(p)).ToArray();
    }

    public override int GetHashCode() => IdHashCodeHelper.ComputeOrderedHash(Components);

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is HierarchicalIdConfigurable<T> other) return Equals(other);

#if ENABLE_SINGLE_COMPOSITE_EQUALITY || ENABLE_ORDERED_UNORDERED_EQUALITY || ENABLE_CROSS_TYPE_EQUALITY
        return IdEqualityHelper.TryCrossTypeEquality(this, obj);
#else
            return false;
#endif
    }

    public bool Equals(HierarchicalIdConfigurable<T>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return IdEqualityHelper.AreOrderedComponentsEqual(Components, other.Components);
    }
}