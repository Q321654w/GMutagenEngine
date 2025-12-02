using System.Collections;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.BaseClases;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Settings;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Visitors.BaseClases;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Realizations.Live.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Registries.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Constants;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Registries.Interfaces;
using GMutagenEngine.Utils;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Visitors.Realizations
{
    public class DictionaryLiveVisitor(IContextRegistry registry, ISchemaRegistry schemaRegistry) : BaseLiveContextVisitor
    {
        public override IEnumerable<Type> CanVisitTypes() => FrameworkStandardTypes.Dictionaries;

        public override ContextBase VisitInternal(ISchema schema,
            LiveContextGeneratorSettings settings,
            ContextBase context = null,
            object instance = null)
        {
            if (instance is not IDictionary dictionary)
                return null;

            var kvpMember = schema.Members.GetValueOrDefault(SchemaConstants.ELEMENT_KEY);
            if (kvpMember?.Schema == null)
                return null;

            var valueMember = kvpMember.Schema.Members.GetValueOrDefault(SchemaConstants.VALUE_PROPERTY);
            if (valueMember?.Schema == null)
                return null;
        
            var keyMember = kvpMember.Schema.Members.GetValueOrDefault(SchemaConstants.KEY_PROPERTY);
            if (keyMember?.Schema == null)
                return null;

            var valueSchema = valueMember.Schema;
            var keySchema = keyMember.Schema;
            var valueType = valueSchema.TargetType;
            var isPrimitive = valueType != null && FrameworkStandardTypes.Primitive.Contains(valueType);
        
            var myContext = new LiveDictionaryContext(dictionary, valueSchema, keySchema, registry, isPrimitive);
        
            registry.Add(instance, myContext);

            if (!isPrimitive)
                ProcessNonPrimitive(dictionary, valueSchema, valueType, settings, myContext, schemaRegistry);

            return myContext;
        }

        private void ProcessNonPrimitive(IDictionary dictionary, ISchema valueSchema, Type valueType, 
            LiveContextGeneratorSettings settings, LiveDictionaryContext myContext, ISchemaRegistry schemaRegistry)
        {
            foreach (DictionaryEntry entry in dictionary)
            {
                if (entry.Value == null)
                    continue;

                var item = entry.Value;
            
                ISchema itemSchema;
                if (!valueType.IsAbstract && !valueType.IsInterface)
                {
                    itemSchema = valueSchema;
                }
                else if (schemaRegistry.TryGet(item.GetType(), out var concreteSchema))
                {
                    itemSchema = concreteSchema;
                }
                else
                {
                    continue;
                }
            
                var itemContext = settings.Generator.Generate(itemSchema, settings, myContext, item);
            }
        }
    }
}