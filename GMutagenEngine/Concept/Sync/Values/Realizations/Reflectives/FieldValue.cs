using System.Reflection;
using GMutagenEngine.Concept.Sync.Values.Interfaces;
using GMutagenEngine.Utils;

namespace GMutagenEngine.Concept.Sync.Values.Realizations.Reflectives
{
    public class FieldValue<T>(object instance, FieldInfo fieldInfo) : IValue<T>
    {
        public T Value
        {
            get { return (T)fieldInfo.GetValue(instance); }
            set { fieldInfo.SetValue(instance, value); }
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