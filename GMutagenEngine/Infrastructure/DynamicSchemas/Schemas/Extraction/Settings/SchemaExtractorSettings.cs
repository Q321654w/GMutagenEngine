using System.Reflection;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Realizations;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Settings
{
    public class SchemaExtractorSettings(SchemaExtractor extractor)
    {
        public SchemaExtractor Extractor { get; } = extractor;


        public readonly HashSet<Type> ExcludedTypes = new();

        public Func<Type, bool>? ExcludedTypePredicate { get; set; }

        public BindingFlags PropertiesBindingFlags { get; set; } = BindingFlags.Public | BindingFlags.Instance;
        public BindingFlags FieldsBindingFlags { get; set; } = BindingFlags.Public | BindingFlags.Instance;
        public int MaxDepth { get; set; } = 100;


        public bool ShouldHandle(Type type)
        {
            if (ExcludedTypePredicate?.Invoke(type) == true)
                return false;

            return !ExcludedTypes.Contains(type);
        }
    }
}