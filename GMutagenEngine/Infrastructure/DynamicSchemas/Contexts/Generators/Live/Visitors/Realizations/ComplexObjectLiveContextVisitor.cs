using GMutagenEngine.Concept.Sync.Values.Factories.Default.Interfaces;
using GMutagenEngine.Concept.Sync.Values.Factories.Reflective.Interfaces;
using GMutagenEngine.Concept.Sync.Values.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.BaseClases;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Descriptors.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Settings;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Visitors.BaseClases;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Realizations.Live.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Registries.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Realizations.MemberInfos.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Registries.Interfaces;
using GMutagenEngine.Utils;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Visitors.Realizations
{
    public class ComplexObjectLiveContextVisitor(
        IContextRegistry registry,
        IReflectionValueFactory reflectionValueFactory,
        IDefaultValueFactory defaultValueFactory,
        ISchemaRegistry schemaRegistry) : BaseLiveContextVisitor
    {
        public override IEnumerable<Type> CanVisitTypes() => [typeof(object)];

        public override ContextBase VisitInternal(ISchema schema, LiveContextGeneratorSettings settings,
            ContextBase context = null, object instance = null)
        {
            if (instance == null)
                return null;
        
            var myContext = new LiveContext(instance, schema, registry);
     
            registry.Add(instance, myContext);

            foreach (var member in schema.Members.Values)
            {
                if (member.Schema == null || member.Name == null)
                    continue;

                var memberType = member.Schema.TargetType;
                if (memberType == null)
                    continue;
            
                if (FrameworkStandardTypes.Primitive.Contains(memberType))
                {
                    IValue value;
                    if (member.Info != null)
                    {
                        value = reflectionValueFactory.Create(instance, member.Info);
                    }
                    else
                    {
                        var memberValue = member.GetMemberValue(instance);
                        value = defaultValueFactory.Create(memberValue, memberType);
                    }
                    myContext.SetValue(member.Name, value);
                }
                else
                {
                    var memberValue = member.GetMemberValue(instance);
                    if (memberValue == null)
                        continue;

                    ISchema memberSchema;
                    if (!memberType.IsAbstract && !memberType.IsInterface)
                    {
                        memberSchema = member.Schema;
                    }
                    else if (schemaRegistry.TryGet(memberValue.GetType(), out var concreteSchema))
                    {
                        memberSchema = concreteSchema;
                    }
                    else
                    {
                        continue;
                    }
                
                    var memberContext = settings.Generator.Generate(memberSchema, settings, myContext, memberValue);
                    if (memberContext != null)
                    {
                        var descriptor = new ObjectContextDescriptor(instance, member);
                        myContext.AddDescriptor(member.Name, descriptor);
                    }
                }
            }

            return myContext;
        }
    }
}