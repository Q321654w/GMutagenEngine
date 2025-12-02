using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Descriptors.Interfaces.Marks;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Registries.Interfaces;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Descriptors.Interfaces
{
    public interface IContextDescriptor : IContextDescriptorMark
    {
        IContext? ResolveContext(IContextRegistry registry);
    }
}