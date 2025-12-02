using GMutagenEngine.Infrastructure.Identification.Interfaces;
using GMutagenEngine.Infrastructure.Identification.Realizations.Single;
using GMutagenEngine.Infrastructure.Identification.Utils;

namespace GMutagenEngine.Infrastructure.Identification.Realizations.Role;

public class RoleId<T> : IOrderedCompositeId, IEquatable<RoleId<T>>
{
    public IReadOnlyList<IId> Components { get; }
    public T? Value { get; }
    public string Role { get; }

    public RoleId(T? value, string role)
    {
        Role = role ?? throw new ArgumentNullException(nameof(role));
        Value = value;
        Components = new IId[] { new SingleId<T>(value), new SingleId<string>(role) };
    }

    public override int GetHashCode() => IdHashCodeHelper.ComputeOrderedHash(Components);

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is RoleId<T> other) return Equals(other);

#if ENABLE_SINGLE_COMPOSITE_EQUALITY || ENABLE_ORDERED_UNORDERED_EQUALITY || ENABLE_CROSS_TYPE_EQUALITY
        return IdEqualityHelper.TryCrossTypeEquality(this, obj);
#else
            return false;
#endif
    }

    public bool Equals(RoleId<T>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return IdEqualityHelper.AreOrderedComponentsEqual(Components, other.Components);
    }
}