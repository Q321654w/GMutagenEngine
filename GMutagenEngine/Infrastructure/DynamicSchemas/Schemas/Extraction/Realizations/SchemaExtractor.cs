using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Settings;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Visitors.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Visitors.Registries.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Interfaces;
using GMutagenEngine.Utils;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Extraction.Realizations
{
    public class SchemaExtractor(ISchemaVisitorRegistry visitors) : ISchemaExtractor
    {
        public ISchema Extract(Type type, SchemaExtractorSettings? settings = null, int depth = 0)
        {
            settings ??= new SchemaExtractorSettings(this);

            if (TryGetVisitor(type, out var visitor))
                return visitor.Visit(type, settings, depth + 1);

            throw new Exception($"Could not find any visitor for type: {type.FullName}");
        }

        private bool TryGetVisitor(Type targetType, out ISchemaVisitor visitor)
        {
            var typeDiscovery = targetType.AsTypeDiscovery();

            foreach (var type in typeDiscovery)
            {
               
                if (visitors.TryGet(type, out visitor))
                    return true;
            }

            visitor = null;
            return false;
        }
    }
}