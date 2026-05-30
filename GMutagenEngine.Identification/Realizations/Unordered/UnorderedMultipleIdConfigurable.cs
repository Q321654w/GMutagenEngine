using GMutagenEngine.Identification.Constants;
using GMutagenEngine.Identification.Interfaces;
using GMutagenEngine.Identification.Utils;

namespace GMutagenEngine.Identification.Realizations.Unordered;

public class UnorderedMultipleIdConfigurable : IUnorderedCompositeId, IConfigurableId,
    IEquatable<UnorderedMultipleIdConfigurable>
{
    public IReadOnlySet<IId> Components { get; }
    public IdEqualityBehavior EqualityBehavior { get; }

    public UnorderedMultipleIdConfigurable(IReadOnlySet<IId> components,
        IdEqualityBehavior behavior = IdEqualityBehavior.Strict)
    {
        Components = components ?? throw new ArgumentNullException(nameof(components));
        EqualityBehavior = behavior;
    }

    public UnorderedMultipleIdConfigurable(IdEqualityBehavior behavior, params IId[] components)
        : this(new HashSet<IId>(components), behavior)
    {
    }

    public override int GetHashCode() => IdHashCodeHelper.ComputeUnorderedHash(Components);

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is UnorderedMultipleIdConfigurable other) return Equals(other);

#if ENABLE_SINGLE_COMPOSITE_EQUALITY || ENABLE_ORDERED_UNORDERED_EQUALITY || ENABLE_CROSS_TYPE_EQUALITY
        return IdEqualityHelper.TryCrossTypeEquality(this, obj);
#else
        return false;
#endif
    }

    public bool Equals(UnorderedMultipleIdConfigurable? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return IdEqualityHelper.AreUnorderedComponentsEqual(Components, other.Components);
    }
}