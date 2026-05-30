using GMutagenEngine.Values.BaseClasses;

namespace GMutagenEngine.Values.Default.Realizations;

public class BoolValue(bool initial = false) : BaseValue<bool>(initial), IComparable<bool>, IComparable<BoolValue>
{
    public void Toggle() => Value = !Value;
    public void SetTrue() => Value = true;
    public void SetFalse() => Value = false;

    public static BoolValue operator !(BoolValue value)
    {
        value.Value = !value.Value;
        return value;
    }

    public int CompareTo(bool other) => Value.CompareTo(other);
    public int CompareTo(BoolValue? other) => other == null ? 1 : Value.CompareTo(other.Value);
}