using GMutagenEngine.Concept.Sync.Values.Interfaces;
using GMutagenEngine.Utils;

namespace GMutagenEngine.Concept.Sync.Values.Realizations.Lambdas
{
    public class LambdaValue<T>(Func<T> get, Action<T> set) : IValue<T>
    {
        public T Value
        {
            get => get();
            set => set(value);
        }

        public Type ValueType => typeof(T);

        public override string ToString() => Value.ToString() ?? Literals.NULL;

        object IValue.Value
        {
            get => Value;
            set => Value = (T)value;
        }
    }
}