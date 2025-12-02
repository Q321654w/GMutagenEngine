using System.Collections;
using GMutagenEngine.Concept.Sync.Values.Factories.Default.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.BaseClases;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Snapshot.Settings;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Snapshot.Visitors.BaseClases;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Realizations.Snapshots.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Constants;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Registries.Interfaces;
using GMutagenEngine.Utils;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Snapshot.Visitors.Realizations
{
    public class DictionarySnapshotVisitor(ISchemaRegistry schemaRegistry, IDefaultValueFactory defaultValueFactory)
        : BaseSnapshotContextVisitor
    {
        public override IEnumerable<Type> CanVisitTypes() => FrameworkStandardTypes.Dictionaries;

        public override ContextBase VisitInternal(ISchema schema,
            SnapshotContextGeneratorSettings settings,
            ContextBase context = null,
            object instance = null)
        {
            var kvpMember = schema.Members.GetValueOrDefault(SchemaConstants.ELEMENT_KEY);
            if (kvpMember?.Schema == null)
                return null;
        
            var valueMember = kvpMember.Schema.Members.GetValueOrDefault(SchemaConstants.VALUE_PROPERTY);
            if (valueMember?.Schema == null)
                return null;

            var myContext = new ContextSnapshot();

            var valueSchema = valueMember.Schema;
            var valueType = valueSchema.TargetType;
            var isPrimitive = valueType != null && FrameworkStandardTypes.Primitive.Contains(valueType);

            if (instance is IDictionary dictionary)
            {
                if (!isPrimitive)
                    ProcessNonPrimitive(dictionary, valueSchema, settings, myContext);
                else
                    ProcessPrimitive(dictionary, valueSchema, settings, myContext);
            }
            else
            {
                // instance == null - создаем пустую структуру по схеме
                // Для словарей при instance == null просто возвращаем пустой контекст
            }

            return myContext;
        }

        private void ProcessPrimitive(IDictionary dictionary, ISchema valueSchema,
            SnapshotContextGeneratorSettings settings,
            ContextSnapshot myContext)
        {
            foreach (DictionaryEntry entry in dictionary)
            {
                var key = entry.Key;
                var value = entry.Value;

                if (value == null || key == null)
                    continue;

                var keyName = key.ToString();

                // Для примитивов создаем значение напрямую
                var primitiveValue = defaultValueFactory.Create(value, valueSchema.TargetType);
                if (primitiveValue != null)
                {
                    myContext.SetValue(keyName, primitiveValue);
                }
            }
        }

        private void ProcessNonPrimitive(IDictionary dictionary, ISchema valueSchema,
            SnapshotContextGeneratorSettings settings,
            ContextSnapshot myContext)
        {
            foreach (DictionaryEntry entry in dictionary)
            {
                var key = entry.Key;
                var value = entry.Value;

                if (value == null || key == null)
                    continue;

                var keyName = key.ToString();

                ContextBase contextBase = null;
                var valueType = value.GetType();
            
                // Определяем схему для значения
                if (!valueSchema.TargetType.IsAbstract && !valueSchema.TargetType.IsInterface)
                {
                    contextBase = settings.Generator.Generate(valueSchema, settings, myContext, value);
                }
                else if (schemaRegistry.TryGet(valueType, out var itemSchema))
                {
                    contextBase = settings.Generator.Generate(itemSchema, settings, myContext, value);
                }
                else
                {
                    // Если не нашли схему, создаем по типу объекта
                    var fallbackSchema = new Schema { TargetType = valueType };
                    contextBase = settings.Generator.Generate(fallbackSchema, settings, myContext, value);
                }

                if (contextBase != null)
                {
                    myContext.AttachChild(keyName, contextBase);
                }
            }
        }
    }
}