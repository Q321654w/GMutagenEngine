using GMutagenEngine.Values.BaseClasses;

namespace GMutagenEngine.Values.Default.Realizations;

public class GuidValue(Guid? initial = null)
    : BaseValue<Guid>(initial ?? Guid.Empty), IComparable<Guid>, IComparable<GuidValue>
{
    public void Regenerate() => Value = Guid.NewGuid();

    public int CompareTo(Guid other) => Value.CompareTo(other);
    public int CompareTo(GuidValue? other) => other == null ? 1 : Value.CompareTo(other.Value);
}