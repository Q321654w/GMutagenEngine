using GMutagenEngine.Infrastructure.Identification.Interfaces;
using GMutagenEngine.Infrastructure.Identification.Realizations.Single;
using GMutagenEngine.Infrastructure.Identification.Utils;

namespace GMutagenEngine.Infrastructure.Identification.Realizations.Role;

public class RoleIdCached<T> : IOrderedCompositeId, IEquatable<RoleIdCached<T>>, IIdOptimized
{
    private readonly int _hashCode;
    public IReadOnlyList<IId> Components { get; }
    public T? Value { get; }
    public string Role { get; }

    public RoleIdCached(T? value, string role)
    {
        Role = role ?? throw new ArgumentNullException(nameof(role));
        Value = value;
        Components = new IId[] { new SingleIdCached<T>(value), new SingleIdCached<string>(role) };
        _hashCode = IdHashCodeHelper.ComputeOrderedHash(Components);
    }

    public override int GetHashCode() => _hashCode;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is RoleIdCached<T> other) return Equals(other);

#if ENABLE_SINGLE_COMPOSITE_EQUALITY || ENABLE_ORDERED_UNORDERED_EQUALITY || ENABLE_CROSS_TYPE_EQUALITY
        return IdEqualityHelper.TryCrossTypeEquality(this, obj);
#else
            return false;
#endif
    }

    public bool Equals(RoleIdCached<T>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (_hashCode != other._hashCode) return false;
        return IdEqualityHelper.AreOrderedComponentsEqual(Components, other.Components);
    }
}