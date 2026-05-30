using GMutagenEngine.Identification.Interfaces;
using GMutagenEngine.Identification.Utils;

namespace GMutagenEngine.Identification.Realizations.Unordered;

public class UnorderedMultipleId : IUnorderedCompositeId, IEquatable<UnorderedMultipleId>
{
    public IReadOnlySet<IId> Components { get; }

    public UnorderedMultipleId(IReadOnlySet<IId> components)
    {
        Components = components ?? throw new ArgumentNullException(nameof(components));
    }

    public UnorderedMultipleId(params IId[] components)
        : this(new HashSet<IId>(components))
    {
    }

    public override int GetHashCode() => IdHashCodeHelper.ComputeUnorderedHash(Components);

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is UnorderedMultipleId other) return Equals(other);

#if ENABLE_SINGLE_COMPOSITE_EQUALITY || ENABLE_ORDERED_UNORDERED_EQUALITY || ENABLE_CROSS_TYPE_EQUALITY
        return IdEqualityHelper.TryCrossTypeEquality(this, obj);
#else
        return false;
#endif
    }

    public bool Equals(UnorderedMultipleId? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return IdEqualityHelper.AreUnorderedComponentsEqual(Components, other.Components);
    }
}