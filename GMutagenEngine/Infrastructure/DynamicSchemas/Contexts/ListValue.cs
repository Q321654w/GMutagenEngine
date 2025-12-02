using System.Collections;
using GMutagenEngine.Concept.Sync.Values.Interfaces;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Contexts
{
    public class ListValue(IList list, int result, Type? type) : IValue
    {
        public object Value
        {
            get { return list[result]; }
            set { list[result] = value; }
        }

        public Type ValueType => type;
    }
}