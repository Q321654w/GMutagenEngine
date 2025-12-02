using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.BaseClases;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Interfaces.Marks;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Settings;
using GMutagenEngine.Infrastructure.DynamicSchemas.Schemas.Interfaces;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Interfaces
{
    public interface ILiveContextGenerator : ILiveContextGeneratorMark
    {
        ContextBase Generate(ISchema schema, LiveContextGeneratorSettings settings, ContextBase context, object instance);
    }
}