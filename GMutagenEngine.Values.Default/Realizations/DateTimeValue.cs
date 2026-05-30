using GMutagenEngine.Values.Arithmetic.Interfaces;
using GMutagenEngine.Values.BaseClasses;

namespace GMutagenEngine.Values.Default.Realizations;

public class DateTimeValue(DateTime? initial = null) : BaseValue<DateTime>(initial ?? DateTime.Now),
    IAdd<TimeSpan>, ISubtract<TimeSpan>,
    IComparable<DateTime>, IComparable<DateTimeValue>
{
    public void Add(TimeSpan delta) => Value = Value.Add(delta);
    public void Subtract(TimeSpan delta) => Value = Value.Subtract(delta);

    public void AddDays(double days) => Value = Value.AddDays(days);
    public void AddHours(double hours) => Value = Value.AddHours(hours);
    public void AddMinutes(double minutes) => Value = Value.AddMinutes(minutes);

    public int CompareTo(DateTime other) => Value.CompareTo(other);
    public int CompareTo(DateTimeValue? other) => other == null ? 1 : Value.CompareTo(other.Value);
}