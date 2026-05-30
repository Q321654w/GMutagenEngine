using System.Reflection;
using Constants.Literals;
using GMutagenEngine.Values.Interfaces;

namespace GMutagenEngine.Values.Reflective.Realizations;

public class FieldValue<T>(object instance, FieldInfo fieldInfo) : IValue<T>
{
    public T Value
    {
        get { return (T)fieldInfo.GetValue(instance); }
        set { fieldInfo.SetValue(instance, value); }
    }

    public Type ValueType => typeof(T);

    public override string ToString() => Value.ToString() ?? Literals.Keywords.NULL_STRING;

    object IValue.Value
    {
        get => Value;
        set => Value = (T)value;
    }
}