using GMutagenEngine.Concept.Sync.Values.Arithmetic;
using GMutagenEngine.Concept.Sync.Values.BaseClases;

namespace GMutagenEngine.Concept.Sync.Values.Realizations.Default
{
    public class StringValue(string initial = "") : BaseValue<string>(initial),
        IAdd<string>, IAdd<StringValue>,
        IComparable<string>, IComparable<StringValue>
    {
        public void Add(string delta) => Value += delta;
        public void Add(StringValue delta) => Value += delta.Value;

        public static StringValue operator +(StringValue left, string right)
        {
            left.Value += right;
            return left;
        }

        public static StringValue operator +(StringValue left, StringValue right)
        {
            left.Value += right.Value;
            return left;
        }

        public int CompareTo(string? other) => string.Compare(Value, other, StringComparison.Ordinal);
        public int CompareTo(StringValue? other) => string.Compare(Value, other?.Value, StringComparison.Ordinal);
    }
}