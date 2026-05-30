using GMutagenEngine.Identification.Interfaces;
using GMutagenEngine.Identification.Realizations.Single;
using GMutagenEngine.Identification.Utils;

namespace GMutagenEngine.Identification.Realizations.Temporal;

public class TemporalId<T> : IOrderedCompositeId, IEquatable<TemporalId<T>>
{
    public IReadOnlyList<IId> Components { get; }
    public T? Value { get; }
    public DateTime Start { get; }
    public DateTime End { get; }

    public TemporalId(T? value, DateTime start, DateTime end)
    {
        Value = value;
        Start = start;
        End = end;
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
        if (obj is TemporalId<T> other) return Equals(other);

#if ENABLE_SINGLE_COMPOSITE_EQUALITY || ENABLE_ORDERED_UNORDERED_EQUALITY || ENABLE_CROSS_TYPE_EQUALITY
        return IdEqualityHelper.TryCrossTypeEquality(this, obj);
#else
            return false;
#endif
    }

    public bool Equals(TemporalId<T>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return IdEqualityHelper.AreOrderedComponentsEqual(Components, other.Components);
    }
}