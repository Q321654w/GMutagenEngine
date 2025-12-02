using GMutagenEngine.Infrastructure.Identification.Interfaces;
using GMutagenEngine.Infrastructure.Identification.Utils;

namespace GMutagenEngine.Infrastructure.Identification.Realizations.Unordered;

public class UnorderedMultipleIdCached : IUnorderedCompositeId, IEquatable<UnorderedMultipleIdCached>,
    IIdOptimized
{
    private readonly int _hashCode;
    public IReadOnlySet<IId> Components { get; }

    public UnorderedMultipleIdCached(IReadOnlySet<IId> components)
    {
        Components = components ?? throw new ArgumentNullException(nameof(components));
        _hashCode = IdHashCodeHelper.ComputeUnorderedHash(components);
    }

    public UnorderedMultipleIdCached(params IId[] components)
        : this(new HashSet<IId>(components))
    {
    }

    public override int GetHashCode() => _hashCode;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is UnorderedMultipleIdCached other) return Equals(other);

#if ENABLE_SINGLE_COMPOSITE_EQUALITY || ENABLE_ORDERED_UNORDERED_EQUALITY || ENABLE_CROSS_TYPE_EQUALITY
        return IdEqualityHelper.TryCrossTypeEquality(this, obj);
#else
        return false;
#endif
    }

    public bool Equals(UnorderedMultipleIdCached? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (_hashCode != other._hashCode) return false;
        return IdEqualityHelper.AreUnorderedComponentsEqual(Components, other.Components);
    }
}