using GMutagenEngine.Identification.Constants;
using GMutagenEngine.Identification.Interfaces;
using GMutagenEngine.Identification.Utils;

namespace GMutagenEngine.Identification.Realizations.Unordered;

public class UnorderedMultipleIdCachedConfigurable : IUnorderedCompositeId, IConfigurableId,
    IEquatable<UnorderedMultipleIdCachedConfigurable>, IIdOptimized
{
    private readonly int _hashCode;
    public IReadOnlySet<IId> Components { get; }
    public IdEqualityBehavior EqualityBehavior { get; }

    public UnorderedMultipleIdCachedConfigurable(IReadOnlySet<IId> components,
        IdEqualityBehavior behavior = IdEqualityBehavior.Strict)
    {
        Components = components ?? throw new ArgumentNullException(nameof(components));
        EqualityBehavior = behavior;
        _hashCode = IdHashCodeHelper.ComputeUnorderedHash(components);
    }

    public UnorderedMultipleIdCachedConfigurable(IdEqualityBehavior behavior, params IId[] components)
        : this(new HashSet<IId>(components), behavior)
    {
    }

    public override int GetHashCode() => _hashCode;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is UnorderedMultipleIdCachedConfigurable other) return Equals(other);

#if ENABLE_SINGLE_COMPOSITE_EQUALITY || ENABLE_ORDERED_UNORDERED_EQUALITY || ENABLE_CROSS_TYPE_EQUALITY
        return IdEqualityHelper.TryCrossTypeEquality(this, obj);
#else
        return false;
#endif
    }

    public bool Equals(UnorderedMultipleIdCachedConfigurable? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (_hashCode != other._hashCode) return false;
        return IdEqualityHelper.AreUnorderedComponentsEqual(Components, other.Components);
    }
}