using Constants.Literals;
using GMutagenEngine.Values.Interfaces;

namespace GMutagenEngine.Values.Lambda.Realizations;

public class LambdaValue<T>(Func<T> get, Action<T> set) : IValue<T>
{
    public T Value
    {
        get => get();
        set => set(value);
    }

    public Type ValueType => typeof(T);

    public override string ToString() => Value.ToString() ?? Literals.Keywords.NULL_STRING;

    object IValue.Value
    {
        get => Value;
        set => Value = (T)value;
    }
}