using GMutagenEngine.Concept.Sync.Values.Factories.Default.Interfaces;
using GMutagenEngine.Concept.Sync.Values.Interfaces;
using GMutagenEngine.Concept.Sync.Values.Realizations.Default;

namespace GMutagenEngine.Concept.Sync.Values.Factories.Default.Realizations
{
    public class DefaultValueFactory : IDefaultValueFactory
    {
        public IValue Create(object? value, Type type)
        {
            if (type == typeof(int)) return new IntValue((int)value);
            if (type == typeof(long)) return new LongValue((long)value);
            if (type == typeof(float)) return new FloatValue((float)value);
            if (type == typeof(double)) return new DoubleValue((double)value);
            if (type == typeof(decimal)) return new DecimalValue((decimal)value);
            if (type == typeof(string)) return new StringValue((string)value);
            if (type == typeof(bool)) return new BoolValue((bool)value);
            if (type == typeof(Guid)) return new GuidValue((Guid)value);
            if (type == typeof(DateTime)) return new DateTimeValue((DateTime)value);
            if (type == typeof(TimeSpan)) return new TimeSpanValue((TimeSpan)value);

            throw new NotSupportedException($"Value of type {type.Name} is not supported.");
        }
    }
}