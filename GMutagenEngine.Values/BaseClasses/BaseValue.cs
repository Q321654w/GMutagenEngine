using Constants.Literals;
using GMutagenEngine.Values.Interfaces;

namespace GMutagenEngine.Values.BaseClasses;

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

    public override string ToString() => Value?.ToString() ?? Literals.Keywords.NULL_STRING;
}