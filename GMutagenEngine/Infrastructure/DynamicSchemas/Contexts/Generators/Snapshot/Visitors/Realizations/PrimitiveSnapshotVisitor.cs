using GMutagenEngine.Concept.Sync.Values.Factories.Default.Extensions;
using GMutagenEngine.Concept.Sync.Values.Factories.Default.Interfaces;
using GMutagenEngine.Concept.Sync.Values.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.BaseClases;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Snapshot.Settings;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Snapshot.Visitors.BaseClases;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Realizations.Snapshots.Realizations;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Realizations.MemberInfos.Interfaces;
using GMutagenEngine.Utils;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Snapshot.Visitors.Realizations
{
    public class PrimitiveSnapshotVisitor(IDefaultValueFactory defaultValueFactory)
        : BaseSnapshotContextVisitor
    {
        private static readonly IEnumerable<Type> SupportedGenericDefinitions = FrameworkStandardTypes.Primitive;

        public override IEnumerable<Type> CanVisitTypes() => SupportedGenericDefinitions;

        public override ContextBase VisitInternal(ISchema schema, SnapshotContextGeneratorSettings settings,
            ContextBase context = null, object instance = null)
        {
            var targetContext = context as ContextSnapshot ?? new ContextSnapshot();

            foreach (var member in schema.Members.Values)
            {
                if (member.Schema == null || member.Name == null)
                    continue;

                IValue? value;
            
                if (instance != null && member.Schema.TargetType != null)
                {
                    var memberValue = member.GetMemberValue(instance);
                    value = defaultValueFactory.Create(memberValue, member.Schema.TargetType);
                }
                else if (member.Schema.TargetType != null)
                {
                    value = defaultValueFactory.Create(member.Schema.TargetType);
                }
                else
                {
                    continue;
                }

                if (value != null)
                {
                    targetContext.SetValue(member.Name, value);
                }
            }

            return targetContext;
        }
    }
}