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
    public class CollectionSnapshotContextVisitor(ISchemaRegistry schemaRegistry, IDefaultValueFactory defaultValueFactory) : BaseSnapshotContextVisitor
    {
        public override IEnumerable<Type> CanVisitTypes() => FrameworkStandardTypes.Collections;

        public override ContextBase VisitInternal(ISchema schema,
            SnapshotContextGeneratorSettings settings,
            ContextBase context = null,
            object instance = null)
        {
            var elementMember = schema.Members.GetValueOrDefault(SchemaConstants.ELEMENT_KEY);
            var elementSchema = elementMember?.Schema;

            if (elementSchema?.TargetType == null)
                return null;

            var isPrimitive = FrameworkStandardTypes.Primitive.Contains(elementSchema.TargetType);
            var myContext = new ContextSnapshot();

            if (instance is IList collection)
            {
                // Есть instance - обрабатываем элементы
                if (isPrimitive)
                    ProcessPrimitives(collection, settings, myContext, elementSchema);
                else
                    ProcessNonPrimitive(collection, settings, myContext, elementSchema);
            }
            else
            {
                // instance == null - создаем пустую структуру по схеме
                // Для коллекций при instance == null просто возвращаем пустой контекст
                // так как мы не знаем количество элементов
            }

            return myContext;
        }

        private void ProcessNonPrimitive(IList collection, SnapshotContextGeneratorSettings settings,
            ContextSnapshot myContext, ISchema elementSchema)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                var item = collection[i];
                if (item == null)
                    continue;

                ContextBase itemContext = null;
                var itemType = item.GetType();
            
                // Определяем схему для элемента
                if (!elementSchema.TargetType.IsAbstract && !elementSchema.TargetType.IsInterface)
                {
                    itemContext = settings.Generator.Generate(elementSchema, settings, myContext, item);
                }
                else if (schemaRegistry.TryGet(itemType, out var itemSchema))
                {
                    itemContext = settings.Generator.Generate(itemSchema, settings, myContext, item);
                }
                else
                {
                    // Если не нашли схему, создаем по типу объекта
                    var fallbackSchema = new Schema { TargetType = itemType };
                    itemContext = settings.Generator.Generate(fallbackSchema, settings, myContext, item);
                }

                if (itemContext != null)
                {
                    myContext.AttachChild(i.ToString(), itemContext);
                }
            }
        }

        private void ProcessPrimitives(IList collection, SnapshotContextGeneratorSettings settings,
            ContextSnapshot myContext, ISchema elementSchema)
        {
            for (var i = 0; i < collection.Count; i++)
            {
                var item = collection[i];
                if (item == null)
                    continue;

                // Для примитивов создаем значение напрямую
                var value = defaultValueFactory.Create(item, elementSchema.TargetType);
                if (value != null)
                {
                    myContext.SetValue(i.ToString(), value);
                }
            }
        }
    }
}