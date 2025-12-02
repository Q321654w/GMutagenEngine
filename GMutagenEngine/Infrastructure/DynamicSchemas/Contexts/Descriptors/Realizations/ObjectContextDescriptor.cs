using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Descriptors.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Registries.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Realizations.MemberInfos.Interfaces;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Descriptors.Realizations
{
    public class ObjectContextDescriptor(object instance, ISchemaMemberInfo memberInfo) : IContextDescriptor
    {
        public IContext? ResolveContext(IContextRegistry registry)
        {
            var currentValue = memberInfo.GetMemberValue(instance);
            if (currentValue == null)
                return null;

            return registry.Get(currentValue);
        }
    }
}