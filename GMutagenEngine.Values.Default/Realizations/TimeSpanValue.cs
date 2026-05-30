using GMutagenEngine.Values.Arithmetic.Interfaces;
using GMutagenEngine.Values.BaseClasses;

namespace GMutagenEngine.Values.Default.Realizations;

public class TimeSpanValue(TimeSpan? initial = null) : BaseValue<TimeSpan>(initial ?? TimeSpan.Zero),
    IAdd<TimeSpan>, IAdd<TimeSpanValue>,
    ISubtract<TimeSpan>, ISubtract<TimeSpanValue>,
    IComparable<TimeSpan>, IComparable<TimeSpanValue>
{
    public void Add(TimeSpan delta) => Value = Value.Add(delta);
    public void Add(TimeSpanValue delta) => Value = Value.Add(delta.Value);
    public void Subtract(TimeSpan delta) => Value = Value.Subtract(delta);
    public void Subtract(TimeSpanValue delta) => Value = Value.Subtract(delta.Value);

    public static TimeSpanValue operator +(TimeSpanValue left, TimeSpan right)
    {
        left.Value += right;
        return left;
    }

    public static TimeSpanValue operator +(TimeSpanValue left, TimeSpanValue right)
    {
        left.Value += right.Value;
        return left;
    }

    public static TimeSpanValue operator -(TimeSpanValue left, TimeSpan right)
    {
        left.Value -= right;
        return left;
    }

    public static TimeSpanValue operator -(TimeSpanValue left, TimeSpanValue right)
    {
        left.Value -= right.Value;
        return left;
    }

    public int CompareTo(TimeSpan other) => Value.CompareTo(other);
    public int CompareTo(TimeSpanValue? other) => other == null ? 1 : Value.CompareTo(other.Value);
}