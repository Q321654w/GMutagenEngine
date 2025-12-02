using System.Collections;
using GMutagenEngine.Concept.Sync.Values.Interfaces;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Contexts
{
    public class DictionaryValue(IDictionary dictionary, string result, Type? type) : IValue
    {
        public object? Value
        {
            get { return dictionary[result]; }
            set { dictionary[result] = value; }
        }

        public Type ValueType => type;
    }
}