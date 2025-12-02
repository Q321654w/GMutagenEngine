using GMutagenEngine.Infrastructure.Identification.Constants;
using GMutagenEngine.Infrastructure.Identification.Interfaces;
using GMutagenEngine.Infrastructure.Identification.Realizations.Single;
using GMutagenEngine.Infrastructure.Identification.Utils;

namespace GMutagenEngine.Infrastructure.Identification.Realizations.Temporal;

public class TemporalIdConfigurable<T> : IOrderedCompositeId, IConfigurableId,
    IEquatable<TemporalIdConfigurable<T>>
{
    public IReadOnlyList<IId> Components { get; }
    public T? Value { get; }
    public DateTime Start { get; }
    public DateTime End { get; }
    public IdEqualityBehavior EqualityBehavior { get; }

    public TemporalIdConfigurable(T? value, DateTime start, DateTime end,
        IdEqualityBehavior behavior = IdEqualityBehavior.Strict)
    {
        Value = value;
        Start = start;
        End = end;
        EqualityBehavior = behavior;
        Components = new IId[]
        {
            new SingleId<T>(value),
            new SingleId<DateTime>(start),
            new SingleId<DateTime>(end)
        };
    }

    public override int GetHashCode() => IdHashCodeHelper.ComputeOrderedHash(Components);

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is TemporalIdConfigurable<T> other) return Equals(other);

#if ENABLE_SINGLE_COMPOSITE_EQUALITY || ENABLE_ORDERED_UNORDERED_EQUALITY || ENABLE_CROSS_TYPE_EQUALITY
        return IdEqualityHelper.TryCrossTypeEquality(this, obj);
#else
            return false;
#endif
    }

    public bool Equals(TemporalIdConfigurable<T>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return IdEqualityHelper.AreOrderedComponentsEqual(Components, other.Components);
    }
}