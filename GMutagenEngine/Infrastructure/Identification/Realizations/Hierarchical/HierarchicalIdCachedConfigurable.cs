using GMutagenEngine.Infrastructure.Identification.Constants;
using GMutagenEngine.Infrastructure.Identification.Interfaces;
using GMutagenEngine.Infrastructure.Identification.Realizations.Single;
using GMutagenEngine.Infrastructure.Identification.Utils;

namespace GMutagenEngine.Infrastructure.Identification.Realizations.Hierarchical;

public class HierarchicalIdCachedConfigurable<T> : IOrderedCompositeId, IConfigurableId,
    IEquatable<HierarchicalIdCachedConfigurable<T>>, IIdOptimized
{
    private readonly int _hashCode;
    public IReadOnlyList<IId> Components { get; }
    public T[] Parts { get; }
    public IdEqualityBehavior EqualityBehavior { get; }

    public HierarchicalIdCachedConfigurable(T[] parts,
        IdEqualityBehavior behavior = IdEqualityBehavior.Strict)
    {
        Parts = parts ?? throw new ArgumentNullException(nameof(parts));
        EqualityBehavior = behavior;
        Components = parts.Select(p => (IId)new SingleIdCached<T>(p)).ToArray();
        _hashCode = IdHashCodeHelper.ComputeOrderedHash(Components);
    }

    public override int GetHashCode() => _hashCode;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is HierarchicalIdCachedConfigurable<T> other) return Equals(other);

#if ENABLE_SINGLE_COMPOSITE_EQUALITY || ENABLE_ORDERED_UNORDERED_EQUALITY || ENABLE_CROSS_TYPE_EQUALITY
        return IdEqualityHelper.TryCrossTypeEquality(this, obj);
#else
            return false;
#endif
    }

    public bool Equals(HierarchicalIdCachedConfigurable<T>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (_hashCode != other._hashCode) return false;
        return IdEqualityHelper.AreOrderedComponentsEqual(Components, other.Components);
    }
}