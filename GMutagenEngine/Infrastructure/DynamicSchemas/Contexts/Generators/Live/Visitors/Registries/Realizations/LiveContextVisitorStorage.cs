using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Visitors.Interfaces;
using GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Visitors.Registries.Interfaces;
using GMutagenEngine.Infrastructure.Storing.Registries.Sync.Indexed;

namespace GMutagenEngine.Infrastructure.DynamicSchemas.Contexts.Generators.Live.Visitors.Registries.Realizations
{
    public class LiveContextVisitorStorage : InMemoryIndexedSyncRegistry<Type, ILiveContextVisitor>,
        ILiveContextVisitorRegistry
    {
        public void Add(ILiveContextVisitor visitor)
        {
            foreach (var type in visitor.CanVisitTypes())
                Add(type, visitor);
        }
    }
}