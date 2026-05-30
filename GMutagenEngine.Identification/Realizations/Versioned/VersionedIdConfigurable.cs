using GMutagenEngine.Identification.Constants;
using GMutagenEngine.Identification.Interfaces;
using GMutagenEngine.Identification.Realizations.Single;
using GMutagenEngine.Identification.Utils;

namespace GMutagenEngine.Identification.Realizations.Versioned;

public class VersionedIdConfigurable<T> : IOrderedCompositeId, IConfigurableId,
    IEquatable<VersionedIdConfigurable<T>>
{
    public IReadOnlyList<IId> Components { get; }
    public T? Value { get; }
    public int Version { get; }
    public IdEqualityBehavior EqualityBehavior { get; }

    public VersionedIdConfigurable(T? value, int version,
        IdEqualityBehavior behavior = IdEqualityBehavior.Strict)
    {
        Value = value;
        Version = version;
        EqualityBehavior = behavior;
        Components = new IId[] { new SingleId<T>(value), new SingleId<int>(version) };
    }

    public override int GetHashCode() => IdHashCodeHelper.ComputeOrderedHash(Components);

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is VersionedIdConfigurable<T> other) return Equals(other);

#if ENABLE_SINGLE_COMPOSITE_EQUALITY || ENABLE_ORDERED_UNORDERED_EQUALITY || ENABLE_CROSS_TYPE_EQUALITY
        return IdEqualityHelper.TryCrossTypeEquality(this, obj);
#else
            return false;
#endif
    }

    public bool Equals(VersionedIdConfigurable<T>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return IdEqualityHelper.AreOrderedComponentsEqual(Components, other.Components);
    }
}