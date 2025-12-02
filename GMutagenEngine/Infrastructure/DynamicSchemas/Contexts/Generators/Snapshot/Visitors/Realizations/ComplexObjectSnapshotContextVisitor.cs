using GMutagenEngine.Concept.Sync.Values.Factories.Default.Extensions;
using GMutagenEngine.Concept.Sync.Values.Factories.Default.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.BaseClases;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Snapshot.Settings;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Snapshot.Visitors.BaseClases;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Realizations.Snapshots.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Realizations.MemberInfos.Interfaces;
using GMutagenEngine.Utils;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Snapshot.Visitors.Realizations
{
    public class ComplexObjectSnapshotContextVisitor(IDefaultValueFactory defaultValueFactory) : BaseSnapshotContextVisitor
    {
        public override IEnumerable<Type> CanVisitTypes() => [typeof(object)];

        public override ContextBase VisitInternal(ISchema schema, SnapshotContextGeneratorSettings settings,
            ContextBase context = null, object instance = null)
        {
            var myContext = new ContextSnapshot();

            foreach (var member in schema.Members.Values)
            {
                if (member.Schema == null || member.Name == null)
                    continue;

                var memberType = member.Schema.TargetType;
                if (memberType == null)
                    continue;
            
                var memberInstance = instance != null ? member.GetMemberValue(instance) : null;
            
                if (FrameworkStandardTypes.Primitive.Contains(memberType))
                {
                    var value = memberInstance != null ?
                        defaultValueFactory.Create(memberInstance, memberType) : defaultValueFactory.Create(memberType);
                
                    myContext.SetValue(member.Name, value);
                }
                else
                {
                    var innerContext = settings.Generator.Generate(member.Schema, settings, myContext, memberInstance);
                    if (innerContext != null)
                    {
                        myContext.AttachChild(member.Name, innerContext);
                    }
                }
            }

            return myContext;
        }
    }
}