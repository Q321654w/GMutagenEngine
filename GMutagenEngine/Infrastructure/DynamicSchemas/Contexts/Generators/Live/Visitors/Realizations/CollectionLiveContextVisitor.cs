using System.Collections;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.BaseClases;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Settings;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Visitors.BaseClases;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Realizations.Live.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Realizations.Live.Realizations.GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Realizations.Live.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Registries.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Constants;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Registries.Interfaces;
using GMutagenEngine.Utils;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Visitors.Realizations
{
    public class CollectionLiveContextVisitor(
        IContextRegistry registry,
        ISchemaRegistry schemaRegistry) : BaseLiveContextVisitor
    {
        public override IEnumerable<Type> CanVisitTypes() => FrameworkStandardTypes.Collections;

        public override ContextBase VisitInternal(ISchema schema, LiveContextGeneratorSettings settings,
            ContextBase context = null, object instance = null)
        {
            var elementMember = schema.Members.GetValueOrDefault(SchemaConstants.ELEMENT_KEY);
            if (elementMember?.Schema == null)
                return null;

            var elementSchema = elementMember.Schema;
            var elementType = elementSchema.TargetType;
            var isPrimitive = elementType != null && FrameworkStandardTypes.Primitive.Contains(elementType);
        
            var myContext = new LiveCollectionContext(instance as IList, elementSchema, registry, isPrimitive);
        
            // Регистрируем контекст коллекции
            if (instance != null)
            {
                registry.Add(instance, myContext);
            }

            if (instance is not IList collection || isPrimitive)
                return myContext;

            // Для не-примитивных элементов создаем контексты
            foreach (var item in collection)
            {
                if (item == null)
                    continue;

                // Определяем схему для элемента
                ISchema itemSchema;
                if (!elementType.IsAbstract && !elementType.IsInterface)
                {
                    itemSchema = elementSchema;
                }
                else if (schemaRegistry.TryGet(item.GetType(), out var concreteSchema))
                {
                    itemSchema = concreteSchema;
                }
                else
                {
                    continue; // Не можем определить схему
                }

                // Рекурсивно создаем контекст для элемента
                var itemContext = settings.Generator.Generate(itemSchema, settings, myContext, item);
            }

            return myContext;
        }
    }
}