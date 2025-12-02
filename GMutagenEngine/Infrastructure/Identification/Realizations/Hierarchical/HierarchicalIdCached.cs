using GMutagenEngine.Infrastructure.Identification.Interfaces;
using GMutagenEngine.Infrastructure.Identification.Realizations.Single;
using GMutagenEngine.Infrastructure.Identification.Utils;

namespace GMutagenEngine.Infrastructure.Identification.Realizations.Hierarchical;

public class HierarchicalIdCached<T> : IOrderedCompositeId, IEquatable<HierarchicalIdCached<T>>, IIdOptimized
{
    private readonly int _hashCode;
    public IReadOnlyList<IId> Components { get; }
    public T[] Parts { get; }

    public HierarchicalIdCached(T[] parts)
    {
        Parts = parts ?? throw new ArgumentNullException(nameof(parts));
        Components = parts.Select(p => (IId)new SingleIdCached<T>(p)).ToArray();
        _hashCode = IdHashCodeHelper.ComputeOrderedHash(Components);
    }

    public override int GetHashCode() => _hashCode;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is HierarchicalIdCached<T> other) return Equals(other);

#if ENABLE_SINGLE_COMPOSITE_EQUALITY || ENABLE_ORDERED_UNORDERED_EQUALITY || ENABLE_CROSS_TYPE_EQUALITY
        return IdEqualityHelper.TryCrossTypeEquality(this, obj);
#else
            return false;
#endif
    }

    public bool Equals(HierarchicalIdCached<T>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (_hashCode != other._hashCode) return false;
        return IdEqualityHelper.AreOrderedComponentsEqual(Components, other.Components);
    }
}