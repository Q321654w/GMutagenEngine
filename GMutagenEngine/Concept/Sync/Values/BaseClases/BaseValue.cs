using GMutagenEngine.Concept.Sync.Values.Interfaces;
using GMutagenEngine.Utils;

namespace GMutagenEngine.Concept.Sync.Values.BaseClases
{
    public abstract class BaseValue<T>(T initialValue) : IValue<T>
    {
        public BaseValue() : this(default!)
        {
        }

        public T Value { get; set; } = initialValue;
        public Type ValueType => typeof(T);

        object IValue.Value
        {
            get => Value!;
            set => Value = (T)value;
        }

        public override string ToString() => Value?.ToString() ?? Literals.NULL;
    }
}